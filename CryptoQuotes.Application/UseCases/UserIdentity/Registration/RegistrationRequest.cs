using Application.Abstractions.UseCases;

namespace Application.UseCases.UserIdentity.Registration
{
	public record RegistrationRequest : IRequest
	{
		public string Login { get; init; }
		public string Password { get; init; }
		
		public string RepeatedPassword { get; init; }
	}
}