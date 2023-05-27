using ItechCleanArst.Application.Bussines.Categories.Commands;
using ItechCleanArst.Application.Bussines.Categories.DTOs;
using ItechCleanArst.Application.Bussines.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : BaseController
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<CategoryDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoriesQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        [ProducesResponseType(type: typeof(CategoryDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return Ok(result);
        }

    }
}