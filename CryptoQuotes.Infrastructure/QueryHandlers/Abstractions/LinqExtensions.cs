using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.QueryHandlers.Abstractions
{
    internal static class LinqExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            return query.OrderBy(e => EF.Property<object>(e, name));
        }

        public static IQueryable<T> OrderByDesc<T>(this IQueryable<T> query, string name)
        {
            return query.OrderByDescending(e => EF.Property<object>(e, name));
        }
    }
}