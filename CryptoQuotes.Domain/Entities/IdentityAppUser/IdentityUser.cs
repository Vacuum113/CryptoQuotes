using Domain.Abstractions;

namespace Domain.Entities.IdentityAppUser
{
    public interface IIdentityUser : IEntity<int>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}