namespace Mitekat.RestApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.RestApi.DataTransferObjects;
    using Mitekat.RestApi.Services;

    public class AuthController : ApiControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService) =>
            _userService = userService;
        
        [HttpGet("who-am-i")]
        public IActionResult GetCurrentUserInfo([FromQuery] string accessToken)
        {
            if (accessToken.StartsWith("Bearer"))
            {
                accessToken = accessToken.Replace("Bearer ", string.Empty);
            }
            var user = _userService.GetTokenOwnerInfo(accessToken);
            
            return user switch
            {
                null => Unauthorized(),
                _ => Ok(new CurrentUserInfoDto(user.Id, user.Username))
            };
        }

        [HttpPost("register")]
        public IActionResult RegisterNewUser(RegisterNewUserDto dto)
        {
            _userService.RegisterNewUser(dto.Username, dto.Password);
            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser(AuthenticateUserDto dto)
        {
            var accessToken = _userService.AuthenticateUser(dto.Username, dto.Password);
            
            return accessToken switch
            {
                null => BadRequest("Incorrect username or password."),
                _ => Ok(accessToken)
            };
        }
    }
}
