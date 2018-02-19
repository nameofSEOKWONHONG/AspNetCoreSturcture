using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NeonCore.WebAPI.Middleware
{
    public class HeaderAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderAuthorizationMiddleware> _logger;

        public HeaderAuthorizationMiddleware(RequestDelegate next, ILogger<HeaderAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogWarning($"In Use. BeforeRun, Order: {DateTime.Now}");

            //logic
            //여기서 hmac matching 진행해야 함.
            if(context.Request.Headers.TryGetValue("Authorization", out var authValue))
            {
                Debug.WriteLine($"header auth : {authValue}");
            }

            await _next.Invoke(context);

            _logger.LogWarning($"In Use. AfterRun, Order:{DateTime.Now}");
        }
    }

    public static class SampleMiddlewareExtensions
    {
        public static IApplicationBuilder UseSampleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderAuthorizationMiddleware>();
        }
    }
}
