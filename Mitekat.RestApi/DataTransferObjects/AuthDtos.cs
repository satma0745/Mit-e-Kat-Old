namespace Mitekat.RestApi.DataTransferObjects
{
    using System;

    public record CurrentUserInfoDto(Guid Id, string Username);
    
    public record TokenPairDto(string AccessToken, string RefreshToken);
    
    public record RegisterNewUserDto(string Username, string Password);

    public record AuthenticateUserDto(string Username, string Password);

    public record RefreshTokenPairDto(string RefreshToken);
}
