using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GymLogger.Exceptions.Web
{
    public class GymLoggerHttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GymLoggerHttpExceptionMiddleware> logger;

        public GymLoggerHttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.logger = loggerFactory?.CreateLogger<GymLoggerHttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (GymLoggerException httpException)
            {
                if (context.Response.HasStarted)
                {
                    this.logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                var result = httpException
                    .AsHttpException();

                context.Response.StatusCode = result.StatusCode;
                context.Response.ContentType = "application/json";                
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
