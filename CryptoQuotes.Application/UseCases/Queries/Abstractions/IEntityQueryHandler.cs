using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions;
using MediatR;

namespace Application.UseCases.Queries.Abstractions
{
	public interface IGetManyQueryHandler<TFilter, TResponseEntity>  : IQueryHandler<EntityRequest<TFilter>, GetManyResponse<TResponseEntity>>
	{
	}

	public interface IQueryHandler<in TRequest, TResponse>
	{
		Task<TResponse> Handle(TRequest request);
	}
}