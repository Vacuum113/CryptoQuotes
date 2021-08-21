using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;
using MediatR;

namespace Application.UseCases.Queries.Abstractions
{
	public interface IEntityQueryHandler<in TRequest, TDto, TEntity>: IRequestHandler<TRequest, IEnumerable<TDto>> 
		where TRequest : IRequest<IEnumerable<TDto>>
		where TEntity: class, IEntity<int>
	{
		TDto MapEntity(TEntity entity);

		IQueryable<TEntity> Sort(IQueryable<TEntity> query, TRequest request);

		IQueryable<TEntity> GetQuery();

		IQueryable<TEntity> Filter(IQueryable<TEntity> query, TRequest request);
	}
}