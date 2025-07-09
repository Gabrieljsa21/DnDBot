using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.FichaPersonagemConfig
{
    /// <summary>
    /// Configuração da entidade FichaPersonagemProficiencia para o Entity Framework Core.
    /// Define a chave composta e os relacionamentos.
    /// </summary>
    public class FichaPersonagemProficienciaConfiguration : IEntityTypeConfiguration<FichaPersonagemProficiencia>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemProficiencia> builder)
        {
            // Define chave composta
            builder.HasKey(fp => new { fp.FichaPersonagemId, fp.ProficienciaId });

            // Relacionamento com FichaPersonagem
            builder.HasOne(fp => fp.FichaPersonagem)
                   .WithMany(p => p.Proficiencias)
                   .HasForeignKey(fp => fp.FichaPersonagemId);

            // Relacionamento com Proficiencia
            builder.HasOne(fp => fp.Proficiencia)
                   .WithMany()
                   .HasForeignKey(fp => fp.ProficienciaId);
        }
    }
}
