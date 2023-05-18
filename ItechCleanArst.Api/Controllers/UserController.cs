using ItechCleanArst.Application.Bussines.Users.Commands;
using ItechCleanArst.Application.Bussines.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet(template:"{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            request = request with
            {
                Id = id
            };

            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}