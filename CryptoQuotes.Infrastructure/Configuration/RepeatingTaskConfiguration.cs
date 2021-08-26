using CryptoQuotes.Background.Entities;
using CryptoQuotes.Background.Entities.RepeatingTask;
using Domain.Entities.Cryptocurrency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoQuotes.Infrastructure.Configuration
{
	internal class RepeatingTaskConfiguration : IEntityTypeConfiguration<RepeatingTask>
	{
		public void Configure(EntityTypeBuilder<RepeatingTask> builder)
		{
			builder.HasKey(u => u.Id);
		}
	}
}