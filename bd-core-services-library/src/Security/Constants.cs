namespace BD.Core.Security
{
    public static class Constants
    {
        /// <summary>
        /// Authorization Policy to cross check Applicaiton id in Claim with Applicaiton id in Route
        /// </summary>
        public const string ApplicationAuthorizationPolicy = "ApplicationOwner";

        /// <summary>
        /// The anonymous application authorization policy
        /// </summary>
        public const string AnonymousApplicationAuthorizationPolicy = "AnonymousApplicationOwner";

        /// <summary>
        /// The anonymous authentication scheme
        /// </summary>
        public const string AnonymousAuthScheme = "AnonymousAuthScheme";

        /// <summary>
        /// The Mortgage Customer authentication scheme
        /// </summary>
        public const string ExistingCustomerAuthScheme = "ExistingCustomerAuthScheme";

        /// <summary>
        /// The Mortgage Customer Authorization Policy
        /// </summary>
        public const string ExistingCustomerAuthorizationPolicy = "ExistingCustomerAuthorization";

        /// <summary>
        /// The application identifier route attribute, should match was service (case manager is using)
        /// </summary>
        public const string ApplicationIdRouteAttribute = "id";

        /// <summary>
        /// Claim Type for application identifier 
        /// </summary>
        public const string ApplicationId = "applicationid";

        /// <summary>
        /// Claim Type for application identifier 
        /// </summary>
        public const string SecurityIntrusion = "SecurityIntrusion";
    }
}
