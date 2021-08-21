using System.Collections.Generic;
using Domain.Abstractions;
using Domain.Entities.IdentityAppUser;

namespace Domain.Entities.AppUser
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }

        public int IdentityUserId { get; set; }

        public IIdentityUser IdentityUser { get; set; }
    }
}