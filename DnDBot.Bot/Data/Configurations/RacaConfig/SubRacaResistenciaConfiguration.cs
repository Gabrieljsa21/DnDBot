using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.RacaConfig
{
    /// <summary>
    /// Configuração da entidade SubRacaResistencia para o Entity Framework Core.
    /// Define o mapeamento da relação muitos-para-muitos entre SubRaça e Resistência.
    /// </summary>
    public class SubRacaResistenciaConfiguration : IEntityTypeConfiguration<SubRacaResistencia>
    {
        /// <summary>
        /// Configura a entidade SubRacaResistencia.
        /// </summary>
        /// <param name="builder">Construtor para configuração da entidade SubRacaResistencia.</param>
        public void Configure(EntityTypeBuilder<SubRacaResistencia> builder)
        {
            // Chave composta
            builder.HasKey(x => new { x.SubRacaId, x.ResistenciaId });

            // Relacionamento com SubRaca
            builder.HasOne(x => x.SubRaca)
                   .WithMany(sr => sr.Resistencias)
                   .HasForeignKey(x => x.SubRacaId);

            // Relacionamento com Resistencia
            builder.HasOne(x => x.Resistencia)
                   .WithMany()
                   .HasForeignKey(x => x.ResistenciaId);
        }
    }
}
