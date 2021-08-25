using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Presenters;
using Application.Identity;

namespace Api.Identity
{
    public class AuthorizationMiddleware
    {
        private readonly IIdentityService _identityService;
        private readonly SimpleOutputPresenter _simpleOutputPresenter;

        public AuthorizationMiddleware(IIdentityService identityService, SimpleOutputPresenter simpleOutputPresenter)
        {
            _identityService = identityService;
            _simpleOutputPresenter = simpleOutputPresenter;
        }

        public async Task<bool> Process<THandler, TCommand>(THandler handler, TCommand input)
        {
            var authorizeAttribute = GetAttribute<THandler, AllowAnonymousAttribute>(handler);
            if (authorizeAttribute != null)
                return true;

            var user = await _identityService.GetCurrentUser();
            if (user != null) 
                return true;
            
            await _simpleOutputPresenter.NotUnauthorized();
            return false;
        }

        private TAttribute GetAttribute<THandler, TAttribute>(THandler handler)
            where TAttribute : Attribute
        {
            var attribute = Attribute.GetCustomAttribute(typeof(THandler), typeof(TAttribute)) ??
                            Attribute.GetCustomAttribute(handler.GetType(), typeof(TAttribute));

            return attribute as TAttribute;
        }
    }
}