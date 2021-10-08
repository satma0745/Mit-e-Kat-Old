namespace Mitekat.RestApi.Controllers
{
    using System.Net.Mime;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
