namespace Mitekat.RestApi.DataTransferObjects
{
    using System;
    using Mitekat.Core.Features.Auth;
    using Mitekat.Core.Features.Shared.Requests;

    public record CurrentUserInfoDto(Guid Id, string Username, string Role)
    {
        public static CurrentUserInfoDto FromUserInfoResult(UserInfoResult userInfoResult) =>
            new(userInfoResult.Id, userInfoResult.Username, userInfoResult.Role.ToString());
    }
    
    public record TokenPairDto(string AccessToken, string RefreshToken)
    {
        public static TokenPairDto FromTokenPairResult(TokenPairResult tokenPairResult) =>
            new(tokenPairResult.AccessToken, tokenPairResult.RefreshToken);
    }

    public record RegisterNewUserDto(string Username, string Password)
    {
        public RegisterNewUserRequest ToRegisterNewUserRequest() =>
            new(Username, Password);
    }

    public record UpdateUserDto(string Username, string Password)
    {
        public UpdateUserRequest ToUpdateUserRequest(Guid userId, IRequester requester) =>
            new(userId, Username, Password, requester);
    }

    public record AuthenticateUserDto(string Username, string Password)
    {
        public AuthenticateUserRequest ToAuthenticateUserRequest() =>
            new(Username, Password);
    }

    public record RefreshTokenPairDto(string RefreshToken)
    {
        public RefreshTokenPairRequest ToRefreshTokenPairRequest() =>
            new(RefreshToken);
    }
}
