using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using NetFramework.Uuid;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace NetMiddlewares.ResponseTime.Tests
{
    public class ResponseTimeMiddlewareTests
    {
        [Fact]
        public async Task TestResponseTimeMiddlewareAddsHeaderCorrectly()
        {
            // Arrange
            (var context, var next) = GetOnStartingResponseAwareContext();
            ResponseTimeMiddleware mw = new ResponseTimeMiddleware();

            // Act
            await mw.InvokeAsync(context, next);

            // Assert

            Assert.True(context.Response.Headers.TryGetValue("X-Response-Time", out var value1));
            Assert.NotEmpty(value1);

        }

        private static (HttpContext, RequestDelegate) GetOnStartingResponseAwareContext()
        {
            DummyResponseFeature feature = GetResponse();

            var context = new DefaultHttpContext();
            context.Features.Set<IHttpResponseFeature>(feature);

            async Task next(HttpContext ctx)
            {
                await feature.InvokeCallBack();
            }

            return (context, (RequestDelegate)next);
        }

        private static DummyResponseFeature GetResponse()
        {
            var feature = new DummyResponseFeature
            {
                Headers = new HeaderDictionary()
            };

            return feature;
        }
    }

    class DummyResponseFeature : IHttpResponseFeature
    {
        public Stream Body { get; set; }

        public bool HasStarted { get { return hasStarted; } }

        public IHeaderDictionary Headers { get; set; }

        public string ReasonPhrase { get; set; }

        public int StatusCode { get; set; }

        public void OnCompleted(Func<object, Task> callback, object state)
        {
            //...No-op
        }

        public void OnStarting(Func<object, Task> callback, object state)
        {
            this.callback = callback;
            this.state = state;
        }

        bool hasStarted = false;
        Func<object, Task> callback;
        object state;

        public Task InvokeCallBack()
        {
            hasStarted = true;
            return callback(state);
        }
    }
}
