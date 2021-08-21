using System;
using System.Threading.Tasks;

namespace Domain
{
	public interface IUnitOfWork : IDisposable
	{
		Task Save();

		Task Apply();
		Task Cancel();
	}
}