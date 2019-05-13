using System.Threading.Tasks;

namespace BD.Core.EventBus.Abstractions
{
    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
