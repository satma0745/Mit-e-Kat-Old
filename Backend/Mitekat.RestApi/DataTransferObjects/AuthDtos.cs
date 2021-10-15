namespace Mitekat.RestApi.DataTransferObjects
{
    using System;

    public record CurrentUserInfoDto(Guid Id, string Username);
    
    public class TokenPairDto
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenPairDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
    
    public record RegisterNewUserDto(string Username, string Password);

    public record AuthenticateUserDto(string Username, string Password);

    public record RefreshTokenPairDto(string RefreshToken);
}
