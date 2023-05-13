using ItechCleanArst.Application.Bussines.Books.Commands;
using ItechCleanArst.Application.Bussines.Books.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBooksQuery(), cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet(template:"{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBookByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateBookCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteBookCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}