namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class RefreshTokenPairHandler : IRequestHandler<RefreshTokenPairRequest, TokenPairResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;

        public RefreshTokenPairHandler(IUnitOfWork unitOfWork, IAuthTokenHelper authTokenHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
        }
        
        public async Task<TokenPairResponse> Handle(RefreshTokenPairRequest request, CancellationToken _)
        {
            var oldRefreshTokenInfo = _authTokenHelper.ParseRefreshToken(request.RefreshToken);
            if (oldRefreshTokenInfo is null)
            {
                // refresh token is not valid
                return null;
            }

            var oldRefreshToken = await _unitOfWork.RefreshTokens.FindAsync(oldRefreshTokenInfo.TokenId);
            if (oldRefreshToken is null)
            {
                // token is not registered
                return null;
            }

            var newTokenPairInfo = _authTokenHelper.IssueTokenPair(oldRefreshTokenInfo.OwnerId);
            var newRefreshTokenInfo = newTokenPairInfo.RefreshToken;
            
            var newRefreshToken = new RefreshTokenEntity(newRefreshTokenInfo.TokenId, newRefreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Replace(oldRefreshToken, newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return TokenPairResponse.FromTokenPairInfo(newTokenPairInfo);
        }
    }
}
