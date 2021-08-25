namespace Application.Abstractions.Queries
{
	public class EntityRequest<TFilter>
	{
		public int? Start { get; set; }
		public int? End { get; set; }
		
		public string Order { get; set; }
		public SortOrder? OrderBy { get; set; }
		public TFilter Filter { get; set; }
	}
	
	public enum SortOrder
	{
		Asc,
		Desc,
	}
}