using System;
using System.Collections.Generic;
using TransactionQueue.Ingestion.BusinessLayer.Models;
using TransactionQueue.Ingestion.BusinessLayer.Models.Enums;
using TransactionQueue.Shared.DataAccess.Mongo.Entities;

namespace TransactionQueue.Ingestion.Infrastructure.DBModel
{
    /// <summary> It contains Transaction information </summary>
    public class TransactionQueue : Entity
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for FormularyId
        /// </summary>
        public int FormularyId { get; set; }
        /// <summary>
        /// To hold value for Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// To hold value for Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>		
        /// to hold the stale status		
        /// </summary>		
        public bool IsStaled { get; set; }
        /// <summary>
        /// To hold value for Exception
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// To hold value for StatusChangeDt
        /// </summary>
        public DateTime? StatusChangeDt { get; set; }
        /// <summary>
        /// To hold value for StatusChangeUtcDateTime
        /// </summary>
        public DateTime? StatusChangeUtcDateTime { get; set; }
        /// <summary>
        /// To hold value for IsaId
        /// </summary>
        public int? IsaId { get; set; }
        /// <summary>
        /// To hold value for Location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// To hold value for IncomingRequestId
        /// </summary>
        public string IncomingRequestId { get; set; }
        /// <summary>
        /// To hold value for OrderId
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// To hold value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To hold value for ExternalIsaId
        /// </summary>
        public int? ExternalIsaId { get; set; }
        /// <summary>
        /// To hold value for TranPriorityId
        /// </summary>
        public int TranPriorityId { get; set; }
        /// <summary>
        /// To hold value for RemoteOrderDetailId
        /// </summary>
        public int? RemoteOrderDetailId { get; set; }
        /// <summary>
        /// To hold value for Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// To hold value for ReceivedDt
        /// </summary>
        public DateTime? ReceivedDt { get; set; }
        /// <summary>
        /// To hold value for ReceivedUtcDateTime
        /// </summary>
        public DateTime? ReceivedUtcDateTime { get; set; }
        /// <summary>
        /// To hold value for UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// To hold value for Mrn
        /// </summary>
        public string Mrn { get; set; }
        /// <summary>
        /// To hold value for PatientName
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// To hold value for ItemId
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// To hold value for Quantity
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// To hold value for QuantityProcessed
        /// </summary>
        public int? QuantityProcessed { get; set; }
        /// <summary>
        /// To hold value for Ndc
        /// </summary>
        public string Ndc { get; set; }
        /// <summary>
        /// To hold value for GenericName
        /// </summary>
        public string GenericName { get; set; }
        /// <summary>
        /// To hold value for TradeName
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// To hold value for Strength
        /// </summary>
        public string Strength { get; set; }
        /// <summary>
        /// To hold value for StrengthUnit
        /// </summary>
        public string StrengthUnit { get; set; }
        /// <summary>
        /// To hold value for Volume
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// To hold value for VolumeUnit
        /// </summary>
        public string VolumeUnit { get; set; }
        /// <summary>
        /// To hold value for Destination
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// To hold value for PatientStation
        /// </summary>
        public string PatientStation { get; set; }
        /// <summary>
        /// To hold value for PatientRoom
        /// </summary>
        public string PatientRoom { get; set; }
        /// <summary>
        /// To hold value for PatientBed
        /// </summary>
        public string PatientBed { get; set; }
        /// <summary>
        /// To hold value for Comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// To hold value for ComponentNumber
        /// </summary>
        public int? ComponentNumber { get; set; }
        /// <summary>
        /// To hold value for NumberOfComponents
        /// </summary>
        public int? NumberOfComponents { get; set; }
        /// <summary>
        /// To hold value for PatientAcctNumber
        /// </summary>
        public string PatientAcctNumber { get; set; }
        /// <summary>
        /// To hold value for Cost
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        /// To hold value for RequestedBy
        /// </summary>
        public string RequestedBy { get; set; }
        /// <summary>
        /// To hold value for CostCenterCode
        /// </summary>
        public string CostCenterCode { get; set; }
        /// <summary>
        /// To hold value for TotalVolume
        /// </summary>
        public string TotalVolume { get; set; }
        /// <summary>
        /// To hold value for TotalVolumeUnits
        /// </summary>
        public string TotalVolumeUnits { get; set; }
        /// <summary>
        /// To hold value for OrderDose
        /// </summary>
        public string OrderDose { get; set; }
        /// <summary>
        /// To hold value for GiveRateAmount
        /// </summary>
        public string GiveRateAmount { get; set; }
        /// <summary>
        /// To hold value for GiveRateUnits
        /// </summary>
        public string GiveRateUnits { get; set; }
        /// <summary>
        /// To hold value for Concentration
        /// </summary>
        public string Concentration { get; set; }
        /// <summary>
        /// To hold value for TotalDose
        /// </summary>
        public string TotalDose { get; set; }
        /// <summary>
        /// To hold value for DispenseAmount
        /// </summary>
        public string DispenseAmount { get; set; }
        /// <summary>
        /// To hold value for TimeToLive
        /// </summary>
        public int TimeToLive { get; set; }
        /// <summary>
        /// To hold value for StorageSpaces
        /// </summary>
        public IEnumerable<Device> Devices { get; set; }
        #endregion
    }
}
