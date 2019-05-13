
using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Parsing;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json.Serialization;
using BD.Core.Logging.Models;

namespace BD.Core.Logging.Formatters
{
    public class CustomFormatter : ITextFormatter
    {
        private readonly MessageTemplate _outputTemplate;
        private readonly IEnumerable<IConfigurationSection> _containerProperties;
        private readonly IEnumerable<string> _customPropertiesKeys;
        private readonly JsonSerializerSettings _serializerSettings;

        ///// <summary>
        ///// Construct a <see cref="MessageTemplateTextFormatter"/>.
        ///// </summary>        
        ///// <param name="configuration">Supplies json configuration, or null.</param>
        public CustomFormatter(IConfigurationRoot configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var customPropertySection = configuration.GetSection("Serilog:CustomProperties");
            var containerPropertySection = configuration.GetSection("Serilog:ContainerProperties");
            var outputTemplate = configuration.GetSection("Serilog:OutputTemplate");

            _containerProperties = containerPropertySection.GetChildren();
            _customPropertiesKeys = customPropertySection.GetChildren().Select(item => item.Key);
            _outputTemplate = new MessageTemplateParser().Parse(outputTemplate.Value);

            _serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    IgnoreSerializableAttribute = true,
                    IgnoreSerializableInterface = true
                },
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var dictionary = new Dictionary<string, dynamic>();

            foreach (var token in _outputTemplate.Tokens)
            {
                if (token is TextToken)
                {
                    continue;
                }

                var pt = (PropertyToken)token;

                switch (pt.PropertyName)
                {
                    case CustomOutputProperties.CONTAINER_PROPERTIES:
                        {
                            foreach (var item in _containerProperties)
                            {
                                dictionary.Add(item.Key, item.Value);
                            }

                            break;
                        }
                    case CustomOutputProperties.CUSTOM_PROPERTIES:
                        {
                            foreach (var (key, value) in logEvent.Properties.Where(prop => _customPropertiesKeys.Contains(prop.Key)))
                            {
                                if (value.GetType() == typeof(ScalarValue))
                                {
                                    dictionary.Add(key, ((ScalarValue)value).Value);
                                }
                                else if (value.GetType() == typeof(StructureValue))
                                {
                                    dictionary.Add(key, value.ToString());
                                }
                                else if (value.GetType() == typeof(SequenceValue))
                                {
                                    dictionary.Add(key,
                                        ((SequenceValue)value).Elements.Select(e => ((ScalarValue)e).Value));
                                }
                            }

                            break;
                        }
                    case OutputProperties.PropertiesPropertyName:
                        {
                            if (!dictionary.ContainsKey(Model.DATA))
                            {
                                dictionary.Add(Model.DATA, new Dictionary<string, dynamic>());
                            }

                            if (Log.IsEnabled(LogEventLevel.Debug))
                            {
                                foreach (var (key, value) in logEvent.Properties.Where(prop => !_customPropertiesKeys.Contains(
                                    prop.Key,
                                    StringComparer.CurrentCultureIgnoreCase)))
                                {
                                    if (value.GetType() == typeof(ScalarValue))
                                    {
                                        dictionary[Model.DATA].Add(key, ((ScalarValue)value).Value);
                                    }
                                    else if (value.GetType() == typeof(StructureValue))
                                    {
                                        dictionary[Model.DATA].Add(key, value.ToString());
                                    }
                                    else if (value.GetType() == typeof(SequenceValue))
                                    {
                                        var seqVal = (SequenceValue)value;

                                        if (key == Model.SCOPE)
                                        {
                                            if (seqVal.Elements.FirstOrDefault().GetType() == typeof(DictionaryValue))
                                            {
                                                foreach (var element in ((DictionaryValue)seqVal.Elements.FirstOrDefault())
                                                    .Elements)
                                                {
                                                    dictionary[Model.DATA].Add(element.Key.Value.ToString(),
                                                        ((ScalarValue)element.Value).Value);
                                                }
                                            }
                                            else
                                            {
                                                dictionary[Model.DATA].Add(key,
                                                    ((SequenceValue)value).Elements.Select(e => e.ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (seqVal.Elements.FirstOrDefault().GetType() == typeof(ScalarValue))
                                            {
                                                dictionary[Model.DATA].Add(key,
                                                    ((SequenceValue)value).Elements.Select(e => ((ScalarValue)e).Value));
                                            }
                                            else
                                            {
                                                dictionary[Model.DATA].Add(key,
                                                    ((SequenceValue)value).Elements.Select(e => e.ToString()));
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        }
                    case OutputProperties.MessagePropertyName:
                        {
                            if (!dictionary.ContainsKey(Model.DATA))
                            {
                                dictionary.Add(Model.DATA, new Dictionary<string, dynamic>());
                            }

                            if (Log.IsEnabled(LogEventLevel.Debug) && logEvent.MessageTemplate != null)
                            {
                                dictionary[Model.DATA][Model.MESSAGE] = logEvent.MessageTemplate.Text;
                            }

                            break;
                        }
                    case OutputProperties.ExceptionPropertyName:
                        {
                            if (!dictionary.ContainsKey(Model.DATA))
                            {
                                dictionary.Add(Model.DATA, new Dictionary<string, dynamic>());
                            }

                            if (Log.IsEnabled(LogEventLevel.Debug) && logEvent.Exception != null)
                            {
                                dictionary[Model.DATA][Model.STACKTRACE] = logEvent.Exception.StackTrace;
                            }

                            break;
                        }
                }
            }

            output.WriteLineAsync(JsonConvert.SerializeObject(dictionary, _serializerSettings));
        }
    }
}