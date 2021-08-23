using System.Threading.Tasks;
using Application.UseCases.User;
using Application.UseCases.UserIdentity;
using Application.UseCases.UserIdentity.Login;
using Application.UseCases.UserIdentity.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        [HttpPost("signin")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginQuery query)
        {
	        return Ok(await Mediator.Send(query));
        }

		[HttpPost("signup")]
		public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationCommand command)
		{
			return Ok(await Mediator.Send(command));
		}
    }
}
