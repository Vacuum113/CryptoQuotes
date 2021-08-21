using System.Threading.Tasks;

namespace Application.Identity
{
    public interface ISignInManager
    {
        Task<bool> SignIn(string username, string password);
    }
}