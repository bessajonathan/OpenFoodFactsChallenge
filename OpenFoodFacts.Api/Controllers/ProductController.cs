using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using NSwag.Annotations;
using OpenFoodFacts.Application.ApiDetails.Queries;
using OpenFoodFacts.Application.Product.Commands.ChangeStatus;
using OpenFoodFacts.Application.Product.Commands.Update;
using OpenFoodFacts.Application.Product.Queries;
using OpenFoodFacts.Application.Product.Queries.Get;

namespace OpenFoodFacts.API.Controllers
{
    [Route("v1")]
    public class ProductController : Controller
    {
        /// <summary>
        /// Get Api Details
        /// </summary>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet]
        [OpenApiTag("Products")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Home([FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new ApiDetailsQuery()));
        }
        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("products")]
        [OpenApiTag("Products")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProducts([FromServices] IMediator mediator,[FromQuery] GetProductsQuery query)
        {
            if (query is null)
                return BadRequest();

            return Ok(await mediator.Send(query));
        }

        /// <summary>
        /// Get product by code
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("products/{code}")]
        [OpenApiTag("Products")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProduct([FromServices] IMediator mediator, [FromRoute] string code)
        {

            return Ok(await mediator.Send(new GetProductQuery{Code = code}));
        }

        /// <summary>
        /// Change product status for trash
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpDelete("products/{code}")]
        [OpenApiTag("Products")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeProductStatus([FromServices] IMediator mediator, [FromRoute] string code)
        {
            await mediator.Send(new ChangeProductStatusCommand {Code = code});

            return NoContent();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="code"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("products/{code}")]
        [OpenApiTag("Products")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromServices] IMediator mediator, [FromRoute] string code,[FromBody] UpdateProductCommand command)
        {
            if (command is null)
                return BadRequest();

            command.Code = code;

            return Accepted(await mediator.Send(command));
        }
    }
}
