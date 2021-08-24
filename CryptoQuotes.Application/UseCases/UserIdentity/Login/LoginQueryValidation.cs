using FluentValidation;

namespace Application.UseCases.UserIdentity.Login
{
    public class LoginQueryValidation : AbstractValidator<LoginQuery>
	{
		public LoginQueryValidation()
		{
			RuleFor(x => x.Login).NotEmpty();

			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
