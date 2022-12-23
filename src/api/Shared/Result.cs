using System.Collections.Generic;
using System.Net;

namespace API.Shared
{
    public class Result<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public string Exception { get; set; }

        public Result(bool success, T data, List<string> errorMessages)
        {
            Success = success;
            Data = data;
            ErrorMessages = errorMessages;
        }

        public Result(bool success, T data, int statusCode, List<string> errorMessages, string exception)
        {
            Success = success;
            Data = data;
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
            Exception = exception;
        }
        
        public Result(bool success, int statusCode, List<string> errorMessages, string exception)
            : this(success, default, statusCode, errorMessages, exception)
        {
        }

        public static Result<T> Ok(T result)
        {
            return new Result<T>(true, result, 200, null, null);
        }

        public static Result<T> Fail(string errorMessage, int statusCode = (int)HttpStatusCode.BadRequest, string exception = null)
        {
            return new Result<T>(false, statusCode, new List<string>() { errorMessage }, exception);
        }

        public static Result<T> Error(List<string> errorMessages, int statusCode = (int)HttpStatusCode.InternalServerError, string exception = null)
        {
            return new Result<T>(false, statusCode, errorMessages, exception);
        }

        public static Result<T> Fail<P>(Result<P> other)
        {
            return new Result<T>(false, other.StatusCode, other.ErrorMessages, other.Exception);
        }
    }

    public sealed class Result : Result<string>
    {
        private Result(bool success, int statusCode, List<string> errorMessages, string exception)
            : base(success, default, statusCode, errorMessages , exception)
        {
        }

        public static Result Ok()
        {
            return new Result(false, 200, null, null);
        }

        public static Result<T> Ok<T>(T result)
        {
            return new Result<T>(true, result, 200, null, null);
        }

        new public static Result Fail(string errorMessage, int statusCode = (int)HttpStatusCode.BadRequest, string exception = null)
        {
            return new Result(false, statusCode, new List<string>() { errorMessage }, exception);
        }

        new public static Result Fail(List<string> errorMessages, int statusCode = (int)HttpStatusCode.BadRequest, string exception = null)
        {
            return new Result(false, statusCode, errorMessages , exception);
        }

        new public static Result Error(List<string> errorMessages, int statusCode = (int)HttpStatusCode.InternalServerError, string exception = null)
        {
            return new Result(false, statusCode, errorMessages, exception);
        }

        new public static Result Fail<P>(Result<P> other)
        {
            return new Result(false, other.StatusCode, other.ErrorMessages, other.Exception);
        }
    }
}