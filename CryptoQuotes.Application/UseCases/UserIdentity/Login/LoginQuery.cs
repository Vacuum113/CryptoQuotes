using Application.UseCases.User;
using MediatR;

namespace Application.UseCases.UserIdentity.Login
{
    public record LoginQuery : IRequest<UserIdentityModel>
	{
		public string Email { get; init; }

		public string Password { get; init; }
	}
}
