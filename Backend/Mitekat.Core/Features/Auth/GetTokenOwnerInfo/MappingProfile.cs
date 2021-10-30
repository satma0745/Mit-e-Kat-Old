namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using AutoMapper;
    using Mitekat.Core.Persistence.Entities;

    internal class GetTokenOwnerInfoResultMappingProfile : Profile
    {
        public GetTokenOwnerInfoResultMappingProfile() =>
            CreateMap<UserEntity, GetTokenOwnerInfoResult>();
    }
}
