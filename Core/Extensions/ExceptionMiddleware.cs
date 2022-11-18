using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
  public class ExceptionMiddleware
  {
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
    {
      try
      {
        var request = httpContext.Request;

        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        //get body string here...
        var requestContent = Encoding.UTF8.GetString(buffer);

        request.Body.Position = 0;  //rewinding the stream to 0
        await _next(httpContext);
      }
      catch (Exception e)
      {
        await HandleExceptionAsync(httpContext, e, logger);
      }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception e, ILogger<ExceptionMiddleware> logger)
    {
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


      string message = "Internal Server Error";
      IEnumerable<ValidationFailure> errors;
      if (e.GetType() == typeof(ValidationException))
      {
        message = e.Message;
        errors = ((ValidationException)e).Errors;
        httpContext.Response.StatusCode = 400;


        return httpContext.Response.WriteAsync(new ValidationErrorDetails()
        {
          StatusCode = 400,
          Message = message + e.Message + "from" + e.Source,
          Errors = errors
        }.ToString());
      }

      return httpContext.Response.WriteAsync(new ErrorDetails
      {
        StatusCode = httpContext.Response.StatusCode,
        Message = message
      }.ToString());
    }
  }
}
