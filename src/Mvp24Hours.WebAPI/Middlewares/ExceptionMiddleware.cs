//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.Enums;
using Mvp24Hours.Extensions;
using Mvp24Hours.Helpers;
using Mvp24Hours.WebAPI.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mvp24Hours.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionOptions options;

        public ExceptionMiddleware(RequestDelegate next, IOptions<ExceptionOptions> options)
        {
            _next = next;
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options), "[ExceptionMiddleware] Options is required. Check: services.AddMvp24HoursWebExceptions().");
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Response.HasStarted)
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                if (!httpContext.Response.HasStarted)
                {
                    await HandleExceptionAsync(httpContext, ex);
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message;

            if (options.TraceMiddleware)
            {
                message = $"Message: {(exception?.InnerException ?? exception).Message} / Trace: {exception?.StackTrace}";
            }
            else
            {
                message = $"Message: {(exception?.InnerException ?? exception).Message}";
            }

            var boResult = message
                .ToMessageResult("internalservererror", MessageType.Error)
                .ToBusiness<VoidResult>();

            var messageResult = JsonHelper.Serialize(boResult);
            return context.Response.WriteAsync(messageResult);
        }
    }
}
