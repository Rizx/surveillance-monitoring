using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using API.Exceptions;
using API.Shared;

namespace API.Middlewares
{
    internal class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (exception is not WeatherForecastException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                int statusCode;
                List<string> errorMessages = new();
                string exceptionMessage = exception.Message;
                _logger.LogError(exception, exception.Message);

                switch (exception)
                {
                    case WeatherForecastException e:
                        response.StatusCode = statusCode = (int)e.StatusCode;
                        errorMessages = e.ErrorMessages;
                        break;

                    default:
                        response.StatusCode = statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var responseModel = Result.Fail(errorMessages, statusCode: statusCode, exception: exceptionMessage);
                string result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result);
            }
        }
    }
}