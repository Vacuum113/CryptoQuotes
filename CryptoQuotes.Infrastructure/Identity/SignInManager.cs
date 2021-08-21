using System.Threading.Tasks;
using Application.Identity;
using Microsoft.AspNetCore.Identity;

namespace CryptoQuotes.Infrastructure.Identity
{
    internal class SignInManager : ISignInManager
    {
        private readonly SignInManager<IdentityAppUser> _signInManager;

        public SignInManager(SignInManager<IdentityAppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> SignIn(string username, string password)
        {
            return (await _signInManager.PasswordSignInAsync(username, password, true, true)).Succeeded;
        }
    }
}