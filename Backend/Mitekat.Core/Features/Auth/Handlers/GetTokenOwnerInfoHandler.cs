namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Features.Auth.Requests;
    using Mitekat.Core.Features.Auth.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class GetTokenOwnerInfoHandler : IRequestHandler<GetTokenOwnerInfoRequest, UserInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;

        public GetTokenOwnerInfoHandler(IUnitOfWork unitOfWork, IAuthTokenHelper authTokenHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
        }
        
        public async Task<UserInfoResponse> Handle(GetTokenOwnerInfoRequest request, CancellationToken _)
        {
            var accessTokenInfo = _authTokenHelper.ParseAccessToken(request.AccessToken);
            if (accessTokenInfo is null)
            {
                return null;
            }
            
            var tokenOwner = await _unitOfWork.Users.FindAsync(accessTokenInfo.OwnerId);

            return new UserInfoResponse
            {
                Id = tokenOwner.Id,
                Username = tokenOwner.Username
            };
        }
    }
}
