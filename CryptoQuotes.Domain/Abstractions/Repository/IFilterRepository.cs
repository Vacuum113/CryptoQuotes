using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Repository
{
    public interface IFilterRepository<T>
        where T: IEntity<int>
    {
        Task<IList<T>> GetByFilter(Expression<Func<T, bool>> conditions);
    }
}
