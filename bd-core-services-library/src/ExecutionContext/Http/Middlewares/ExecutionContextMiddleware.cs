using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace BD.Core.Context.Http.Middlewares
{
    /// <summary> AntiForgeryMiddleware </summary>
    public class ExecutionContextMiddleware
    {
        /// <summary>
        /// This Middleware will get tenant and facility/other context from incoming requests.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Execution Context Factory
        /// </summary>
        private readonly IExecutionContextFactory _executionContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="executionContextFactory">The executionContextFactory.</param>
        /// <param name="config">The configuration.</param>
        public ExecutionContextMiddleware(RequestDelegate next,
            IExecutionContextFactory executionContextFactory,
            IConfiguration config)
        {
            _executionContextFactory = executionContextFactory;
            _next = next;

        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            //TO DO: Code here to get from header or token, whatever u want dear
            request.Query.TryGetValue(ContextKeys.TenantKey,
                out var tenantKey);
            request.Query.TryGetValue(ContextKeys.FacilityKey,
                out var facilityKey);
            request.Query.TryGetValue(ContextKeys.FacilityCode,
                out var facilityCode);
            request.Query.TryGetValue(ContextKeys.Locale,
                out var locale);

            //Hard Code first Tenant in system 
            if (string.IsNullOrWhiteSpace(tenantKey))
                tenantKey = "e767b738-3944-4896-93a0-6f074ba16616";
            if (string.IsNullOrWhiteSpace(facilityKey))
            {
                if (request.HttpContext.GetRouteData()?.Values.ContainsKey(ContextKeys.FacilityKey) ??
                    false)
                {
                    facilityKey = request.HttpContext?.GetRouteData()?.Values[ContextKeys.FacilityKey].ToString();
                }
                else
                {
                    facilityKey = string.Empty;
                }
            }

            _executionContextFactory.Create(
                new TenantContext(tenantKey), 
                new FacilityContext(facilityKey, facilityCode),
                locale);

            await _next(context);
        }
    }
}
