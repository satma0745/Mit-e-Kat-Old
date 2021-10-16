namespace Mitekat.Core.Features.Auth
{
    using System;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;

    public record TokenPairResponse(string AccessToken, string RefreshToken)
    {
        public static TokenPairResponse FromTokenPairInfo(ITokenPairInfo tokenPairInfo) =>
            new (tokenPairInfo.AccessToken.EncodedToken, tokenPairInfo.RefreshToken.EncodedToken);
    }

    public record UserInfoResponse(Guid Id, string Username, UserRole Role)
    {
        public static UserInfoResponse FromUserEntity(UserEntity userEntity) =>
            new (userEntity.Id, userEntity.Username, userEntity.Role);
    }
}
