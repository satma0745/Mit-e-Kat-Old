namespace Mitekat.Core.Features.Shared.Responses
{
    // Response with result payload
    public record Response<TResult>
    {
        public static Response<TResult> Success(TResult result) =>
            new(true, result, default);

        public static Response<TResult> Failure(Error error) =>
            new(false, default, error);

        public bool IsSuccess { get; }
        public TResult Result { get; }
        public Error Error { get; }
        
        private Response(bool isSuccess, TResult result, Error error)
        {
            IsSuccess = isSuccess;
            Result = result;
            Error = error;
        }

        public void Deconstruct(out bool isSuccess, out TResult result, out Error error) =>
            (isSuccess, result, error) = (IsSuccess, Result, Error);
    }

    // Response without result payload
    public record Response
    {
        public static Response Success() =>
            new(true, default);

        public static Response Failure(Error error) =>
            new(false, error);
        
        public bool IsSuccess { get; }
        public Error Error { get; }

        private Response(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public void Deconstruct(out bool isSuccess, out Error error) =>
            (isSuccess, error) = (IsSuccess, Error);
    }
}
