using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetFramework.Uuid
{
    public class ResponseTimeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            context.Response.OnStarting(() =>
            {
                stopWatch.Stop();
                context.Response.Headers.Add("X-Response-Time", stopWatch.ElapsedMilliseconds.ToString());

                return Task.CompletedTask;
            });

            await next(context);
        }
    }
}
