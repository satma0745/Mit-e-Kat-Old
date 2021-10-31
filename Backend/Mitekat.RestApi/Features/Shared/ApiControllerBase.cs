namespace Mitekat.RestApi.Features.Shared
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Net.Mime;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Persistence.Entities;

    [ApiController]
    [Route("/api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IRequester Requester
        {
            get
            {
                var idClaim = User.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier);
                var requesterId = Guid.Parse(idClaim.Value);

                var roleClaim = User.Claims.Single(claim => claim.Type == ClaimTypes.Role);
                var requesterRole = Enum.Parse<UserRole>(roleClaim.Value);

                return new RequesterInfo(requesterId, requesterRole);
            }
        }

        public override BadRequestObjectResult BadRequest(object error)
        {
            var errors = error switch
            {
                IEnumerable => error,
                _ => new[] {error}
            };

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                {
                    {"errors", errors}
                }
            };

            return new BadRequestObjectResult(problemDetails);
        }

        protected IActionResult InternalServerError() =>
            StatusCode(StatusCodes.Status500InternalServerError);
    }
}
