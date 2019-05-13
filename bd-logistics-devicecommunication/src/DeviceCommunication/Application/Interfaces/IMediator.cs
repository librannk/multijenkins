using BD.Core.EventBus.Events;
using System.Threading.Tasks;

namespace Logistics.Services.DeviceCommunication.API.Application.Interfaces
{
    /// <summary>
    /// interface IMediator
    /// </summary>
    public interface IMediator
    {

        /// <summary>
        /// process data comming from subscriber
        /// </summary>
        /// <param name="event"></param>
        Task Execute(Event @event);
    }
}

