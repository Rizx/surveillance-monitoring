using System;
using System.Collections.Generic;
using System.Net;

namespace APIExceptions
{
    public class WeatherForecastException : Exception
    {
        public List<string> ErrorMessages { get; } = new List<string>();

        public HttpStatusCode StatusCode { get; }

        public WeatherForecastException(string exception = "", List<string> messages = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(exception)
        {
            if (messages != null)
                ErrorMessages = messages;
            StatusCode = statusCode;
        }

        public WeatherForecastException(string exception = "", string message = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(exception)
        {
            if(message != null)
                ErrorMessages = new List<string>() { message };
            StatusCode = statusCode;
        }

        public WeatherForecastException() : base()
        {
        }

        public WeatherForecastException(string message) : base(message)
        {
        }

        public WeatherForecastException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}