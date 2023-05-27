using ItechCleanArst.Application.Bussines.Articles.Commands;
using ItechCleanArst.Application.Bussines.Articles.DTOs;
using ItechCleanArst.Application.Bussines.Articles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : BaseController
    {
        /// <summary>
        /// Get all articles
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<ArticleDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetArticlesQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get article by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        [ProducesResponseType(type: typeof(ArticleDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetArticleByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create article
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("id")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateArticleCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteArticleCommand(id), cancellationToken);
            return Ok(result);
        }
    }
}