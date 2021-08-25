using Microsoft.AspNetCore.Mvc;
using FluentMediator;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
