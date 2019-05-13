using Microsoft.AspNetCore.Authorization;

namespace BD.Core.Security.Authorization
{
    /// <summary>
    /// Application Ownership requirement
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class ApplicationOwnershipRequirement : IAuthorizationRequirement
    {
    }
}
