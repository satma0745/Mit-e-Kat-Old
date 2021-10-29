namespace Mitekat.RestApi.Features.HealthCheck
{
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.RestApi.Features.Shared;

    public class HealthCheckController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus() => Ok();
    }
}
