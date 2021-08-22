using System.Threading.Tasks;
using Application.UseCases.Queries.CryptocurrencyQuery;
using Application.UseCases.User;
using Application.UseCases.UserIdentity;
using Application.UseCases.UserIdentity.Login;
using FluentMediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class CryptocurrencyController : QueryHandlerController<CryptocurrencyRequest, CryptocurrencyResponse>
    {
        public CryptocurrencyController(IMediator mediator) : base(mediator)
        {
        }
    }
}
