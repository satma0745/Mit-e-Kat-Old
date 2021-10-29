namespace Mitekat.RestApi.Features.Auth
{
    using System;
    using Mitekat.Core.Features.Auth.AuthenticateUser;
    using Mitekat.Core.Features.Auth.GetTokenOwnerInfo;
    using Mitekat.Core.Features.Auth.RefreshTokenPair;
    using Mitekat.Core.Features.Auth.RegisterNewUser;
    using Mitekat.Core.Features.Auth.UpdateUser;
    using Mitekat.Core.Features.Shared.Requests;

    public record GetTokenOwnerInfoResultDto(Guid Id, string Username, string Role)
    {
        public static GetTokenOwnerInfoResultDto FromResult(GetTokenOwnerInfoResult result) =>
            new(result.Id, result.Username, result.Role.ToString());
    }
    
    public record RefreshTokenPairResultDto(string AccessToken, string RefreshToken)
    {
        public static RefreshTokenPairResultDto FromResult(RefreshTokenPairResult result) =>
            new(result.AccessToken, result.RefreshToken);
    }

    public record AuthenticateUserResultDto(string AccessToken, string RefreshToken)
    {
        public static AuthenticateUserResultDto FromResult(AuthenticateUserResult result) =>
            new(result.AccessToken, result.RefreshToken);
    }

    public record RegisterNewUserRequestDto(string Username, string Password)
    {
        public RegisterNewUserRequest ToRequest() =>
            new(Username, Password);
    }

    public record UpdateUserRequestDto(string Username, string Password)
    {
        public UpdateUserRequest ToRequest(Guid userId, IRequester requester) =>
            new(userId, Username, Password, requester);
    }

    public record AuthenticateUserRequestDto(string Username, string Password)
    {
        public AuthenticateUserRequest ToRequest() =>
            new(Username, Password);
    }

    public record RefreshTokenRequestDto(string RefreshToken)
    {
        public RefreshTokenPairRequest ToRequest() =>
            new(RefreshToken);
    }
}