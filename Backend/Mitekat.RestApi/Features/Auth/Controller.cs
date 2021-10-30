namespace Mitekat.RestApi.Features.Auth
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth.GetTokenOwnerInfo;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.RestApi.Extensions;
    using Mitekat.RestApi.Features.Auth.Dtos;
    using Mitekat.RestApi.Features.Shared;

    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) =>
            _mediator = mediator;
        
        [Authorize]
        [HttpGet("who-am-i")]
        public Task<IActionResult> GetTokenOwnerInfo() =>
            _mediator
                .Send(new GetTokenOwnerInfoRequest(Requester))
                .ToActionResult(result => Ok(GetTokenOwnerInfoResultDto.FromResult(result)));

        [HttpPost("register")]
        public Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserRequestDto dto) =>
            _mediator
                .Send(dto.ToRequest())
                .ToActionResult();

        [Authorize]
        [HttpPost("{userId:guid}/update")]
        public Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequestDto dto) =>
            _mediator
                .Send(dto.ToRequest(userId, Requester))
                .ToActionResult(
                    _ => Ok(),
                    error => error switch
                    {
                        Error.AccessViolationError => Forbid(),
                        Error.NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequestDto dto) =>
            _mediator
                .Send(dto.ToRequest())
                .ToActionResult(
                    result => Ok(AuthenticateUserResultDto.FromResult(result)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest("Incorrect username or password."),
                        Error.NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });

        [HttpPost("refresh")]
        public Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenRequestDto dto) =>
            _mediator
                .Send(dto.ToRequest())
                .ToActionResult(
                    result => Ok(RefreshTokenPairResultDto.FromResult(result)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest(),
                        _ => InternalServerError()
                    });
    }
}
