namespace Domain.Abstractions.Entity
{
	public interface IEntity<TKey>
	{
		TKey Id { get; }
	}
}