using System.Collections.Generic;

namespace Application.UseCases.Queries.Abstractions
{
	public class EntityRequest<TModel>: IEntityRequest<IEnumerable<TModel>> where TModel : class
	{
		
	}
}