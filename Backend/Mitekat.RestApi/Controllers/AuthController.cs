namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth;
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
            var request = new GetTokenOwnerInfoRequest(AccessToken);
            var ownerInfo = await _mediator.Send(request);
            
            return ownerInfo switch
            {
                null => Unauthorized(),
                _ => Ok(CurrentUserInfoDto.FromUserInfoResponse(ownerInfo))
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            var request = dto.ToRegisterNewUserRequest();
            await _mediator.Send(request);
            
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto dto)
        {
            var request = dto.ToAuthenticateUserRequest();
            var tokenPair = await _mediator.Send(request);
            
            return tokenPair switch
            {
                null => BadRequest("Incorrect username or password."),
                _ => Ok(TokenPairDto.FromTokenPairResponse(tokenPair))
            };
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto)
        {
            var request = dto.ToRefreshTokenPairRequest();
            var tokenPair = await _mediator.Send(request);

            return tokenPair switch
            {
                null => BadRequest(),
                _ => Ok(TokenPairDto.FromTokenPairResponse(tokenPair))
            };
        }
    }
}
