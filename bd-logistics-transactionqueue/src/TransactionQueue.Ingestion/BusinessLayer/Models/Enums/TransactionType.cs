
namespace TransactionQueue.Ingestion.BusinessLayer.Models.Enums
{
    /// <summary> TransactionType </summary>
    public enum TransactionType
    {
        /// <summary> Pick </summary>
        Pick = 1,

        /// <summary> Restock </summary>
        Restock = 2,

        /// <summary> Batch </summary>
        Batch = 3,
        
        /// <summary> Batch Parent </summary>
        BatchParent = 4,
        
        /// <summary> Cycle Cout </summary>
        CycleCount = 5
    }
}