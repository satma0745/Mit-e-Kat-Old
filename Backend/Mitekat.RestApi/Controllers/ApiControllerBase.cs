namespace Mitekat.RestApi.Controllers
{
    using System.Linq;
    using System.Net.Mime;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected string AccessToken
        {
            get
            {
                var authorizationHeaderStringValues = Request.Headers["Authorization"];
                if (authorizationHeaderStringValues.Count != 1)
                {
                    return null;
                }

                var authorizationHeader = authorizationHeaderStringValues.Single();
                if (!authorizationHeader.StartsWith("Bearer "))
                {
                    return null;
                }

                return authorizationHeader.Replace("Bearer ", string.Empty);
            }
        }

        protected IActionResult InternalServerError() =>
            StatusCode(StatusCodes.Status500InternalServerError);
    }
}
