﻿using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.User;
using Domain.Entities.IdentityAppUser;
using MediatR;

namespace Application.UseCases.UserIdentity.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserIdentityModel>
	{
		private readonly IJwtGenerator _jwtGenerator;
		private readonly IIdentityAppUserRepository _identityAppUserRepository;
		private readonly IIdentityAppUserFactory _userFactory;

		public RegistrationHandler(IJwtGenerator jwtGenerator, IIdentityAppUserFactory userFactory, IIdentityAppUserRepository identityAppUserRepository)
		{
			_jwtGenerator = jwtGenerator;
			_userFactory = userFactory;
			_identityAppUserRepository = identityAppUserRepository;
		}

		public async Task<UserIdentityModel> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			if ((await _identityAppUserRepository.GetByFilter(x => x.Email == request.Login)).Any())
				throw new RestException(HttpStatusCode.BadRequest, "Email already exist");

			var user = _userFactory.Create(new IdentityAppUserFactoryEntity(request.Login, request.Password));

			if (user == null)
				throw new RestException(HttpStatusCode.BadRequest,"Registration failed.");
			
			return new UserIdentityModel(_jwtGenerator.CreateToken(user), user.UserName);
		}
	}
}