using Domain.Entities.Cryptocurrency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoQuotes.Infrastructure.Configuration
{
	internal class CryptocurrencyConfiguration : IEntityTypeConfiguration<Cryptocurrency>
	{
		public void Configure(EntityTypeBuilder<Cryptocurrency> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(c => c.Name).IsRequired();
			builder.Property(c => c.CoinMarketCapId).IsRequired();
			builder.Property(c => c.Symbol).IsRequired();

			builder
				.HasMany(c => c.CryptoQuote)
				.WithOne(cq => cq.Cryptocurrency)
				.HasForeignKey(c => c.CryptocurrencyId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}