using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BD.Core.Context;
using Confluent.Kafka;

namespace BD.Core.EventBusKafka
{
    //This middleware needs to be injected in cases where event bus i loaded in context of 
    //MVC application with http context.
    public class MiddlewareContextHeaders : IContextHeaders
    {
        private readonly IExecutionContextAccessor _executionContext;
        public MiddlewareContextHeaders(IExecutionContextAccessor executionContext)
        {
            _executionContext = executionContext;
        }

        bool IContextHeaders.TryGetContext(string key, 
            Headers headers,
            out Context.Context context)
        {
            if (headers.TryGetLast(key, out var header))
            {
                context = (Context.Context) ByteArrayToObject(header);
                return true;
            }
            else
            {
                context = default(Context.Context);
                return false;
            }
        }

        public void SetContextHeaders(string key,Headers headers)
        {
            if (headers!=null)
                headers.Add(key, ObjectToByteArray(_executionContext.Current));
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
