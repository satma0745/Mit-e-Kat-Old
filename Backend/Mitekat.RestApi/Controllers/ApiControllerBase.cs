namespace Mitekat.RestApi.Controllers
{
    using System;
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

        protected IActionResult InternalServerError() =>
            StatusCode(StatusCodes.Status500InternalServerError);

        private record RequesterInfo(Guid Id, UserRole Role) : IRequester;
    }
}
