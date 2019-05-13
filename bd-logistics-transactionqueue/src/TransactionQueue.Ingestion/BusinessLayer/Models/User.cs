namespace TransactionQueue.Ingestion.BusinessLayer.Models
{
    /// <summary> User </summary>
    public class User
    {
        #region Auto-Properties
        /// <summary>
        /// To hold value for UserName
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Constructors
        /// <summary> Constructs object of User </summary>
        /// <param name="userName"></param>
        public User(string userName)
        {
            UserName = userName;
        }
        #endregion
    }
}
