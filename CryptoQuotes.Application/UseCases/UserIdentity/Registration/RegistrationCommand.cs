using Application.UseCases.User;
using MediatR;

namespace Application.UseCases.UserIdentity.Registration
{
	public class RegistrationCommand : IRequest<UserIdentityModel>
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}