using Domain.Abstractions.Factory;

namespace Domain.Entities.IdentityAppUser
{
    public class IdentityAppUserFactoryEntity : IFactoryEntity
    {
        public IdentityAppUserFactoryEntity(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}