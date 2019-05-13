using System;
using BD.Core.Context;
using Xunit;
using BD.Core.EventBusKafka;
using Confluent.Kafka;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BD.Core.EventBus.Tests
{
    public class TestEventBusContextHeader
    {
        private const string ContextKey = "ContextKey";
        private IExecutionContextAccessor _contextAccessors;
        public TestEventBusContextHeader()
        {
            _contextAccessors =  new ExecutionContextAccessor();
            _contextAccessors.Current = new Context.Context
            {
               Tenant = new TenantContext("TestKey"),
               Facility = new FacilityContext("ac","abc"),
               Locale = "en-us"
            };
        }
        [Theory]
        [InlineData("TestKey")]
        //Summary to test message header is getting correctly set
        public void Test_Get_Context_FromMessageHeader(string tenantKey)
        {
            IContextHeaders contextMiddleware = new MiddlewareContextHeaders(_contextAccessors);
            var headers = new Headers();
            headers.Add(ContextKey, ObjectToByteArray(_contextAccessors.Current));
            contextMiddleware.TryGetContext(ContextKey, headers, out var context);
            Assert.Equal(tenantKey, context.Tenant.TenantKey);
        }
        [Theory]
        [InlineData("TestKey")]
        //Summary to test message header is getting correctly set
        public void Test_Set_Context_FromMessageHeader(string tenantKey)
        {
            IContextHeaders contextMiddleware = new MiddlewareContextHeaders(_contextAccessors);
            var headers = new Headers();
            headers.Add(ContextKey, ObjectToByteArray(_contextAccessors.Current));
            contextMiddleware.SetContextHeaders(tenantKey, headers);
            contextMiddleware.TryGetContext(ContextKey, headers, out var context);
            Assert.Equal(tenantKey, context.Tenant.TenantKey);
        }

        [Fact]
        //Summary to test message header is getting correctly set
        public void Test_Get_Context_FromMessageHeader_ForContextKeyNotPresent()
        {
            IContextHeaders contextMiddleware = new MiddlewareContextHeaders(_contextAccessors);
            var headers = new Headers();
            var tenantExists=contextMiddleware.TryGetContext(ContextKey, headers, out var context);
            Assert.False(tenantExists);
        }

        [Fact]
        //Summary to test message header is getting correctly set
        public void Test_Get_Context_FromMessageHeader_ForInvalidTenant_ContextNull()
        {
            IContextHeaders contextMiddleware = new MiddlewareContextHeaders(_contextAccessors);
            var headers = new Headers();
            var tenantExists = contextMiddleware.TryGetContext(ContextKey, headers, out var context);
            Assert.Equal(default(Context.Context), context);
        }

        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
