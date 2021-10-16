namespace Mitekat.RestApi.DataTransferObjects
{
    using System;
    using Mitekat.Core.Features.Auth;

    public record CurrentUserInfoDto(Guid Id, string Username, string Role)
    {
        public static CurrentUserInfoDto FromUserInfoResponse(UserInfoResponse userInfoResponse) =>
            new(userInfoResponse.Id, userInfoResponse.Username, userInfoResponse.Role.ToString());
    }
    
    public record TokenPairDto(string AccessToken, string RefreshToken)
    {
        public static TokenPairDto FromTokenPairResponse(TokenPairResponse tokenPairResponse) =>
            new(tokenPairResponse.AccessToken, tokenPairResponse.RefreshToken);
    }

    public record RegisterNewUserDto(string Username, string Password)
    {
        public RegisterNewUserRequest ToRegisterNewUserRequest() =>
            new(Username, Password);
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
