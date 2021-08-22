using System.Collections.Generic;
using MediatR;

namespace Application.UseCases.Queries.Abstractions
{
	public class EntityRequest<TFilter>
	{
		public int? Start { get; set; }
		public int? End { get; set; }
		
		public string OrderBy { get; set; }
		
		public TFilter Filter { get; set; }
	}
}