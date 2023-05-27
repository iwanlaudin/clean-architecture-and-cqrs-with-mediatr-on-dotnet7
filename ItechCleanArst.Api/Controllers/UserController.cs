using ItechCleanArst.Application.Bussines.Users.Commands;
using ItechCleanArst.Application.Bussines.Users.DTOs;
using ItechCleanArst.Application.Bussines.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        [ProducesResponseType(type: typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}