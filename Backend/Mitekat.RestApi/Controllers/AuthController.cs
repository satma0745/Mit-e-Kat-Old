namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth.Requests;
    using Mitekat.RestApi.DataTransferObjects;

    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) =>
            _mediator = mediator;
        
        [Authorize]
        [HttpGet("who-am-i")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var request = new GetTokenOwnerInfoRequest
            {
                AccessToken = AccessToken
            };
            var ownerInfo = await _mediator.Send(request);
            
            return ownerInfo switch
            {
                null => Unauthorized(),
                _ => Ok(new CurrentUserInfoDto(ownerInfo.Id, ownerInfo.Username))
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            var request = new RegisterNewUserRequest
            {
                Username = dto.Username,
                Password = dto.Password
            };
            await _mediator.Send(request);
            
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto dto)
        {
            var request = new AuthenticateUserRequest
            {
                Username = dto.Username,
                Password = dto.Password
            };
            var tokenPair = await _mediator.Send(request);
            
            return tokenPair switch
            {
                null => BadRequest("Incorrect username or password."),
                _ => Ok(new TokenPairDto(tokenPair.AccessToken, tokenPair.RefreshToken))
            };
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto)
        {
            var request = new RefreshTokenPairRequest
            {
                RefreshToken = dto.RefreshToken
            };
            var tokenPair = await _mediator.Send(request);

            return tokenPair switch
            {
                null => BadRequest(),
                _ => Ok(new TokenPairDto(tokenPair.AccessToken, tokenPair.RefreshToken))
            };
        }
    }
}
