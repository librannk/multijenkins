using BD.Core.EventBus.Events;
using System.Threading.Tasks;

namespace TransactionQueue.Ingestion.IntegrationEvents.EventHandling
{
    /// <summary>
    /// This interface helps to defer execution from current class.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Execute the method based upon event
        /// </summary>
        /// <param name="event"> data received from Event-Bus</param>
        Task Execute(Event @event);
    }
}
