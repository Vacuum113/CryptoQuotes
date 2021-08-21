using Domain.Entities.AppUser;
using Domain.Entities.IdentityAppUser;
using Microsoft.AspNetCore.Identity;

namespace CryptoQuotes.Infrastructure.Identity
{
    public class IdentityAppUser : IdentityUser<int>, IIdentityUser
    {
    }
}