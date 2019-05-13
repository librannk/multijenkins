
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BD.Core.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BD.Core.Extensions.Tests
{
    /// <summary>
    /// Mock Controller to test extensions , filters and middleware
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]/[action]")]
    public class MiddlewareTestController : Controller
    {

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public string Get()
        {
            throw new Exception();
        }

        /// <summary>
        /// Add the claim for Swither mortgage customer.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task AuthenticateSwitcherCustomerAsync()
        {
            var claims = new List<Claim>
            {
                new Claim("tenantid", "2123123")
            };
            var userIdentity = new ClaimsIdentity(claims, "cookie");
            userIdentity.AddClaims(claims);
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(
                scheme: "BDCustomerAuthScheme",
                principal: claimsPrincipal
            );
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Authorize(Policy = "BDAdminAuthorization")]
        public string GetClaimAsync()
        {
            return "pass";

        }


        /// <summary>
        /// Tests the json handler.
        /// </summary>
        /// <returns></returns>
        public string TestJsonHandler()
        {

            return JsonHandler.Serialize(new TestSerializer
            { Field1 = "test" }
            );
        }




        /// <summary>
        /// 
        /// </summary>
        public class TestSerializer
        {
            /// <summary>
            /// The field1
            /// </summary>
            public string Field1;
        }
    }
}
