namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    using AutoMapper;
    using Mitekat.Core.Helpers.AuthToken;

    internal class AuthenticateUserMappingProfile : Profile
    {
        public AuthenticateUserMappingProfile() =>
            CreateMap<ITokenPairInfo, AuthenticateUserResult>()
                .ForMember(
                    destinationMember: result => result.AccessToken,
                    memberOptions: config => config.MapFrom(tokenPairInfo => tokenPairInfo.AccessToken.EncodedToken))
                .ForMember(
                    destinationMember: result => result.RefreshToken,
                    memberOptions: config => config.MapFrom(tokenPairInfo => tokenPairInfo.RefreshToken.EncodedToken));
    }
}
