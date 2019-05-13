using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using BD.Core.Context.Http.Middlewares;

namespace BD.Core.Context.Http.Extensions
{
    public static class ExecutionContextExtension
    {
        public static IServiceCollection AddExecutionContext(this IServiceCollection services,
            ILogger logger)
        {
            services.TryAddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.TryAddSingleton<IExecutionContextFactory, ExecutionContextFactory>();
            return services;
        }

        public static IApplicationBuilder UseExecutionContext(this IApplicationBuilder app, IConfiguration configuration,
            ILogger logger)
        {
            app.UseMiddleware<ExecutionContextMiddleware>();
            return app;
        }
    }
}
