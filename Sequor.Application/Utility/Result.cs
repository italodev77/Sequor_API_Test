using System;

namespace Sequor.Application.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public string ExtraMessage { get; }
        public T? Data { get; }

        private Result(bool isSuccess, string message, T? data, string extraMessage = "")
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            ExtraMessage = extraMessage;
        }

        public static Result<T> Success(T data, string message = "", string extraMessage = "") =>
            new Result<T>(true, message, data, extraMessage);

        public static Result<T> Failure(T data, string message, string extraMessage = "") =>
            new Result<T>(false, message, data, extraMessage);
    }
}
