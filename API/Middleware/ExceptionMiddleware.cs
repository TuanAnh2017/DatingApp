using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            // RequestDelegate: What is coming up next in the middleware pipeline
            // Ilogger: We can still log out our exception into terminal, we don't want to swallow our exception completely
            // IHostEnvironment: Check what environment we are running in, are we in production, are we in development ?
            this._env = env;
            this._logger = logger;
            this._next = next;
        }

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public async Task InvokeAsync(HttpContext context)
        {
            //context: because this is happening in the context of an HTTP request when we add middleway
            // we have access to HTTP request that's coming in
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                // PropertyNamingPolicy: Is just going to ensure that our response just goes back as a normal 
                // Json Formatted Response in Camel case

                var json = JsonSerializer.Serialize(response, options);
                // options: we want this to be a camel case

                await context.Response.WriteAsync(json);

            }
        }
    }
}