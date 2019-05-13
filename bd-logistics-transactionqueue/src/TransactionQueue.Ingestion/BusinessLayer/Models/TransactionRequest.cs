using System.Collections.Generic;

namespace TransactionQueue.Ingestion.BusinessLayer.Models
{
    /// <summary>
    /// This model is used to bind facility properties
    /// </summary>
    public class FacilityRequest
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for FacilityId
        /// </summary>
        public int FacilityId { get; set; }
        /// <summary>
        /// To hold value for OrderingFacility
        /// </summary>
        public string OrderingFacility { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind Patient information
    /// </summary>
    public class Patient
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// To hold value for MiddleName
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// To hold value for LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// To hold value for Suffix
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// To hold value for Sex
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// To hold value for Dob
        /// </summary>
        public string DateOfBirth { get; set; }
        /// <summary>
        /// To hold value for Height
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// To hold value for weight
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// To hold value for AccountNumber
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// To hold value for ContactNO
        /// </summary>
        public string ContactNo { get; set; }
        /// <summary>
        /// To hold value for Bed
        /// </summary>
        public string Bed { get; set; }
        /// <summary>
        /// To hold value for Room
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// To hold value for VisitId
        /// </summary>
        public string VisitId { get; set; }
        /// <summary>
        /// To hold value for Mrn
        /// </summary>
        public string Mrn { get; set; }
        /// <summary>
        /// To hold value for PrescriptionNo
        /// </summary>
        public string PrescriptionNo { get; set; }
        /// <summary>
        /// To hold value for Dept
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// To hold value for DeliverToLocation
        /// </summary>
        public string DeliverToLocation { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind IVOrder information
    /// </summary>
    public class IVOrder
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for IsFirstDose
        /// </summary>
        public bool IsFirstDose { get; set; }
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
        /// To hold value for Route
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// To hold value for Frequency
        /// </summary>
        public string Frequency { get; set; }
        /// <summary>
        /// To hold value for GiveMedId
        /// </summary>
        public string GiveMedId { get; set; }
        /// <summary>
        /// To hold value for GiveMedDescription
        /// </summary>
        public string GiveMedDescription { get; set; }
        /// <summary>
        /// To hold value for GiveAmountMin
        /// </summary>
        public string GiveAmountMin { get; set; }
        /// <summary>
        /// To hold value for GiveAmountMax
        /// </summary>
        public int GiveAmountMax { get; set; }
        /// <summary>
        /// To hold value for GiveUnits
        /// </summary>
        public string GiveUnits { get; set; }
        /// <summary>
        /// To hold value for GiveDosageForm
        /// </summary>
        public string GiveDosageForm { get; set; }
        /// <summary>
        /// To hold value for GiveRateAmount
        /// </summary>
        public string GiveRateAmount { get; set; }
        /// <summary>
        /// To hold value for GiveRateUnits
        /// </summary>
        public string GiveRateUnits { get; set; }
        /// <summary>
        /// To hold value for GiveStrength
        /// </summary>
        public string GiveStrength { get; set; }
        /// <summary>
        /// To hold value for GiveStrengthUnits
        /// </summary>
        public string GiveStrengthUnits { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind Order details
    /// </summary>
    public class Order
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for OrderNo
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// To hold value for CopeOrderNo
        /// </summary>
        public string CopeOrderNo { get; set; }
        /// <summary>
        /// To hold value for OrderControlId
        /// </summary>
        public string OrderControlId { get; set; }
        /// <summary>
        /// To hold value for IsStatOrder
        /// </summary>
        public string IsStatOrder { get; set; }
        /// <summary>
        /// To hold value for OrderingPriority
        /// </summary>
        public string OrderingPriority { get; set; }
        /// <summary>
        /// To hold value for OrderingDrInstructions
        /// </summary>
        public string OrderingDrInstructions { get; set; }
        /// <summary>
        /// To hold value for OrderingDueTime
        /// </summary>
        public string OrderingDueTime { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind ADU Details
    /// </summary>
    public class ADM
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for AduTransId
        /// </summary>
        public string AdmTransId { get; set; }
        /// <summary>
        /// To hold value for StationName
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// To hold value for Drawer
        /// </summary>
        public string Drawer { get; set; }
        /// <summary>
        /// To hold value for Subdrawer
        /// </summary>
        public string Subdrawer { get; set; }
        /// <summary>
        /// To hold value for Pocket
        /// </summary>
        public string Pocket { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind UserDef information
    /// </summary>
    public class UserDef
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for UserDef1
        /// </summary>
        public string UsrDef1 { get; set; }
        /// <summary>
        /// To hold value for UserDef2
        /// </summary>
        public string UsrDef2 { get; set; }
        /// <summary>
        /// To hold value for UserDef3
        /// </summary>
        public string UsrDef3 { get; set; }
        /// <summary>
        /// To hold value for UserDef4
        /// </summary>
        public string UsrDef4 { get; set; }
        /// <summary>
        /// To hold value for UserDef5
        /// </summary>
        public string UsrDef5 { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind formulary data
    /// </summary>
    public class Item
    {
        #region Auto-Properties  
        /// <summary>
        /// To hold value for ItemId
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// To hold value for ItemName
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// To hold value for ComponentType
        /// </summary>
        public string ComponentType { get; set; }
        /// <summary>
        /// To hold value for ComponentStrength
        /// </summary>
        public string ComponentStrength { get; set; }
        /// <summary>
        /// To hold value for ComponentStrengthUnits
        /// </summary>
        public string ComponentStrengthUnits { get; set; }
        /// <summary>
        /// To hold value for ComponentAmount
        /// </summary>
        public string ComponentAmount { get; set; }
        /// <summary>
        /// To hold value for OrderAmount
        /// </summary>
        public string OrderAmount { get; set; }
        /// <summary>
        /// To hold value for DispenseUnits
        /// </summary>
        public string DispenseUnits { get; set; }
        /// <summary>
        /// To hold value for SupplementaryCode
        /// </summary>
        public string SupplementaryCode { get; set; }
        /// <summary>
        /// To hold value for TotalDose
        /// </summary>
        public string TotalDose { get; set; }
        /// <summary>
        /// To hold value for Volume
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// To hold value for Strength
        /// </summary>
        public string Strength { get; set; }
        /// <summary>
        /// To hold value for Concentration
        /// </summary>
        public string Concentration { get; set; }
        /// <summary>
        /// To hold value for PharmacySpecialDispInstructions
        /// </summary>
        public string PharmacySpecialDispInstructions { get; set; }
        /// <summary>
        /// To hold value for DispenseAmount
        /// </summary>
        public string DispenseAmount { get; set; }
        #endregion
    }

    /// <summary>
    /// This model is used to bind incoming transaction request
    /// </summary>
    public class TransactionRequest
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for RequestId
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// To hold value for Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// To hold value for Priority
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// To hold value for Facility
        /// </summary>
        public FacilityRequest Facility { get; set; }
        /// <summary>
        /// To hold value for Patient
        /// </summary>
        public Patient Patient { get; set; }
        /// <summary>
        /// To hold value for Order
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// To hold value for ADU
        /// </summary>
        public ADM ADM { get; set; }
        /// <summary>
        /// To hold value for UserDef
        /// </summary>
        public UserDef UserDef { get; set; }
        /// <summary>
        /// To hold value for Items
        /// </summary>
        public List<Item> Items { get; set; }
        #endregion
    }
}
