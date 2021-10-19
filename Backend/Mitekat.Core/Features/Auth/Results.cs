namespace Mitekat.Core.Features.Auth
{
    using System;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;

    public record TokenPairResult(string AccessToken, string RefreshToken)
    {
        public static TokenPairResult FromTokenPairInfo(ITokenPairInfo tokenPairInfo) =>
            new (tokenPairInfo.AccessToken.EncodedToken, tokenPairInfo.RefreshToken.EncodedToken);
    }

    public record UserInfoResult(Guid Id, string Username, UserRole Role)
    {
        public static UserInfoResult FromUserEntity(UserEntity userEntity) =>
            new (userEntity.Id, userEntity.Username, userEntity.Role);
    }
}
