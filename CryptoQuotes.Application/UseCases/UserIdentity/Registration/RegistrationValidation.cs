using Application.Validators;
using FluentValidation;

namespace Application.UseCases.UserIdentity.Registration
{
	public class RegistrationValidation : AbstractValidator<RegistrationCommand>
	{
		public RegistrationValidation()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty().Password();
		}
	}
}