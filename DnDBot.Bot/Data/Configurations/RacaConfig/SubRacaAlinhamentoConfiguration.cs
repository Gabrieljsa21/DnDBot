using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.RacaConfig
{
    /// <summary>
    /// Configuração da entidade SubRacaAlinhamento para o Entity Framework Core.
    /// Define o mapeamento da relação muitos-para-muitos entre SubRaça e Alinhamento.
    /// </summary>
    public class SubRacaAlinhamentoConfiguration : IEntityTypeConfiguration<SubRacaAlinhamento>
    {
        /// <summary>
        /// Configura a entidade SubRacaAlinhamento.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade SubRacaAlinhamento.</param>
        public void Configure(EntityTypeBuilder<SubRacaAlinhamento> entity)
       {

            // Chave composta
            entity.HasKey(sa => new { sa.SubRacaId, sa.AlinhamentoId });

            // Relacionamento com SubRaca
            entity.HasOne(sa => sa.SubRaca)
                   .WithMany(s => s.AlinhamentosComuns)
                   .HasForeignKey(sa => sa.SubRacaId);

            // Relacionamento com Alinhamento
            entity.HasOne(sa => sa.Alinhamento)
                   .WithMany()
                   .HasForeignKey(sa => sa.AlinhamentoId);
        }
    }
}
