namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.RestApi.DataTransferObjects;
    using Mitekat.RestApi.Services;

    public class AuthController : ApiControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService) =>
            _userService = userService;
        
        [HttpGet("who-am-i")]
        public async Task<IActionResult> GetCurrentUserInfo([FromQuery] string accessToken)
        {
            if (accessToken.StartsWith("Bearer"))
            {
                accessToken = accessToken.Replace("Bearer ", string.Empty);
            }
            var user = await _userService.GetTokenOwnerInfo(accessToken);
            
            return user switch
            {
                null => Unauthorized(),
                _ => Ok(new CurrentUserInfoDto(user.Id, user.Username))
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserDto dto)
        {
            await _userService.RegisterNewUser(dto.Username, dto.Password);
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserDto dto)
        {
            var accessToken = await _userService.AuthenticateUser(dto.Username, dto.Password);
            
            return accessToken switch
            {
                null => BadRequest("Incorrect username or password."),
                _ => Ok(accessToken)
            };
        }
    }
}
