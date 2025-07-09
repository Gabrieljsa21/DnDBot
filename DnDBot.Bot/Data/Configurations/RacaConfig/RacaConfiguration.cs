using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.RacaConfig
{
    public class RacaConfiguration : IEntityTypeConfiguration<Raca>
    {
        public void Configure(EntityTypeBuilder<Raca> entity)
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Configura relacionamento com RacaTags (many-to-many via entidade)
            entity.HasMany(r => r.RacaTags)
                .WithOne(rt => rt.Raca)
                .HasForeignKey(rt => rt.RacaId);

        }
    }
}
