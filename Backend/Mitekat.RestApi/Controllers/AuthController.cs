namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Services;
    using Mitekat.RestApi.DataTransferObjects;

    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;
        
        [Authorize]
        [HttpGet("who-am-i")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var user = await _authService.GetTokenOwnerInfo(AccessToken);
            
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
                _ => Ok(new TokenPairDto(tokenPair))
            };
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto)
        {
            var tokenPair = await _authService.RefreshTokenPair(dto.RefreshToken);

            return tokenPair switch
            {
                null => BadRequest(),
                _ => Ok(new TokenPairDto(tokenPair))
            };
        }
    }
}
