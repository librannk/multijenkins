using System.Collections.Generic;

namespace StorageSpace.API.BusinessLayer
{
    /// <summary> Contract to implement rule validations </summary>
    internal interface IRuleValidation
    {
        #region Auto-Properties
        /// <summary> IsValid </summary>
        bool IsValid { get; }
        #endregion

        #region Methods
        /// <summary> Throws an exception if instance of implementing class is invalid </summary>
        void OnValidate();

        /// <summary> Collection of rule violations </summary>
        IEnumerable<RuleViolation> GetRuleViolations();
        #endregion
    }
}
