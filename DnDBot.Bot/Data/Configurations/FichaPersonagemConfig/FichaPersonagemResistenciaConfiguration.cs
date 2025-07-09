using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.FichaPersonagemConfig
{
    /// <summary>
    /// Configuração da entidade FichaPersonagemResistencia para o Entity Framework Core.
    /// Define o mapeamento da relação muitos-para-muitos entre FichaPersonagem e Resistência.
    /// </summary>
    public class FichaPersonagemResistenciaConfiguration : IEntityTypeConfiguration<FichaPersonagemResistencia>
    {
        /// <summary>
        /// Configura a entidade FichaPersonagemResistencia.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade FichaPersonagemResistencia.</param>
        public void Configure(EntityTypeBuilder<FichaPersonagemResistencia> entity)
        {
            // Chave composta
            entity.HasKey(f => new { f.FichaPersonagemId, f.ResistenciaId });

            // Relacionamento com FichaPersonagem
            entity.HasOne(f => f.FichaPersonagem)
                   .WithMany(p => p.Resistencias)
                   .HasForeignKey(f => f.FichaPersonagemId);

            // Relacionamento com Resistencia
            entity.HasOne(f => f.Resistencia)
                   .WithMany()
                   .HasForeignKey(f => f.ResistenciaId);
        }
    }
}
