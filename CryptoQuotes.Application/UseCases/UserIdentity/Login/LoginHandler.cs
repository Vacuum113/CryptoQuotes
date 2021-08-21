using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Identity;
using Application.Interfaces;
using Application.UseCases.User;
using Domain.Entities.IdentityAppUser;
using MediatR;

namespace Application.UseCases.UserIdentity.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, UserIdentityModel>
	{
		private readonly IJwtGenerator _jwtGenerator;
		private readonly ISignInManager _signInManager;
		private readonly IIdentityAppUserRepository _identityAppUserRepository;
		public LoginHandler(IJwtGenerator jwtGenerator, ISignInManager signInManager, IIdentityAppUserRepository identityAppUserRepository)
		{
			_jwtGenerator = jwtGenerator;
			_signInManager = signInManager;
			_identityAppUserRepository = identityAppUserRepository;
		}

		public async Task<UserIdentityModel> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var user = await _identityAppUserRepository.FindByEmailAsync(request.Email);
			if (user == null)
			{
				throw new RestException(HttpStatusCode.Unauthorized);
			}

			var result = await _signInManager.SignIn(user.UserName, request.Password);

			if (result)
			{
				return new UserIdentityModel(_jwtGenerator.CreateToken(user), user.UserName);
			}

			throw new RestException(HttpStatusCode.Unauthorized);
		}
	}
}
