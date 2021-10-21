namespace Mitekat.Core.Features.Auth
{
    using System;
    using Mitekat.Core.Features.Shared.Requests;

    public record AuthenticateUserRequest(string Username, string Password) : RequestBase<TokenPairResult>;
    
    public record GetTokenOwnerInfoRequest(IRequester Requester) : RequestBase<UserInfoResult>;
    
    public record RefreshTokenPairRequest(string RefreshToken) : RequestBase<TokenPairResult>;

    public record RegisterNewUserRequest(string Username, string Password) : RequestBase;

    public record UpdateUserRequest(Guid Id, string Username, string Password, IRequester Requester) : RequestBase;
}
