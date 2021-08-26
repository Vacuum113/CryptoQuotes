using System;
using Domain.Abstractions;

namespace CryptoQuotes.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}