using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class CaracteristicaPorNivelConfiguration : IEntityTypeConfiguration<CaracteristicaPorNivel>
    {
        public void Configure(EntityTypeBuilder<CaracteristicaPorNivel> entity)
        {
            entity.HasKey(c => new { c.ClasseId, c.Nivel, c.CaracteristicaId });

            entity.HasOne(c => c.Classe)
                  .WithMany()
                  .HasForeignKey(c => c.ClasseId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Caracteristica)
                  .WithMany()
                  .HasForeignKey(c => c.CaracteristicaId)
                  .IsRequired(true)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Antecedente)
                  .WithMany()
                  .HasForeignKey(c => c.AntecedenteId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Raca)
                  .WithMany()
                  .HasForeignKey(c => c.RacaId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
