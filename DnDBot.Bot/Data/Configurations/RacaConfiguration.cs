using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class RacaConfiguration : IEntityTypeConfiguration<Raca>
    {
        public void Configure(EntityTypeBuilder<Raca> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Configura relacionamento com RacaTags (many-to-many via entidade)
            builder.HasMany(r => r.RacaTags)
                .WithOne(rt => rt.Raca)
                .HasForeignKey(rt => rt.RacaId);

        }
    }
}
