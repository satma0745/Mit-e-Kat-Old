namespace Mitekat.Core.Features.Auth
{
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    public record AuthenticateUserRequest(string Username, string Password) : IRequest<Response<TokenPairResult>>;
    
    public record GetTokenOwnerInfoRequest(string AccessToken) : IRequest<Response<UserInfoResult>>;
    
    public record RefreshTokenPairRequest(string RefreshToken) : IRequest<Response<TokenPairResult>>;

    public record RegisterNewUserRequest(string Username, string Password) : IRequest<Response<Unit>>;
}
