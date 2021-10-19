namespace Mitekat.Core.Features.Shared.Responses
{
    public class Error
    {
        public static readonly NotFoundError NotFound = new();
        public static readonly UnauthorizedError Unauthorized = new();
        public static readonly AccessViolationError AccessViolation = new();
        public static readonly ConflictError Conflict = new();
        
        private Error()
        {
        }

        public sealed class NotFoundError : Error
        {
        }

        public sealed class UnauthorizedError : Error
        {
        }

        public sealed class AccessViolationError : Error
        {
        }

        public sealed class ConflictError : Error
        {
        }
    }
}
