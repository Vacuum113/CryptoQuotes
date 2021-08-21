using Application.UseCases.User;
using MediatR;

namespace Application.UseCases.UserIdentity.Login
{
    public class LoginQuery : IRequest<UserIdentityModel>
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
