using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;

namespace Services.Common.Middleware
{
    public class ExceptionMiddleware
    {


        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionMiddleware> logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong while processing {nameof(context.Request.Path)}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                default:
                    break;
            }

            string response = JsonSerializer.Serialize(errorDetails);

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(response);

        }
    }
}

public class ErrorDetails
{
    public string ErrorType { get; set; }

    public string ErrorMessage { get; set; }
}