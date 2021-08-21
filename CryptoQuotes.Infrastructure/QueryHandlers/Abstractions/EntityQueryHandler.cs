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
	public abstract class EntityQueryHandler<TRequest, TDto, TEntity>: IEntityQueryHandler<TRequest, TDto, TEntity>
		where TRequest : IRequest<IEnumerable<TDto>>
		where TEntity: class, IEntity<int>
	{
		protected readonly DataContext _context;
		protected readonly IMapper _mapper;

		internal EntityQueryHandler(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		
		public async Task<IEnumerable<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var query = GetQuery();
			query = Filter(query, request);
			query = Sort(query, request);
			var result = await query.ToListAsync(cancellationToken: cancellationToken);
			var mappedEntities = new List<TDto>();
			foreach (var entity in result)
			{
				mappedEntities.Add(MapEntity(entity));
			}

			return mappedEntities;
		}

		public TDto MapEntity(TEntity entity)
		{
			return _mapper.Map<TDto>(entity);
		}

		public IQueryable<TEntity> Sort(IQueryable<TEntity> query, TRequest request)
		{
			return query;
		}

		public IQueryable<TEntity> GetQuery()
			=> _context.Set<TEntity>();

		public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> query, TRequest request) 
			=> query;
	}
}