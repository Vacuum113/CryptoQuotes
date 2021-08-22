using System.Collections.Generic;
using Application.UseCases.Queries.Abstractions;
using Domain.Entities.Cryptocurrency;
using MediatR;

namespace Application.UseCases.Queries.CryptocurrencyQuery
{
    public class CryptocurrencyRequest
    {
        public string Name { get; set; }
    }
}