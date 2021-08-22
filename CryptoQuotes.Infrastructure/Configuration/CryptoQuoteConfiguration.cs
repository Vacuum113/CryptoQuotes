using Domain.Entities.CryptoQuote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoQuotes.Infrastructure.Configuration
{
	internal class CryptoQuoteConfiguration : IEntityTypeConfiguration<CryptoQuote>
	{
		public void Configure(EntityTypeBuilder<CryptoQuote> builder)
		{
			builder.HasKey(u => u.Id);
			
			builder.Property(c => c.Price).IsRequired();
			builder.Property(c => c.MarketCap).IsRequired();
			builder.Property(c => c.PercentChangeOneHour).IsRequired();
			builder.Property(c => c.PercentChangeTwentyFourHours).IsRequired();
			builder.Property(c => c.LastUpdated).IsRequired();
			builder.Property(c => c.IsActual).IsRequired().HasDefaultValue(false);
		}
	}
}