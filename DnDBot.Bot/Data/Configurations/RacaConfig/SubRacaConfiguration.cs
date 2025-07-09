using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DnDBot.Bot.Data.Configurations.RacaConfig
{
    /// <summary>
    /// Configuração da entidade SubRaca para o Entity Framework Core.
    /// Define o mapeamento da classe SubRaca para a tabela no banco de dados,
    /// incluindo chaves, relacionamentos e tabelas intermediárias para relacionamentos muitos-para-muitos.
    /// </summary>
    public class SubRacaConfiguration : IEntityTypeConfiguration<SubRaca>
    {
        /// <summary>
        /// Configura a entidade SubRaca.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade SubRaca.</param>
        public void Configure(EntityTypeBuilder<SubRaca> entity)
        {
            // Define a propriedade Id como chave primária da tabela SubRaca
            entity.HasKey(sr => sr.Id);

            entity.Property(sr => sr.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(sr => sr.SubRacaTags)
                .WithOne(srt => srt.SubRaca)
                .HasForeignKey(srt => srt.SubRacaId);

            entity.HasMany(sr => sr.AlinhamentosComuns)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Idiomas)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Resistencias)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Proficiencias)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Caracteristicas)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.MagiasRaciais)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            // Configura o relacionamento muitos-para-um com Raca
            // Uma SubRaca pertence a uma Raca, que possui muitas SubRacas
            // Exclusão em cascata para as SubRacas ao deletar a Raca relacionada
            entity.HasOne(sr => sr.Raca)
                  .WithMany(r => r.SubRaca)
                  .HasForeignKey(sr => sr.RacaId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
