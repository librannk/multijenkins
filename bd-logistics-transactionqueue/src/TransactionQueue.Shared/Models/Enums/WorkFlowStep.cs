using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionQueue.Shared.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WorkFlowStep
    {
        PICK_ITEM,
        PICK_LABEL,
        PICK_BIN,
        PICK_VERIFYQTY,
        RESTOCK_LOTEXPINFO,
        RESTOCK_DESTASSIGNED,
        RESTOCK_CREDITINFO,
        RESTOCK_BIN,
        RESTOCK_VERIFYQTY
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionQueueStatus
    {
        Active,
        Pending,
        Complete,
        Error,
        Delete,
        Interim
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionQueueType
    {

        Pick,
        Restock,
        Return
    }
    
}
