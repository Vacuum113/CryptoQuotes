using System.Collections.Generic;
using Domain.Abstractions;
using Domain.Abstractions.Entity;
using Domain.Entities.IdentityAppUser;

namespace Domain.Entities.AppUser
{
    public class User : IEntity<int>
    {
        internal User()
        {
            
        }
        
        public User(IIdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }

        public int Id { get; private set; }

        public int IdentityUserId { get; private set; }

        public IIdentityUser IdentityUser { get; private set; }
    }
}