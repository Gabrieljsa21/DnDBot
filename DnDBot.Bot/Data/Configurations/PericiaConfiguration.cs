using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Pericia para o Entity Framework Core.
    /// Define o mapeamento da classe Pericia para a tabela no banco de dados,
    /// incluindo propriedades, relacionamentos e conversões de dados.
    /// </summary>
    public class PericiaConfiguration : IEntityTypeConfiguration<Pericia>
    {
        public void Configure(EntityTypeBuilder<Pericia> entity)
        {

            entity.ConfigureEntidadeBase();

            // Chave primária
            entity.HasKey(p => p.Id);

            // Propriedades básicas
            entity.Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.Descricao)
                  .HasMaxLength(2000);

            entity.Property(p => p.AtributoBase)
                  .HasConversion<string>()
                  .IsRequired();

            // Ignora propriedades que não devem ser persistidas
            entity.Ignore(p => p.DificuldadeSugerida);

            // Um-para-muitos: Pericia -> Dificuldades
            entity.HasMany(p => p.Dificuldades)
                  .WithOne()
                  .HasForeignKey("PericiaId")
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
