using ItechCleanArst.Application.Bussines.Articles.Commands;
using ItechCleanArst.Application.Bussines.Articles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetArticlesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetArticleByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateArticleCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteArticleCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}