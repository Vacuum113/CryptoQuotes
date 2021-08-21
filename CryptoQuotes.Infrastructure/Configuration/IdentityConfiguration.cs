using CryptoQuotes.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoQuotes.Infrastructure.Configuration
{
	internal class IdentityConfiguration : IEntityTypeConfiguration<IdentityAppUser>
	{
		public void Configure(EntityTypeBuilder<IdentityAppUser> builder)
		{
			builder.HasKey(u => u.Id);
		}
	}
}