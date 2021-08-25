using System.Threading.Tasks;

namespace Application.Abstractions.UseCases
{
    public interface IAsyncRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task Execute(TRequest command);
    }
}