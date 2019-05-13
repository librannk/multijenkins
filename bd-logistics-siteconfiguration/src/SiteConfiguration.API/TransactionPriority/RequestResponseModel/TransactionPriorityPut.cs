using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.TransactionPriority.RequestResponseModel
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>

    public class TransactionPriorityPut
   {
    /// <summary>
    /// Gets or Sets Description
    /// </summary>
    
    public string PriorityName { get; set; }

    /// <summary>
    /// Gets or Sets ManualPick
    /// </summary>
   
    public bool ForManualPickFlag { get; set; }

    /// <summary>
    /// Gets or Sets ManualRestock
    /// </summary>
    
    public bool ForManualRestockFlag { get; set; }

    /// <summary>
    /// Gets or Sets UseInterfaceMedName
    /// </summary>
   
    public bool UseInterfaceMedNameFlag { get; set; }

    /// <summary>
    /// Gets or Sets IsADU
    /// </summary>
    
    public bool ADUFlag { get; set; }

    /// <summary>
    /// Foreground Color
    /// </summary>
    /// <value>Foreground Color</value>
   
    public string LegendForeColor { get; set; }

    /// <summary>
    /// Background Color
    /// </summary>
    /// <value>Background Color</value>
   
    public string LegendBackColor { get; set; }

    /// <summary>
    /// Gets or Sets IsActive
    /// </summary>
    
    public bool ActiveFlag { get; set; }

    /// <summary>
    /// Gets or Sets MaxHoldLength
    /// </summary>
    
    public int MaxOnHoldLength { get; set; }

    /// <summary>
    /// Gets or Sets System
    /// </summary>
   
    public bool SystemFlag { get; set; }

    /// <summary>
    /// Gets or Sets PriorityOrder
    /// </summary>
   
    public int PriorityOrder { get; set; }

}
}
