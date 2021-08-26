using System;

namespace Domain.Abstractions
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}