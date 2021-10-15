namespace Mitekat.Core.Features.Auth.Requests
{
    using MediatR;

    public class RegisterNewUserRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
