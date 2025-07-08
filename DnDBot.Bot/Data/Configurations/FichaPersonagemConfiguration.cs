using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade FichaPersonagem para o Entity Framework Core.
    /// Define o mapeamento da classe FichaPersonagem para a tabela no banco de dados.
    /// </summary>
    public class FichaPersonagemConfiguration : IEntityTypeConfiguration<FichaPersonagem>
    {
        /// <summary>
        /// Configura a entidade FichaPersonagem.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade FichaPersonagem.</param>
        public void Configure(EntityTypeBuilder<FichaPersonagem> entity)
        {
            // Define a propriedade Id como chave primária da tabela FichaPersonagem
            entity.HasKey(f => f.Id);

            // Configura a propriedade Nome como obrigatória e com tamanho máximo de 100 caracteres
            entity.Property(f => f.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Configura a propriedade BolsaDeMoedas como uma entidade própria (Owned Entity)
            entity.OwnsOne(f => f.BolsaDeMoedas, bolsa =>
            {
                bolsa.ToTable("BolsasDeMoedas");
                bolsa.OwnsMany(b => b.Moedas, moedas =>
                {
                    moedas.ToTable("Moedas");
                    moedas.WithOwner().HasForeignKey("FichaPersonagemId");
                    moedas.HasKey("FichaPersonagemId", "Tipo");

                    moedas.Property(m => m.Tipo).HasConversion<string>().IsRequired();
                    moedas.Property(m => m.Quantidade).IsRequired();
                });
            });

        }
    }
}
