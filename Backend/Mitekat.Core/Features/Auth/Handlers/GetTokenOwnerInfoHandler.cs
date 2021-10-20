namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading.Tasks;
    using Mitekat.Core.Features.Shared;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class GetTokenOwnerInfoHandler : RequestHandlerBase<GetTokenOwnerInfoRequest, UserInfoResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;

        public GetTokenOwnerInfoHandler(IUnitOfWork unitOfWork, IAuthTokenHelper authTokenHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
        }
        
        protected override async Task<Response<UserInfoResult>> HandleAsync(GetTokenOwnerInfoRequest request)
        {
            var accessTokenInfo = _authTokenHelper.ParseAccessToken(request.AccessToken);
            if (accessTokenInfo is null)
            {
                return Failure(Error.Unauthorized);
            }
            
            var tokenOwner = await _unitOfWork.Users.FindAsync(accessTokenInfo.OwnerId);
            return Success(UserInfoResult.FromUserEntity(tokenOwner));
        }
    }
}
