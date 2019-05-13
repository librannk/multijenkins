using BD.Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using StorageSpace.API.BusinessLayer;
using StorageSpace.API.Common;

namespace StorageSpace.API.IntegrationEvents.Events
{
    /// <summary> Partial StorageSpaceRequestEvent, contains only properties </summary>
    public partial class StorageSpaceRequestEvent : Event
    {
        #region Auto-Properties
        /// <summary> TransactionQueueId </summary>
        public string TransactionQueueId { get; set; }

        /// <summary> FormularyId </summary>
        public int FormularyId { get; set; }

        /// <summary> FacilityId </summary>
        public int FacilityId { get; set; }

        /// <summary> ISAId </summary>
        public int ISAId { get; set; }
        #endregion
    }

    /// <summary> Implements IRuleValidation </summary>
    public partial class StorageSpaceRequestEvent : IRuleValidation
    {
        #region Auto-Properties
        /// <summary> Whether instance is valid or not </summary>
        public bool IsValid => !GetRuleViolations().Any();
        #endregion

        #region Public Methods
        /// <summary> Throws an exception when instance is invalid </summary>
        public void OnValidate()
        {
            if (!IsValid)
                throw new ApplicationException(Constant.RulesExceptionMessage);
        }

        /// <summary> Gets collection of rule violations </summary>
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(TransactionQueueId))
                yield return new RuleViolation(nameof(TransactionQueueId), Constant.InvalidValue);
            if (FormularyId <= 0)
                yield return new RuleViolation(nameof(FormularyId), Constant.InvalidValue);
            if (FacilityId <= 0)
                yield return new RuleViolation(nameof(FacilityId), Constant.InvalidValue);
            if (ISAId <= 0)
                yield return new RuleViolation(nameof(ISAId), Constant.InvalidValue);

            yield break;
        }
        #endregion
    }
}
