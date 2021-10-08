namespace Mitekat.RestApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HealthCheckController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus() => Ok();
    }
}
