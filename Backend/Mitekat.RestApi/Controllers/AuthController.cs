namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth;
    using Mitekat.Core.Features.Shared.Responses;
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
            var response = await _mediator.Send(request);
            
            return response switch
            {
                (true, var userInfo, _) => Ok(CurrentUserInfoDto.FromUserInfoResult(userInfo)),
                (false, _, var error) => error switch
                {
                    Error.UnauthorizedError => Unauthorized(),
                    _ => InternalServerError()
                }
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            var request = dto.ToRegisterNewUserRequest();
            var response = await _mediator.Send(request);
            
            return response switch
            {
                (true, _, _) => Ok(),
                (false, _, _) => InternalServerError()
            };
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto dto)
        {
            var request = dto.ToAuthenticateUserRequest();
            var response = await _mediator.Send(request);
            
            return response switch
            {
                (true, var tokenPair, _) => Ok(TokenPairDto.FromTokenPairResponse(tokenPair)),
                (false, _, var error) => error switch
                {
                    Error.ConflictError => BadRequest("Incorrect username or password."),
                    Error.NotFoundError => NotFound(),
                    _ => InternalServerError()
                }
            };
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto)
        {
            var request = dto.ToRefreshTokenPairRequest();
            var response = await _mediator.Send(request);

            return response switch
            {
                (true, var tokenPair, _) => Ok(TokenPairDto.FromTokenPairResponse(tokenPair)),
                (false, _, var error) => error switch
                {
                    Error.ConflictError => BadRequest(),
                    _ => InternalServerError()
                }
            };
        }
    }
}
