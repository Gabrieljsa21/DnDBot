using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DnDBot.Application.Data.Configurations
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
                // Dentro da BolsaDeMoedas, configura a coleção Moedas como entidade própria
                bolsa.OwnsMany(b => b.Moedas, moedas =>
                {
                    // Define a chave estrangeira para associar Moedas ao FichaPersonagem
                    moedas.WithOwner().HasForeignKey("FichaPersonagemId");

                    // Define a propriedade "FichaPersonagemId" como chave estrangeira do tipo Guid
                    moedas.Property<Guid>("FichaPersonagemId");

                    // Define uma chave primária para cada item da coleção Moedas
                    moedas.Property<int>("Id");
                    moedas.HasKey("Id");

                    // Configura a propriedade Tipo do enum, convertendo para string, obrigatório
                    moedas.Property(m => m.Tipo)
                          .HasConversion<string>()
                          .IsRequired();

                    // Configura a propriedade Quantidade como obrigatória
                    moedas.Property(m => m.Quantidade)
                          .IsRequired();

                    // Define o nome da tabela para armazenar as moedas da bolsa
                    moedas.ToTable("FichaPersonagem_BolsaDeMoedas_Moedas");
                });
            });

            // Configura o relacionamento um-para-muitos com HistoricoFinanceiroItem
            // Um FichaPersonagem pode ter muitos itens no histórico financeiro
            entity.HasMany<HistoricoFinanceiroItem>()
                  .WithOne(h => h.FichaPersonagem)
                  .HasForeignKey(h => h.FichaPersonagemId);
        }
    }
}
