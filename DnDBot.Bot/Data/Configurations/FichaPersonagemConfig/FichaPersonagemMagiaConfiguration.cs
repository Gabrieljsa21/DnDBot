using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.FichaPersonagemConfig
{
    /// <summary>
    /// Configuração da entidade FichaPersonagemMagia para o Entity Framework Core.
    /// Define a chave composta e os relacionamentos com FichaPersonagem e Magia.
    /// </summary>
    public class FichaPersonagemMagiaConfiguration : IEntityTypeConfiguration<FichaPersonagemMagia>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemMagia> builder)
        {
            // Chave composta
            builder.HasKey(fm => new { fm.FichaPersonagemId, fm.MagiaId });

            // Relacionamento com FichaPersonagem
            builder.HasOne(fm => fm.FichaPersonagem)
                   .WithMany(fp => fp.MagiasRaciais)
                   .HasForeignKey(fm => fm.FichaPersonagemId);

            // Relacionamento com Magia
            builder.HasOne(fm => fm.Magia)
                   .WithMany()
                   .HasForeignKey(fm => fm.MagiaId);
        }
    }
}
