using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFramework.Uuid
{
    public static class ResponseTimeExtensions
    {
        public static IServiceCollection AddResponseTime(this IServiceCollection services)
        {
            services.TryAddSingleton<ResponseTimeMiddleware, ResponseTimeMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseResponseTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
