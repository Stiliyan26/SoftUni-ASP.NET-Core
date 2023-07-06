using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        [HttpGet("api/resources")]
        [Authorize]
        public IActionResult GetResources()
        {
            return Ok($"protected resources, username: {User.Identity!.Name}");
        }
    }
}
