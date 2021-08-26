using Domain.Abstractions;
using Domain.Abstractions.Entity;

namespace Domain.Entities.IdentityAppUser
{
    public interface IIdentityUser : IEntity<int>
    {
        public int Id { get; }
        public string Email { get; }
        public string UserName { get; }
    }
}