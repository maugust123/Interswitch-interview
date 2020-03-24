using System;
using System.Net;
using System.Threading.Tasks;
using InterviewApi.Common;
using InterviewApi.Common.Exceptions;
using InterviewApi.Common.Extensions;
using InterviewApi.Common.MailBase;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InterviewApi.Filters
{
    //http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
    /// <summary>
    /// Global error handling
    /// </summary>
    public class ErrorHandlingMiddleware : ReadConfig
    {
        private readonly RequestDelegate _next;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Logging framework
        /// </summary>
        protected ILogger Logger { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="emailSender"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, IEmailSender emailSender, ILoggerFactory loggerFactory, IOptions<AppSecretsConfig> options) : base(options)
        {
            _next = next;
            _emailSender = emailSender;
            Logger = loggerFactory.CreateLogger(GetType().Namespace);
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorMsg = exception.Message;

            if (exception is NotFoundException) { code = HttpStatusCode.NotFound; }
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is ApplicationValidationException) code = HttpStatusCode.BadRequest;
            else if (exception is ForbiddenException) code = HttpStatusCode.Forbidden;
            else if (exception is ProcessingException) code = HttpStatusCode.InternalServerError;
            else if (exception is DbUpdateException || exception is SqlException)
            {
                code = HttpStatusCode.BadRequest;
                if (exception.InnerException?.Message != null)
                    errorMsg = exception.InnerException.Message;
            }

            var result = JsonConvert.SerializeObject(new { message = errorMsg });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);

            // If the request is local, don't send the email. This is normally important when in debug mode
            if (context.Request.IsLocal()) return;

            Logger.LogError(exception.ToString());

            var exceptionType = exception.GetType().Name;

            //Send an email only if an error is not a custom exception implementation
            if (exceptionType == "ApplicationValidationException" ||
                exceptionType == "NotFoundException" ||
                exceptionType == "DbUpdateException" ||
                exceptionType == "SqlException") return;

            //Send an email to the support group
            await _emailSender.SendEmailAsync(SupportGroupEmail, EmailSubject(), exception.ToString());

        }
    }
}
