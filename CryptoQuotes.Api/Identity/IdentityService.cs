using System.Security.Claims;
using System.Threading.Tasks;
using Application.Identity;
using Domain.Entities.AppUser;
using Domain.Entities.IdentityAppUser;
using Microsoft.AspNetCore.Http;

namespace Api.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly IIdentityAppUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IIdentityAppUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IIdentityUser> GetCurrentUser()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
                return null;

            if (!int.TryParse(id, out var intId))
                return null;

            return await _userRepository.FindByIdentityId(intId);
        }
    }
}
