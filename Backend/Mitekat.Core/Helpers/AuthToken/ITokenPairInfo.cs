namespace Mitekat.Core.Helpers.AuthToken
{
    using System;

    public interface ITokenPairInfo
    {
        IAccessTokenInfo AccessToken { get; }
        IRefreshTokenInfo RefreshToken { get; }
    }

    public interface IAccessTokenInfo
    {
        Guid OwnerId { get; }
        string EncodedToken { get; }
    }

    public interface IRefreshTokenInfo
    {
        Guid TokenId { get; }
        Guid OwnerId { get; }
        DateTime ExpirationTime { get; }
        string EncodedToken { get; }
    }
}
