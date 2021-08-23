using Application.UseCases.User;
using MediatR;

namespace Application.UseCases.UserIdentity.Registration
{
	public record RegistrationCommand : IRequest<UserIdentityModel>
	{
		public string Login { get; init; }
		
		public string Password { get; init; }
	}
}