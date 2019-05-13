using Autofac;
using BD.Core.EventBus;
using BD.Core.EventBus.Abstractions;
using BD.Core.EventBus.Events;
using BD.Core.Security.ValidateToken;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Tag;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Core.Context;

namespace BD.Core.EventBusKafka
{
    public class EventBusKafka : IEventBus, IDisposable
    {
        #region CONSTANTS
        public const string User = "user";
        public const string DurationNs = "durationNS";
        public const string CorrelationCurrentId = "correlationCurrentId";
        public const string CorrelationParentId = "correlationParentId";
        public const string CorrelationOriginId = "correlationOriginId";
        public const string Authorization = "Authorization";
        public const string ContextHeader = "ContextHeader";

        private const string TraceId = "X-B3-TraceId";
        private const string SpanID = "X-B3-SpanId";
        private const string ParentSpanId = "X-B3-ParentSpanId";
        private const string SampleFlag = "X-B3-Sampled";
        private const string UBER = "uber-trace-id";
        private const string SpanKindConsumer = "EventHub_Consumer";
        private const string SpanKindProducer = "EventHub_Producer";
        #endregion

        #region Private readonly variables
        private readonly EventBusConfiguration _kafkaConfig;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILogger<EventBusKafka> _logger;
        private readonly bool _trace;
        private readonly ConcurrentDictionary<string, Consumer<Ignore, JObject>> _consumerCollection;
        private readonly ILifetimeScope _autofac;
        private readonly string _autofacScopeName = "PLX_event_bus";
        private readonly ITracer _tracer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExecutionContextFactory _executionContextFactory;
        private readonly IContextHeaders _contextHeaders;
        #endregion

        #region Private variables
        private int _kafkaErrorCount;
        private string _eventName;
        private ValidateToken _validateToken;
        private string _pemPath;
        private string _pemFile = "cacert.pem";
        private Producer<Null, Event> _producer;
        #endregion

        #region Constructors
        public EventBusKafka()
        {
        }
        public EventBusKafka(EventBusConfiguration kafkaConfiguration,
            IEventBusSubscriptionsManager subsManager,
            ILifetimeScope lifetimeScope,
            ILogger<EventBusKafka> logger,
            IConfigurationSection traceSection,
            ITracer tracer,
            IHttpContextAccessor httpContextAccessor,
            IExecutionContextFactory executionContextFactory,
            IContextHeaders contextHeaders)
        {
            _kafkaConfig = kafkaConfiguration;
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            _autofac = lifetimeScope;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _executionContextFactory = executionContextFactory;
            _contextHeaders = contextHeaders;
            
            _consumerCollection = new ConcurrentDictionary<string, Consumer<Ignore, JObject>>();
            if (!bool.TryParse(traceSection.GetSection("Enabled").Value, out _trace))
            {
                _trace = true;
            }

            CreateTokenValidatorObject();

            _pemPath = Path.Combine(AppContext.BaseDirectory, _pemFile);
            _producer = new Producer<Null, Event>(ProducerConfig(), null, BinarySerializer);
            _tracer = tracer;
            
        }

        #endregion

        #region Public Methods
        public void Publish(string topicName, Event @event, Dictionary<string, string> headers)
        {
            try
            {
                var message = new Message<Null, Event>
                {
                    Value = @event,
                    Headers = new Headers()
                };
                //add correlation headers
                _producer.OnError += (_, e) =>
                {
                    if (e.IsBrokerError || e.IsFatal)
                    {
                        _logger.LogError($"error on the Producer: [{e.Reason} [{e.Code}]]");
                    }
                };

                if (_tracer != null)
                {
                    using (var scope = _tracer.BuildSpan(SpanKindProducer).StartActive(true))
                    {
                        var span = _tracer.ActiveSpan
                            .SetTag(Tags.SpanKind, Tags.SpanKindProducer)
                            .SetTag(Tags.MessageBusDestination, topicName);

                        _tracer.Inject(span.Context, BuiltinFormats.HttpHeaders, new TextMapInjectAdapter(headers));
                        //add correlation headers
                        SetHeaders(headers, message.Headers);
                        //TenantContext :set current context header for tenant,facility
                        _contextHeaders.SetContextHeaders(ContextHeader, 
                            message.Headers);

                        _producer.ProduceAsync(topicName, message)
                            .GetAwaiter()
                            .GetResult();
                    }
                }
                else
                {
                    SetHeaders(headers, message.Headers);

                    _producer.ProduceAsync(topicName, message).GetAwaiter().GetResult();
                }
            }
            catch (KafkaException e)
            {
                _logger.LogError(e, $"failed to deliver message: {e.Message} [{e.Error.Code}]");
                throw;
            }
        }

        public void Dispose()
        {
            foreach (var item in _consumerCollection)
            {
                item.Value.Dispose();
            }

            _producer?.Dispose();
            _subsManager.Clear();
        }

        public void Subscribe<T, TH>(string topicName, string groupId)
            where T : Event
            where TH : IEventHandler<T>
        {
            _eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(_eventName, topicName, groupId);
            _subsManager.AddSubscription<T, TH>();

        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicEventHandler
        {
            //  DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            foreach (var item in _consumerCollection)
            {
                item.Value.Unsubscribe();
            }
            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicEventHandler => _subsManager.RemoveDynamicSubscription<TH>(eventName);
        #endregion

        #region Private Methods
        private void CreateTokenValidatorObject()
        {
            try
            {
                if (!string.IsNullOrEmpty(_kafkaConfig.TokenServerUri))
                {
                    _validateToken = new ValidateToken(_kafkaConfig.TokenServerUri);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"failed to Create Object of TokenValidator  message: {ex.Message}");
            }
        }

        private byte[] BinarySerializer(string topic, Event data)
        {
            var convertedData = JsonConvert.SerializeObject(data);
            return Encoding.ASCII.GetBytes(convertedData);
        }

        private JObject JsonDeserialize(string topic, ReadOnlySpan<byte> data, bool isNull)
        {
            var encodedString = Encoding.ASCII.GetString(data);
            var obj = JsonConvert.DeserializeObject<JObject>(encodedString);

            return obj;
        }

        private ConsumerConfig ConsumerConfig(string groupId)
        {
            return !_kafkaConfig.IsEventHub
                ? new ConsumerConfig
                {
                    BootstrapServers = _kafkaConfig.KafkaConfiguration.Endpoint,
                    GroupId = groupId,
                    EnableAutoCommit = _kafkaConfig.AutoCommit
                }
                : new ConsumerConfig
                {
                    BootstrapServers = _kafkaConfig.EventHubConfiguration.Endpoint,
                    GroupId = groupId,
                    SecurityProtocol = SecurityProtocolType.Sasl_Ssl,
                    SaslMechanism = SaslMechanismType.Plain,
                    SaslUsername = _kafkaConfig.EventHubConfiguration.Username,
                    SaslPassword = _kafkaConfig.EventHubConfiguration.Password,
                    BrokerVersionFallback = _kafkaConfig.EventHubConfiguration.BrokerVersionFallback,
                    SslCaLocation = _pemPath,
                    ApiVersionFallbackMs = 0,
                    AutoOffsetReset = AutoOffsetResetType.Earliest,
                    EnableAutoCommit = true,
                    AutoCommitIntervalMs = 5000,
                    SocketKeepaliveEnable = true
                };
        }

        private ProducerConfig ProducerConfig()
        {
            return !_kafkaConfig.IsEventHub
                ? new ProducerConfig
                {
                    BootstrapServers = _kafkaConfig.KafkaConfiguration.Endpoint,
                    Acks = _kafkaConfig.Acknowledgement
                }
                : new ProducerConfig
                {

                    BootstrapServers = _kafkaConfig.EventHubConfiguration.Endpoint,
                    SecurityProtocol = SecurityProtocolType.Sasl_Ssl,
                    SaslMechanism = SaslMechanismType.Plain,
                    SaslUsername = _kafkaConfig.EventHubConfiguration.Username,
                    SaslPassword = _kafkaConfig.EventHubConfiguration.Password,
                    SslCaLocation = _pemPath,
                    Acks = _kafkaConfig.Acknowledgement,
                    SocketKeepaliveEnable = true

                };
        }

        private void SetHeaders(Dictionary<string, string> headers, Headers messageHeaders)
        {
            //Parent Correlation ID will be set with Current Correlation ID, 
            //when new kafka request is producing
            messageHeaders.Add(CorrelationParentId, Encoding.ASCII.GetBytes(headers[CorrelationCurrentId]));
            if (headers.ContainsKey(Authorization))
            {
                messageHeaders.Add(Authorization,
                Encoding.ASCII.GetBytes(headers[Authorization].Replace("Bearer", "").Trim()));
            }

            messageHeaders.Add(CorrelationOriginId,
                headers.ContainsKey(CorrelationOriginId)
                    ? Encoding.ASCII.GetBytes(headers[CorrelationOriginId])
                    : Encoding.ASCII.GetBytes(headers[CorrelationCurrentId]));

            messageHeaders.Add(User,
                headers.ContainsKey(User) ? Encoding.ASCII.GetBytes(headers[User]) : Encoding.ASCII.GetBytes("system"));

            if (headers.ContainsKey(SpanID))
                messageHeaders.Add(SpanID, Encoding.ASCII.GetBytes(headers[SpanID]));

            if (headers.ContainsKey(TraceId))
                messageHeaders.Add(TraceId, Encoding.ASCII.GetBytes(headers[TraceId]));

            if (headers.ContainsKey(ParentSpanId))
                messageHeaders.Add(ParentSpanId, Encoding.ASCII.GetBytes(headers[ParentSpanId]));

            if (headers.ContainsKey(SampleFlag))
                messageHeaders.Add(SampleFlag, Encoding.ASCII.GetBytes(headers[SampleFlag]));

            if (headers.ContainsKey(UBER))
                messageHeaders.Add(UBER, Encoding.ASCII.GetBytes(headers[UBER]));
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            // TO DO
        }

        private void DoInternalSubscription(string eventName, string topicName, string groupId)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                var consumer = new Consumer<Ignore, JObject>(ConsumerConfig(groupId), null, JsonDeserialize);
                _consumerCollection.TryAdd(eventName, consumer);
                SetPollingForNewConsumer(eventName, topicName);

            }
        }

        private void SetPollingForNewConsumer(string eventName, string topicName)
        {
            var consumer = _consumerCollection.First(x => x.Key == eventName).Value;
            if (_kafkaConfig.PoolFromBeginning)
            {
                var topics = new List<string> { topicName };
                consumer.Assign(topics.Select(topic => new TopicPartitionOffset(topic, 0, Offset.Beginning)).ToList());
            }
            consumer.OnStatistics += (_, json)
                => Console.WriteLine($"Statistics: {json}");
            consumer.OnError += (_, e)
           =>
            {
                if (e.IsBrokerError || e.IsFatal)
                {
                    _kafkaErrorCount++;
                    _logger.LogError($"error on the consumer: {_kafkaErrorCount} [{e.Reason} [{e.Code}]]");
                }
            };
            consumer.Subscribe(topicName);
            Task.Factory.StartNew(() =>
            {

                while (_kafkaErrorCount < _kafkaConfig.MaxPoolErrorCount)
                {
                    try
                    {
                        var result = consumer.Consume(TimeSpan.FromSeconds(_kafkaConfig.PollTimeOutInSec));

                        if (result != null)
                        {
                            if (!_kafkaConfig.ValidateToken || ValidateMessageToken(result.Headers))
                            {

                                MessageSender(result, eventName);
                                if (!_kafkaConfig.AutoCommit)
                                    consumer.Commit();
                            }
                            else
                            {
                                _logger.LogCritical($"The consumer Token Validation failed for : {topicName} offset [{result.Offset}]");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _kafkaErrorCount++;
                        if (_kafkaErrorCount > _kafkaConfig.MaxPoolErrorCount)
                        {
                            _logger.LogError(ex, ex.Message);
                            throw;
                        }
                        // Log Exception 
                    }

                }
            });
        }

        private bool ValidateMessageToken(Headers headers)
        {
            var header = headers.ToDictionary(items => items.Key, items => Encoding.ASCII.GetString(items.Value));

            return !string.IsNullOrEmpty(header[Authorization]) && _validateToken != null
                ? _validateToken.TokenValidation(header[Authorization])
                : false;
        }

        private void MessageSender(ConsumeResult<Ignore, JObject> messageresult, string eventname)
        {
            foreach (var item in _subsManager.GetAllEventRegistered().Where(x => x.Name == eventname))
                using (var scope = _autofac.BeginLifetimeScope(_autofacScopeName))
                {
                    var handler = _subsManager.GetHandlersForEvent(item.Name).First();
                    var handlerType = scope.ResolveOptional(handler.HandlerType);
                    var eventType = _subsManager.GetEventTypeByName(item.Name);
                    var integrationEvent =
                        (Event)JsonConvert.DeserializeObject(messageresult.Value.ToString(), eventType);

                    //TenantContext :Change to set tenant context recevied.
                    //For Passing Tenant Context....
                    if (_contextHeaders.TryGetContext(ContextHeader,
                        messageresult.Headers, out var context))
                        { 
                            //Here we are getting exsting http request 
                            //context 
                            _executionContextFactory.SetContext(context);
                            //Have to do this hack as existing code
                            // is assuming all the values are string.
                            //When refactored this will not be required.
                            //if we don't do this next line code
                            //.ToDictionary() will fail.
                            messageresult.Headers.Remove(ContextHeader);
                        }

                    integrationEvent.Headers = messageresult.Headers.ToDictionary(items => items.Key,
                        items => Encoding.ASCII.GetString(items.Value));
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    //Logging
                    RetriveHeaders(integrationEvent.Headers);
                    //Get Context from header
                    

                    var scopedDictionary = new Dictionary<string, string>(integrationEvent.Headers);
                    scopedDictionary.Remove(Authorization);

                    // Passing logging headers in request of http context as this is consumer side code,
                    // logging headers are need if consumer further makes any http call.
                    if (_httpContextAccessor != null)
                    {
                        _httpContextAccessor.HttpContext = new DefaultHttpContext();
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationCurrentId] =
                            integrationEvent.Headers[CorrelationCurrentId];
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationOriginId] =
                            integrationEvent.Headers[CorrelationOriginId];
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationParentId] =
                            integrationEvent.Headers[CorrelationParentId];
                    }

                    using (_logger.BeginScope<IDictionary<string, string>>(scopedDictionary))
                    {
                        Stopwatch watch = null;

                        //if trace is on then trace properties will be logged
                        if (_trace)
                        {
                            // Start the Timer using Stopwatch  
                            watch = new Stopwatch();
                            watch.Start();
                        }

                        var spanScope = StartServerSpan(_tracer, scopedDictionary, SpanKindConsumer);
                        spanScope?.Span.SetTag(Tags.SpanKind, Tags.SpanKindConsumer);

                        //Invoking Handle Method
                        var result = (Task)concreteType.GetMethod("Handle")
                            .Invoke(handlerType, new object[] { integrationEvent });

                        //tracing
                        result.ContinueWith(task =>
                        {
                            if (watch != null)
                            {
                                watch.Stop();

                                var responseTimeNs = watch.ElapsedMilliseconds * 1000000;

                                var traceProperties = new Dictionary<string, dynamic>
                                {
                                    { DurationNs, responseTimeNs }
                                };

                                using (_logger.BeginScope<IDictionary<string, dynamic>>(traceProperties))
                                {
                                    _logger.LogInformation("TRACING");
                                }

                                spanScope?.Span.Finish();
                            }
                        });
                    }
                }
        }

        private void RetriveHeaders(Dictionary<string, string> messageHeaders)
        {
            messageHeaders[CorrelationCurrentId] = Guid.NewGuid().ToString();
            if (messageHeaders.ContainsKey(Authorization) && !string.IsNullOrWhiteSpace(messageHeaders[Authorization]))
            {
                messageHeaders[Authorization] = "Bearer " + messageHeaders[Authorization];
            }

            if (string.IsNullOrWhiteSpace(messageHeaders[CorrelationParentId]))
                messageHeaders[CorrelationParentId] = messageHeaders[CorrelationCurrentId];

            //Origin Correlation ID will always be set with first current Correlation ID within the request
            if (string.IsNullOrWhiteSpace(messageHeaders[CorrelationOriginId]))
            {
                messageHeaders[CorrelationOriginId] = messageHeaders[CorrelationCurrentId];
            }

            if (string.IsNullOrWhiteSpace(messageHeaders[User]))
                messageHeaders[User] = "system";
        }

        private static IScope StartServerSpan(ITracer tracer, IDictionary<string, string> headers, string operationName)
        {
            if (tracer != null)
            {
                ISpanBuilder spanBuilder;
                try
                {
                    ISpanContext parentSpanCtx =
                        tracer.Extract(BuiltinFormats.HttpHeaders, new TextMapExtractAdapter(headers));

                    spanBuilder = tracer.BuildSpan(operationName);
                    if (parentSpanCtx != null)
                    {
                        spanBuilder = spanBuilder.AsChildOf(parentSpanCtx);
                    }
                }
                catch (Exception)
                {
                    spanBuilder = tracer.BuildSpan(operationName);
                }

                return spanBuilder.StartActive(true);
            }

            return null;
        }
        #endregion
    }
}
