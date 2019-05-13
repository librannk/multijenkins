using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using BD.Core.Context;
namespace BD.Core.EventBusKafka
{
    public interface IContextHeaders
    {
        void SetContextHeaders(string key,Headers headers);

        bool TryGetContext(string key,
            Headers headers,
            out Context.Context context);
    }
}
