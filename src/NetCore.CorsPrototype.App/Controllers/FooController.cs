using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.CorsPrototype.App.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public sealed class FooController : ControllerBase
    {
        [HttpGet]
        [Route("api/v1/foo/test")]
        public IActionResult Test()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("api/v1/foo/test-restrictive")]
        [EnableCors("restrictive")]
        public IActionResult TestRestrictive()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("api/v1/foo/test-permissive")]
        [EnableCors("permissive")]
        public IActionResult TestPermissive()
        {
            return NoContent();
        }
    }
}
