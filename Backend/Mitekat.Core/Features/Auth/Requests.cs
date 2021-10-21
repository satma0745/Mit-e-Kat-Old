namespace Mitekat.Core.Features.Auth
{
    using Mitekat.Core.Features.Shared.Requests;

    public record AuthenticateUserRequest(string Username, string Password) : RequestBase<TokenPairResult>;
    
    public record GetTokenOwnerInfoRequest(IRequester Requester) : RequestBase<UserInfoResult>;
    
    public record RefreshTokenPairRequest(string RefreshToken) : RequestBase<TokenPairResult>;

    public record RegisterNewUserRequest(string Username, string Password) : RequestBase;
}
