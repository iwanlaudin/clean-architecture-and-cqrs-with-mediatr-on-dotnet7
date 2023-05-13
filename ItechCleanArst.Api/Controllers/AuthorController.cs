using ItechCleanArst.Application.Bussines.Authors.Commands;
using ItechCleanArst.Application.Bussines.Authors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAuthorsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}