using Microsoft.AspNetCore.Builder;

namespace GymLogger.Exceptions.Web
{
    public static class GymLoggerHttpExceptionMiddlewareExtensions
    {
        /// <summary>
        /// An IApplicationBuilder extension method that will register GymLogger HTTP exception middleware.
        /// </summary>
        /// <param name="builder">The builder to act on.</param>
        /// <returns>
        /// An IApplicationBuilder.
        /// </returns>
        public static IApplicationBuilder UseGymLoggerHttpExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GymLoggerHttpExceptionMiddleware>();
        }
    }
}
