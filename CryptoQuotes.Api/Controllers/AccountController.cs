using System.Threading.Tasks;
using Api.Presenters;
using Application.UseCases.UserIdentity.Login;
using Application.UseCases.UserIdentity.Registration;
using FluentMediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
	    public AccountController(IMediator mediator) : base(mediator)
	    {
	    }
	    
        [HttpPost("signin")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest query, [FromServices] SimpleOutputPresenter presenter)
        {
	        await Mediator.PublishAsync(query);
	        return presenter.Result;
        }

		[HttpPost("signup")]
		public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationRequest command, [FromServices] SimpleOutputPresenter presenter)
		{
			await Mediator.PublishAsync(command);
			return presenter.Result;
		}
    }
}
