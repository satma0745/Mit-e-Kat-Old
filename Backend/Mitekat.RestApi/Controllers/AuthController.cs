namespace Mitekat.RestApi.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.RestApi.DataTransferObjects;
    using Mitekat.RestApi.Extensions;

    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) =>
            _mediator = mediator;
        
        [Authorize]
        [HttpGet("who-am-i")]
        public Task<IActionResult> GetCurrentUserInfo() =>
            _mediator
                .Send(new GetTokenOwnerInfoRequest(AccessToken))
                .ToActionResult(
                    userInfo => Ok(CurrentUserInfoDto.FromUserInfoResult(userInfo)),
                    error => error switch
                    {
                        Error.UnauthorizedError => Unauthorized(),
                        _ => InternalServerError()
                    });

        [HttpPost("register")]
        public Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto) =>
            _mediator
                .Send(dto.ToRegisterNewUserRequest())
                .ToActionResult(Ok, _ => InternalServerError());

        [HttpPost("authenticate")]
        public Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto dto) =>
            _mediator
                .Send(dto.ToAuthenticateUserRequest())
                .ToActionResult(
                    tokenPair => Ok(TokenPairDto.FromTokenPairResult(tokenPair)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest("Incorrect username or password."),
                        Error.NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });

        [HttpPost("refresh")]
        public Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairDto dto) =>
            _mediator
                .Send(dto.ToRefreshTokenPairRequest())
                .ToActionResult(
                    tokenPair => Ok(TokenPairDto.FromTokenPairResult(tokenPair)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest(),
                        _ => InternalServerError()
                    });
    }
}
