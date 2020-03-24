using InterviewApi.Filters;
using Microsoft.AspNetCore.Builder;

namespace InterviewApi.Middleware
{
    /// <summary>
    /// Custom api error handling
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// Insert error handling middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
