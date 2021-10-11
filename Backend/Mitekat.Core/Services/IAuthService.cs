namespace Mitekat.Core.Services
{
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Entities;

    public interface IAuthService
    {
        Task<UserEntity> GetTokenOwnerInfo(string encodeAccessToken);

        Task RegisterNewUser(string username, string plainTextPassword);

        Task<ITokenPair> AuthenticateUser(string username, string password);

        Task<ITokenPair> RefreshTokenPair(string encodedRefreshToken);
    }
    
    public interface ITokenPair
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }
    }
}
