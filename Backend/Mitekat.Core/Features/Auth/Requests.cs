namespace Mitekat.Core.Features.Auth
{
    using MediatR;

    public record AuthenticateUserRequest(string Username, string Password) : IRequest<TokenPairResponse>;
    
    public record GetTokenOwnerInfoRequest(string AccessToken) : IRequest<UserInfoResponse>;
    
    public record RefreshTokenPairRequest(string RefreshToken) : IRequest<TokenPairResponse>;

    public record RegisterNewUserRequest(string Username, string Password) : IRequest;
}
