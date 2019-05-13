using BD.Core.EventBus.Abstractions;
using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using static BD.Core.EventBus.InMemoryEventBusSubscriptionsManager;

namespace BD.Core.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicEventHandler;

        void AddSubscription<T, TH>()
           where T : Event
           where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IEventHandler<T>
             where T : Event;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicEventHandler;
        List<Type> GetAllEventRegistered();
        bool HasSubscriptionsForEvent<T>() where T : Event;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : Event;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
