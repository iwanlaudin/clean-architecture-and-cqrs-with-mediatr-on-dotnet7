using ItechCleanArst.Application.Bussines.Auths.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
    }
}