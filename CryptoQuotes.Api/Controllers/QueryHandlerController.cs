using System.Threading.Tasks;
using Application.UseCases.Queries.Abstractions;
using FluentMediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class QueryHandlerController<TFilter, TResponse> : BaseController
    {
        protected readonly IMediator Mediator;

        public QueryHandlerController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetMany([FromQuery] int? start, [FromQuery] int? end,
            [FromQuery] string order, [FromQuery] TFilter filter)
        {
            var request = new EntityRequest<TFilter>()
            {
                Filter = filter,
                Start = start,
                End = end,
                OrderBy = order
            };

            var result = await Mediator.SendAsync<GetManyResponse<TResponse>>(request);

            return Ok(result);
        }
    }
}
