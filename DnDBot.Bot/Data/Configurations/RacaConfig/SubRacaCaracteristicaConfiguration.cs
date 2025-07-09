using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.RacaConfig
{
    public class SubRacaCaracteristicaConfiguration : IEntityTypeConfiguration<SubRacaCaracteristica>
    {
        public void Configure(EntityTypeBuilder<SubRacaCaracteristica> builder)
        {
            // Define chave composta
            builder.HasKey(sc => new { sc.SubRacaId, sc.CaracteristicaId });

            // Relacionamento com SubRaca
            builder.HasOne(sc => sc.SubRaca)
                   .WithMany(s => s.Caracteristicas)
                   .HasForeignKey(sc => sc.SubRacaId);

            // Relacionamento com Caracteristica
            builder.HasOne(sc => sc.Caracteristica)
                   .WithMany()
                   .HasForeignKey(sc => sc.CaracteristicaId);
        }
    }
}
