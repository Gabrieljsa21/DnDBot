using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Proficiencia para o Entity Framework Core.
    /// Define o mapeamento da classe Proficiencia para a tabela no banco de dados,
    /// incluindo propriedades, relacionamentos e conversões de dados.
    /// </summary>
    public class ProficienciaConfiguration : IEntityTypeConfiguration<Proficiencia>
    {
        public void Configure(EntityTypeBuilder<Proficiencia> entity)
        {
            // Aplicar configuração base (Id, CriadoPor, CriadoEm, etc)
            entity.ConfigureEntidadeBase();

            // Chave primária
            entity.HasKey(p => p.Id);

            // Nome é obrigatório e limitado a 100 caracteres
            entity.Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Descrição opcional, com limite maior
            entity.Property(p => p.Descricao)
                  .HasMaxLength(2000);

            // Enum Tipo como string
            entity.Property(p => p.Tipo)
                  .HasConversion<string>()
                  .IsRequired();

        }
    }
}
