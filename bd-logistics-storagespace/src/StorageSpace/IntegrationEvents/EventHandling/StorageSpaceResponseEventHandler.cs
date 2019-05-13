using BD.Core.EventBus.Abstractions;
using System.Threading.Tasks;
using StorageSpace.API.IntegrationEvents.Events;

namespace StorageSpace.API.IntegrationEvents.EventHandling
{
    public class StorageSpaceResponseEventHandler : IEventHandler<StorageSpaceResponseEvent>
    {
        public async Task Handle(StorageSpaceResponseEvent @event)
        {

        }
    }
}
