namespace Domain
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
	}
}