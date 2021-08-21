using Domain.Abstractions.Factory;

namespace Domain.Entities.IdentityAppUser
{
    public class IdentityAppUserFactoryEntity : IFactoryEntity
    {
        public IdentityAppUserFactoryEntity(string email, string userName, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}