using ItechCleanArst.Application.Bussines.Authors.Commands;
using ItechCleanArst.Application.Bussines.Authors.DTOs;
using ItechCleanArst.Application.Bussines.Authors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : BaseController
    {
        /// <summary>
        /// Get all authors
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<AuthorDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAuthorsQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new author
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get an author by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(template:"{id}")]
        [ProducesResponseType(type: typeof(AuthorDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        
        /// <summary>
        /// Update an author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete an author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}