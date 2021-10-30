namespace Mitekat.RestApi.Features.Auth
{
    using AutoMapper;
    using Mitekat.Core.Features.Auth.AuthenticateUser;
    using Mitekat.Core.Features.Auth.GetTokenOwnerInfo;
    using Mitekat.Core.Features.Auth.RefreshTokenPair;
    using Mitekat.Core.Features.Auth.RegisterNewUser;
    using Mitekat.Core.Features.Auth.UpdateUser;
    using Mitekat.RestApi.Features.Auth.Dtos;

    internal class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<AuthenticateUserRequestDto, AuthenticateUserRequest>();
            CreateMap<RefreshTokenPairRequestDto, RefreshTokenPairRequest>();
            CreateMap<RegisterNewUserRequestDto, RegisterNewUserRequest>();
            CreateMap<UpdateUserRequestDto, UpdateUserRequest>();
            
            CreateMap<AuthenticateUserResult, AuthenticateUserResultDto>();
            CreateMap<GetTokenOwnerInfoResult, GetTokenOwnerInfoResultDto>()
                .ForMember(dto => dto.Role, config => config.MapFrom(result => result.Role.ToString()));
            CreateMap<RefreshTokenPairResult, RefreshTokenPairResultDto>();
        }
    }
}
