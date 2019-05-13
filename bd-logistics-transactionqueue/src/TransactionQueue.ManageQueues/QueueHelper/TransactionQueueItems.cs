using System;

namespace TransactionQueue.ManageQueues.QueueHelper
{
    /// <summary>
    /// response items TransactionQueueItems
    /// </summary>
    public class TransactionQueueItems
    {
        /// <summary>
        /// response Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// response PriorityName
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// response Quantity
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// response Item
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// response Location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// response Destination
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// response PatientName
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// response Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// response Color
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// response Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// response Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// response Rack
        /// </summary>
        public int Rack { get; set; }
        /// <summary>
        /// response Shelf
        /// </summary>
        public int Shelf { get; set; }
        /// <summary>
        /// response Bin
        /// </summary>
        public int Bin { get; set; }
        /// <summary>
        /// response Slot
        /// </summary>
        public int Slot { get; set; }
        /// <summary>
        /// response ReceivedDT
        /// </summary>
        public DateTime? ReceivedDT { get; set; }
        /// <summary>
        /// response TransactionPriorityOrder
        /// </summary>
        public int? TransactionPriorityOrder { get; set; }
        /// <summary>
        /// response ComponentNumber
        /// </summary>
        public int? ComponentNumber { get; set; }
    }
}