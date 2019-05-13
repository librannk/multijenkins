using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using StorageSpace.API.Model;

namespace StorageSpace.API.IntegrationEvents.Events
{
    /// <summary> StorageSpaceResponseEvent </summary>
    public class StorageSpaceResponseEvent : Event
    {
        #region Constructors
        /// <summary> StorageSpaceResponseEvent constructor </summary>
        public StorageSpaceResponseEvent()
            : base()
        { }

        /// <summary> StorageSpaceResponseEvent constructor </summary>
        /// <param name="id">Guid</param>
        /// <param name="createDate">DateTime</param>
        public StorageSpaceResponseEvent(Guid id, DateTime createDate)
            : base(id, createDate)
        { }
        #endregion

        #region Auto-Properties
        /// <summary> TransactionQueueId </summary>
        public string TransactionQueueId { get; set; }

        /// <summary> FormularyId </summary>
        public int FormularyId { get; set; }

        /// <summary> MedId </summary>
        public int MedId { get; set; }

        /// <summary> StorageSpaces </summary>
        public List<Device> Devices { get; set; }
        #endregion
    }
}
