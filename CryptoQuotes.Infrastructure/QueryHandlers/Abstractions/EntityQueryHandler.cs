using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Queries;
using AutoMapper;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CryptoQuotes.Infrastructure.QueryHandlers.Abstractions
{
	public abstract class EntityQueryHandlerBase<TRequest, TResponse, TEntity>: IGetManyQueryHandler<TRequest, TResponse>
		where TRequest : class
		where TResponse : class
		where TEntity: class, IEntity<int>
	{
		protected readonly DataContext Context;
		protected readonly IMapper Mapper;

		internal EntityQueryHandlerBase(DataContext context, IMapper mapper)
		{
			Context = context;
			Mapper = mapper;
		}
		
		public virtual async Task<GetManyResponse<TResponse>> Handle(EntityRequest<TRequest> request)
		{
			var query = GetQuery();
			
			query = Filter(query, request);
			query = Sort(query, request);
			
			var count = await query.CountAsync();
			
			query = Paginate(query, request);
			
			var result = await query.ToListAsync();
			
			var mappedEntities = result
				.Select(e => MapEntity(e))
				.ToList();

			return new GetManyResponse<TResponse>(count, mappedEntities);
		}

		public abstract TResponse MapEntity(TEntity entity);

		public virtual IQueryable<TEntity> Sort(IQueryable<TEntity> query, EntityRequest<TRequest> request)
		{
			if (request.Order != null)
			{
				if (request.OrderBy != null)
					query = request.OrderBy == SortOrder.Asc ? query.OrderBy(request.Order) : query.OrderByDesc(request.Order);
				else
					query = query.OrderBy(request.Order);	
			}
			return query;
		}
		
		public virtual IQueryable<TEntity> Paginate(IQueryable<TEntity> query, EntityRequest<TRequest> request)
		{
			if (request.Start.HasValue && request.End.HasValue && request.Start >= 0 && request.End >= 0)
				return query.Skip(request.Start.Value).Take(request.End.Value - request.Start.Value);
			
			return query;
		}

		public virtual IQueryable<TEntity> GetQuery()
			=> Context.Set<TEntity>();

		public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> query, EntityRequest<TRequest> request) 
			=> query;
	}
}