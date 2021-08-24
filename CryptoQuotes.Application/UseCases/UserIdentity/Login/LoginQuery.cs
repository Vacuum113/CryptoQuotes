using Application.UseCases.User;
using MediatR;

namespace Application.UseCases.UserIdentity.Login
{
    public record LoginQuery : IRequest<UserIdentityModel>
	{
		public string Login { get; init; }

		public string Password { get; init; }
	}
}
