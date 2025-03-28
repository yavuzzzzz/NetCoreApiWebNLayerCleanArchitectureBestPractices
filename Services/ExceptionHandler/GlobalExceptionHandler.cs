using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHandler
{
    public class GlobalExceptionHandler:IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorAsDto = ServiceResult.Failure(exception.Message, HttpStatusCode.InternalServerError);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)errorAsDto.Status;
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

            // If return true, the exception will be handled by this exception handler
            return true;
        }
    }
}
