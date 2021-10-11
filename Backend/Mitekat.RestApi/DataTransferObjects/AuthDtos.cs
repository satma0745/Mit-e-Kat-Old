namespace Mitekat.RestApi.DataTransferObjects
{
    using System;
    using Mitekat.Core.Services;

    public record CurrentUserInfoDto(Guid Id, string Username);
    
    public class TokenPairDto
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenPairDto(ITokenPair tokenPair)
        {
            AccessToken = tokenPair.AccessToken;
            RefreshToken = tokenPair.RefreshToken;
        }
    }
    
    public record RegisterNewUserDto(string Username, string Password);

    public record AuthenticateUserDto(string Username, string Password);

    public record RefreshTokenPairDto(string RefreshToken);
}
