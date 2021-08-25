using System.Threading.Tasks;

namespace Application.Abstractions.Queries
{
	public interface IGetManyQueryHandler<TFilter, TResponseEntity>  : IQueryHandler<EntityRequest<TFilter>, GetManyResponse<TResponseEntity>>
	{
	}

	public interface IQueryHandler<in TRequest, TResponse>
	{
		Task<TResponse> Handle(TRequest request);
	}
}