using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChatServer.Middleware
{
    /// <summary>
    /// Middlevare for handling exceptions
    /// </summary>
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Helper function for handling exceptions
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="exception">Exception</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (exception is SecurityException)
            {
                code = HttpStatusCode.Forbidden;
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
