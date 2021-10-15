namespace Mitekat.Core.Features.Auth.Requests
{
    using MediatR;
    using Mitekat.Core.Features.Auth.Responses;

    public class AuthenticateUserRequest : IRequest<TokenPairResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
