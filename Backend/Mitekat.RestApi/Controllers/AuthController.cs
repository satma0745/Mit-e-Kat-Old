namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.RestApi.DataTransferObjects;
    using Mitekat.RestApi.Services;

    public class AuthController : ApiControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) =>
            _authService = authService;
        
        [HttpGet("who-am-i")]
        public async Task<IActionResult> GetCurrentUserInfo([FromQuery] string accessToken)
        {
            if (accessToken.StartsWith("Bearer"))
            {
                accessToken = accessToken.Replace("Bearer ", string.Empty);
            }
            var user = await _authService.GetTokenOwnerInfo(accessToken);
            
            return user switch
            {
                null => Unauthorized(),
                _ => Ok(new CurrentUserInfoDto(user.Id, user.Username))
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            await _authService.RegisterNewUser(dto.Username, dto.Password);
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto dto)
        {
            var tokenPair = await _authService.AuthenticateUser(dto.Username, dto.Password);
            
            return tokenPair switch
            {
                null => BadRequest("Incorrect username or password."),
                _ => Ok(new TokenPairDto(tokenPair.AccessToken.EncodedToken, tokenPair.RefreshToken.EncodedToken))
            };
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto)
        {
            var tokenPair = await _authService.RefreshTokenPair(dto.RefreshToken);

            return tokenPair switch
            {
                null => BadRequest(),
                _ => Ok(new TokenPairDto(tokenPair.AccessToken.EncodedToken, tokenPair.RefreshToken.EncodedToken))
            };
        }
    }
}
