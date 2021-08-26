using Application.UseCases.Queries.CryptocurrencyQuery;
using FluentMediator;
using Microsoft.AspNetCore.Authorization;

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
