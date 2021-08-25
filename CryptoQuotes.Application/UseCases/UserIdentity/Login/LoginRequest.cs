using Application.Abstractions.UseCases;

namespace Application.UseCases.UserIdentity.Login
{
    public record LoginRequest : IRequest
	{
		public string Login { get; init; }

		public string Password { get; init; }
	}
}
