using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Abstractions.UseCases;
using Application.Identity;
using Application.Interfaces;
using Domain.Entities.IdentityAppUser;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.UserIdentity.Registration
{
	[AllowAnonymous]
	public interface IRegistrationUseCase : IAsyncRequestHandler<RegistrationRequest>
	{
	}
	
	public class RegistrationUseCase : IRegistrationUseCase
	{
		private readonly IJwtGenerator _jwtGenerator;
		private readonly IIdentityAppUserRepository _identityAppUserRepository;
		private readonly IIdentityAppUserFactory _userFactory;
		private readonly ISimpleOutputPort _outputPort;

		public RegistrationUseCase(IJwtGenerator jwtGenerator, IIdentityAppUserFactory userFactory, IIdentityAppUserRepository identityAppUserRepository, ISimpleOutputPort outputPort)
		{
			_jwtGenerator = jwtGenerator;
			_userFactory = userFactory;
			_identityAppUserRepository = identityAppUserRepository;
			_outputPort = outputPort;
		}

		public async Task Execute(RegistrationRequest request)
		{
			if (!request.Password.Equals(request.RepeatedPassword))
			{
				await _outputPort.Error("Password and repeated password not equals");
				return;
			}
			
			if ((await _identityAppUserRepository.GetByFilter(x => x.Email == request.Login)).Any())
			{
				await _outputPort.Error("Email already exist");
				return;
			}

			var user = _userFactory.Create(new IdentityAppUserFactoryEntity(request.Login, request.Password));

			if (user == null)
			{
				await _outputPort.Error("Registration failed.");
				return;
			}
			
			await _outputPort.Output(new UserIdentityModel(_jwtGenerator.CreateToken(user), user.UserName));
		}
	}
}