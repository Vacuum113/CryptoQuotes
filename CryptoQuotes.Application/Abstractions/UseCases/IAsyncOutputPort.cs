using System.Threading.Tasks;

namespace Application.Abstractions.UseCases
{
    public interface IAsyncOutputPort
    {
        Task Output(object output);
        Task Error(string error);
    }
}