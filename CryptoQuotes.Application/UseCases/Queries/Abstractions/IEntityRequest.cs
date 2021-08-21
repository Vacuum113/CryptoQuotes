using MediatR;

namespace Application.UseCases.Queries.Abstractions
{
	public interface IEntityRequest<TModel> : IRequest<TModel> where TModel : class
	{
		
	}
}