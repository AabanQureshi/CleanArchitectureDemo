using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Shared.Result
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public ExtendedProblemDetails? Error { get; set; }

        public static Result Success()
        {
            return new Result
            {
                IsSuccess = true,
                IsError = false,
                Error = null
            };
        }

        public static Result Failure(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string title = "Error")
        {
            return new Result
            {
                IsSuccess = false,
                IsError = true,
                Error = new ExtendedProblemDetails
                {
                    Status = (int)statusCode,
                    Title = title,
                    Detail = message,
                    Type = "https://httpstatuses.com/" + (int)statusCode
                }
            };
        }
    }


    public class Result<TClass> : Result
        where TClass : notnull
    {
        public TClass Value { get; set; } = default!;

        public static Result<TClass> Success(TClass value)
        {
            return new Result<TClass>
            {
                IsSuccess = true,
                IsError = false,
                Value = value,
                Error = null

            };
        }

        public static Result<TClass> Failure(TClass value, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string title = "Error")
        {
            return new Result<TClass>
            {
                IsSuccess = false,
                IsError = true,
                Value = value,
                Error = new ExtendedProblemDetails
                {
                    Status = (int)statusCode,
                    Title = title,
                    Detail = message,
                    Type = "https://httpstatuses.com/" + (int)statusCode,
                    Extensions =
                    {
                        ["value"] = value
                    }
                }
            };
        }
    }
}
public class ExtendedProblemDetails : ProblemDetails
{
    public Dictionary<string, object> Extensions { get; set; } = new Dictionary<string, object>();
}