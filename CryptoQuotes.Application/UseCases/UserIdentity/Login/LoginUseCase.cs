using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.Identity;
using Application.Interfaces;
using Domain.Entities.IdentityAppUser;

namespace Application.UseCases.UserIdentity.Login
{
	[AllowAnonymousAttribute]
	public interface ILoginUseCase : IAsyncRequestHandler<LoginRequest>
	{
	}
	
    public class LoginUseCase : ILoginUseCase
	{
		private readonly IJwtGenerator _jwtGenerator;
		private readonly ISignInManager _signInManager;
		private readonly IIdentityAppUserRepository _identityAppUserRepository;
		private readonly ISimpleOutputPort _outputPort;
		public LoginUseCase(IJwtGenerator jwtGenerator, 
			ISignInManager signInManager, 
			IIdentityAppUserRepository identityAppUserRepository, 
			ISimpleOutputPort outputPort)
		{
			_jwtGenerator = jwtGenerator;
			_signInManager = signInManager;
			_identityAppUserRepository = identityAppUserRepository;
			_outputPort = outputPort;
		}

		public async Task Execute(LoginRequest request)
		{
			var user = await _identityAppUserRepository.FindByEmailAsync(request.Login);
			if (user == null)
			{
				await _outputPort.Error("User not found");
				return;
			}

			var result = await _signInManager.SignIn(user.UserName, request.Password);

			if (!result)
			{
				await _outputPort.Error("Wrong email or password");
				return;
			}

			await _outputPort.Output(new UserIdentityModel(_jwtGenerator.CreateToken(user), user.UserName));
		}
	}
}
