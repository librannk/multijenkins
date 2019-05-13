//-------------------------------------------------------------------------
// Copyright ©  2019 Becton Dickinson Corporation. All rights reserved.
// Warning: This computer program is protected by  Copyright   law and
// international treaties. Unauthorized reproduction or distribution
// of this program, or any portion of it, may result in severe civil
// and criminal penalties,  and will be prosecuted to the maximum
// extent possible under the law. 

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static CCEProxy.API.Common.Constants.Constants;

namespace CCEProxy.API.Entity
{
    /// <summary> This model contains detail of IncomingRequest</summary>
    public class IncomingRequest : IValidatableObject
    {
        #region Properties
        ///Request id
        public string RequestId { get; set; }
        ///Status
        public virtual string Status { get; set; }
        /// <summary>
        /// Status Message
        /// </summary>
        public string StatusMessage { get; set; }
        ///Priority
        [Required(ErrorMessage = "Priority is null")]
        public string Priority { get; set; }
        /// <summary>
        /// FacilityDetails
        /// </summary>
        public virtual IncomingFacility Facility { get; set; }
        /// <summary>
        /// PatientDetails
        /// </summary>
        public Patient Patient { get; set; }
        /// <summary>
        /// OrderDetails
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// AduDetails
        /// </summary>
        public ADM ADM { get; set; }
        /// <summary>
        /// UserDef
        /// </summary>
        public UserDef UserDef {get;set;}
        ///Items
        [Required(ErrorMessage ="No Item Present")]
        public virtual IEnumerable<Item> Items { get; set; }

        /// <summary>
        ///  Method Validating the Model
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var itemIdCount = Items.Count(x => !string.IsNullOrEmpty(x.ItemId));
            if (itemIdCount == 0)
            {
                StatusMessage = LoggingMessage.ItemIdNull;
                yield return new ValidationResult(LoggingMessage.ItemIdNull);
            }
        }
        #endregion
    }
}
