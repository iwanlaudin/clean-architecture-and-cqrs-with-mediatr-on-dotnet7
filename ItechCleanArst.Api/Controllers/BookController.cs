using ItechCleanArst.Application.Bussines.Books.Commands;
using ItechCleanArst.Application.Bussines.Books.DTOs;
using ItechCleanArst.Application.Bussines.Books.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : BaseController
    {
        /// <summary>
        /// Get all books
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<BookDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBooksQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(template:"{id}")]
        [ProducesResponseType(type: typeof(BookDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBookByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        
        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateBookCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteBookCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}