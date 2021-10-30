namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using AutoMapper;
    using Mitekat.Core.Helpers.AuthToken;

    internal class RefreshTokenPairResultMappingProfile : Profile
    {
        public RefreshTokenPairResultMappingProfile() =>
            CreateMap<ITokenPairInfo, RefreshTokenPairResult>()
                .ForMember(
                    destinationMember: result => result.AccessToken,
                    memberOptions: config => config.MapFrom(tokenPairInfo => tokenPairInfo.AccessToken.EncodedToken))
                .ForMember(
                    destinationMember: result => result.RefreshToken,
                    memberOptions: config => config.MapFrom(tokenPairInfo => tokenPairInfo.RefreshToken.EncodedToken));
    }
}
