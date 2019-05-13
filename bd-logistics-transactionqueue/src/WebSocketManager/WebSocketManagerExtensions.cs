using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TransactionQueue.SocketManager;
using TransactionQueue.SocketManager.Handlers;

namespace  TransactionQueue.SocketManager
{
    public static class WebSocketManagerExtensions
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services, Assembly assembly = null)
        {
            services.AddTransient<WebSocketConnectionManager>();

         //  Assembly ass = assembly ?? Assembly.GetEntryAssembly();
            //Commented as this file moved from the API project to this project 
            //foreach (var type in ass.ExportedTypes)
            //{
            //    if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
            //    {
                    services.AddSingleton(typeof(TQMessageHandler));
            //    }
            //}

            return services;
        }

        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                              PathString path,
                                                              WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }
    }
}