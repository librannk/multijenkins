using Logistics.Services.DeviceCommunication.API.Application.Models;
using Logistics.Services.DeviceCommunication.API.IntegrationEvents.Events;

namespace DeviceCommunication.API.Application.Interfaces
{

    /// <summary>
    /// IMapper contract
    /// </summary>
    public interface IMapper
    {
        
        /// <summary>
        /// Map transaction queue service data into transaction queue data model
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        //TransactionQueueData MapToTransactionQueueData(ProcessTransactionQueueIntegrationEvent message);
    }
}