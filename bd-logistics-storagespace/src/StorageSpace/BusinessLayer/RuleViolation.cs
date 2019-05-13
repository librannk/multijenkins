namespace StorageSpace.API.BusinessLayer
{
    /// <summary> Helper class, that allows us to provide more details about a rule violation </summary>
    public class RuleViolation
    {
        #region Auto-Properties
        /// <summary> ErrorMessage </summary>
        public string ErrorMessage { get; private set; }

        /// <summary> PropertyName </summary>
        public string PropertyName { get; private set; }
        #endregion

        #region Constructors
        /// <summary> Initialises RuleViolation instances </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        public RuleViolation(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
