﻿namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading.Tasks;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class GetTokenOwnerInfoHandler : RequestHandlerBase<GetTokenOwnerInfoRequest, UserInfoResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTokenOwnerInfoHandler(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;
        
        protected override async Task<Response<UserInfoResult>> HandleAsync(GetTokenOwnerInfoRequest request)
        {
            var tokenOwner = await _unitOfWork.Users.FindAsync(request.Requester.Id);
            return Success(UserInfoResult.FromUserEntity(tokenOwner));
        }
    }
}
