using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Queries.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using MediatR;
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