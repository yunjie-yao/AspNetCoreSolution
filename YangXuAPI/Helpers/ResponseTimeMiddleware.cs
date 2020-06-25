using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace YangXuAPI.Helpers
{
    public class ResponseTimeMiddleware
    {
        // 自定义一个响应头，用于存放API响应时间
        private const string ResponseHeaderResponseTime = "X-Response-Time-ms";

        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var watch=new Stopwatch();
            watch.Start();

            context.Response.OnStarting(() =>
            {
                watch.Stop();
                var responseTime = watch.ElapsedMilliseconds;

                context.Response.Headers.Add(ResponseHeaderResponseTime, responseTime.ToString());
                return Task.CompletedTask;
            });

            return this._next(context);
        }

    }
}
