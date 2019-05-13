using BD.Core.EventBus.Events;
using System.Collections.Generic;

namespace BD.Core.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(string topicName, Event @event, Dictionary<string, string> headers);

        void Subscribe<T, TH>(string topicName, string groupId)
            where T : Event
            where TH : IEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IEventHandler<T>
            where T : Event;
    }
}
