using ItechCleanArst.Application.Bussines.Categories.Commands;
using ItechCleanArst.Application.Bussines.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ItechCleanArst.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoriesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;

            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return Ok(result);
        }

    }
}