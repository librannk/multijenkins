using BD.Core.EventBus.Events;
using System.Threading.Tasks;

namespace BD.Core.EventBus.Abstractions
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}
