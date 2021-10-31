namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class GetTokenOwnerInfoHandler : RequestHandlerBase<GetTokenOwnerInfoRequest, GetTokenOwnerInfoResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTokenOwnerInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<Response<GetTokenOwnerInfoResult>> HandleAsync(GetTokenOwnerInfoRequest request)
        {
            var tokenOwner = await _unitOfWork.Users.FindAsync(request.Requester.Id);
            return Success(_mapper.Map<GetTokenOwnerInfoResult>(tokenOwner));
        }
    }
}
