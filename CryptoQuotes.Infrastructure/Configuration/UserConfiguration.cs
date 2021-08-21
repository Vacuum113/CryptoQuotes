using CryptoQuotes.Infrastructure.Identity;
using Domain.Entities.AppUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoQuotes.Infrastructure.Configuration
{
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder
				.HasOne(u => u.IdentityUser as IdentityAppUser)
				.WithOne()
				.HasForeignKey<User>(u => u.IdentityUserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}