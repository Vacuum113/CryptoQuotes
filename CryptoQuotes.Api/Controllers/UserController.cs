using System.Threading.Tasks;
using Application.UseCases.User;
using Application.UseCases.UserIdentity;
using Application.UseCases.UserIdentity.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginQuery query)
        {
	        return Json(await Mediator.Send(query));
        }

		// [HttpPost("registration")]
		// public async Task<IActionResult> RegistrationAsync(RegistrationCommand command)
		// {
		// 	return Json(await Mediator.Send(command));
		// }
    }
}
