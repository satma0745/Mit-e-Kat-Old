namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class RefreshTokenPairHandler : RequestHandlerBase<RefreshTokenPairRequest, RefreshTokenPairResult>
    {
        private readonly IAuthTokenHelper _authTokenHelper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenPairHandler(IUnitOfWork unitOfWork, IAuthTokenHelper authTokenHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
            _mapper = mapper;
        }

        protected override async Task<Response<RefreshTokenPairResult>> HandleAsync(RefreshTokenPairRequest request)
        {
            var oldRefreshTokenInfo = _authTokenHelper.ParseRefreshToken(request.RefreshToken);
            if (oldRefreshTokenInfo is null)
            {
                // refresh token is not valid
                return Failure(Error.Conflict);
            }

            var oldRefreshToken = await _unitOfWork.RefreshTokens.FindAsync(oldRefreshTokenInfo.TokenId);
            if (oldRefreshToken is null)
            {
                // token is not registered
                return Failure(Error.Conflict);
            }

            var tokenOwner = await _unitOfWork.Users.FindAsync(oldRefreshTokenInfo.OwnerId);
            if (tokenOwner is null)
            {
                // token owner does not exist
                return Failure(Error.Conflict);
            }

            var newTokenPairInfo = _authTokenHelper.IssueTokenPair(tokenOwner.Id, tokenOwner.Role);
            var newRefreshTokenInfo = newTokenPairInfo.RefreshToken;

            // TODO: Use mapping
            var newRefreshToken =
                new RefreshTokenEntity(newRefreshTokenInfo.TokenId, newRefreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Replace(oldRefreshToken, newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return Success(_mapper.Map<RefreshTokenPairResult>(newTokenPairInfo));
        }
    }
}
