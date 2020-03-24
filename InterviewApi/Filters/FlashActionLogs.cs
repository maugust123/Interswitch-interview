using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace InterviewApi.Filters
{
    /// <summary>
    /// Used to flash Serilog file logs
    /// </summary>
    public class FlashActionLogs : IAsyncActionFilter
    {
        /// <summary>
        /// Action execution attribute
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var result = await next();
            // execute any code after the action executes
            Log.CloseAndFlush();
        }
    }
}
