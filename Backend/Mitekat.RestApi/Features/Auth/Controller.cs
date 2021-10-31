namespace Mitekat.RestApi.Features.Auth
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Auth.AuthenticateUser;
    using Mitekat.Core.Features.Auth.GetTokenOwnerInfo;
    using Mitekat.Core.Features.Auth.RefreshTokenPair;
    using Mitekat.Core.Features.Auth.RegisterNewUser;
    using Mitekat.Core.Features.Auth.UpdateUser;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.RestApi.Extensions;
    using Mitekat.RestApi.Features.Auth.Dtos;
    using Mitekat.RestApi.Features.Shared;

    public class AuthController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("who-am-i")]
        public Task<IActionResult> GetTokenOwnerInfo() =>
            _mediator
                .Send(new GetTokenOwnerInfoRequest(Requester))
                .ToActionResult(result => Ok(_mapper.Map<GetTokenOwnerInfoResultDto>(result)));

        [HttpPost("register")]
        public Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserRequestDto dto) =>
            _mediator
                .Send(_mapper.Map<RegisterNewUserRequest>(dto))
                .ToActionResult(
                    _ => Ok(),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest(new { Username = "Username already taken" }),
                        _ => InternalServerError()
                    });

        [Authorize]
        [HttpPost("{userId:guid}/update")]
        public Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequestDto dto) =>
            _mediator
                .Send(() =>
                {
                    var request = new UpdateUserRequest(userId, Requester);
                    return _mapper.Map(dto, request);
                })
                .ToActionResult(
                    _ => Ok(),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest(new { Username = "Username already taken" }),
                        Error.AccessViolationError => Forbid(),
                        Error.NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequestDto dto) =>
            _mediator
                .Send(_mapper.Map<AuthenticateUserRequest>(dto))
                .ToActionResult(
                    result => Ok(_mapper.Map<AuthenticateUserResultDto>(result)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest("Incorrect username or password."),
                        Error.NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });

        [HttpPost("refresh")]
        public Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenPairRequestDto dto) =>
            _mediator
                .Send(_mapper.Map<RefreshTokenPairRequest>(dto))
                .ToActionResult(
                    result => Ok(_mapper.Map<RefreshTokenPairResultDto>(result)),
                    error => error switch
                    {
                        Error.ConflictError => BadRequest(),
                        _ => InternalServerError()
                    });
    }
}
