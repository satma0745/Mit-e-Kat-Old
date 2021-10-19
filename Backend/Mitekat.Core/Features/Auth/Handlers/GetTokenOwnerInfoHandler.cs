namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class GetTokenOwnerInfoHandler : IRequestHandler<GetTokenOwnerInfoRequest, Response<UserInfoResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;

        public GetTokenOwnerInfoHandler(IUnitOfWork unitOfWork, IAuthTokenHelper authTokenHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
        }
        
        public async Task<Response<UserInfoResult>> Handle(GetTokenOwnerInfoRequest request, CancellationToken _)
        {
            var accessTokenInfo = _authTokenHelper.ParseAccessToken(request.AccessToken);
            if (accessTokenInfo is null)
            {
                return Response<UserInfoResult>.Failure(Error.Unauthorized);
            }
            
            var tokenOwner = await _unitOfWork.Users.FindAsync(accessTokenInfo.OwnerId);

            var result = UserInfoResult.FromUserEntity(tokenOwner);
            return Response<UserInfoResult>.Success(result);
        }
    }
}
