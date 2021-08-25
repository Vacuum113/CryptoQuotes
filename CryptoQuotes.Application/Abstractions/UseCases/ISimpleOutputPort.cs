using System.Threading.Tasks;

namespace Application.Abstractions.UseCases
{
    public interface ISimpleOutputPort : IAsyncOutputPort
    {
        Task NotUnauthorized();
    }
}