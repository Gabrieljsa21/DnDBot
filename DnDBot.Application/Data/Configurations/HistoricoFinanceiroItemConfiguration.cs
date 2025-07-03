using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade HistoricoFinanceiroItem para o Entity Framework Core.
    /// Define o mapeamento da classe HistoricoFinanceiroItem para a tabela no banco de dados.
    /// </summary>
    public class HistoricoFinanceiroItemConfiguration : IEntityTypeConfiguration<HistoricoFinanceiroItem>
    {
        /// <summary>
        /// Configura a entidade HistoricoFinanceiroItem.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade HistoricoFinanceiroItem.</param>
        public void Configure(EntityTypeBuilder<HistoricoFinanceiroItem> entity)
        {
            // Define a propriedade Id como chave primária da tabela HistoricoFinanceiroItem
            entity.HasKey(h => h.Id);

            // Configura a propriedade Valor (antes da transação) como uma entidade própria (Owned Entity)
            entity.OwnsOne(h => h.Valor, valor =>
            {
                // Dentro de Valor, configura a coleção Moedas como entidade própria
                valor.OwnsMany(v => v.Moedas, moedas =>
                {
                    // Define chave estrangeira para relacionar moedas à entidade principal
                    moedas.WithOwner().HasForeignKey("HistoricoFinanceiroItemId");

                    // Define a propriedade HistoricoFinanceiroItemId na tabela de moedas
                    moedas.Property<int>("HistoricoFinanceiroItemId");

                    // Define chave primária para cada moeda da coleção
                    moedas.Property<int>("Id");
                    moedas.HasKey("Id");

                    // Configura a propriedade Tipo, convertendo enum para string e tornando obrigatório
                    moedas.Property(m => m.Tipo)
                          .HasConversion<string>()
                          .IsRequired();

                    // Configura a propriedade Quantidade como obrigatória
                    moedas.Property(m => m.Quantidade)
                          .IsRequired();

                    // Define o nome da tabela que armazenará as moedas associadas ao Valor
                    moedas.ToTable("HistoricoFinanceiroItem_Valor_Moedas");
                });
            });

            // Configura a propriedade SaldoApos (saldo após a transação) como entidade própria (Owned Entity)
            entity.OwnsOne(h => h.SaldoApos, saldo =>
            {
                // Dentro de SaldoApos, configura a coleção Moedas como entidade própria
                saldo.OwnsMany(s => s.Moedas, moedas =>
                {
                    // Define chave estrangeira para relacionar moedas ao saldo após a transação
                    moedas.WithOwner().HasForeignKey("HistoricoFinanceiroItemId_Saldo");

                    // Define a propriedade HistoricoFinanceiroItemId_Saldo na tabela de moedas
                    moedas.Property<int>("HistoricoFinanceiroItemId_Saldo");

                    // Define chave primária para cada moeda da coleção
                    moedas.Property<int>("Id");
                    moedas.HasKey("Id");

                    // Configura a propriedade Tipo, convertendo enum para string e tornando obrigatório
                    moedas.Property(m => m.Tipo)
                          .HasConversion<string>()
                          .IsRequired();

                    // Configura a propriedade Quantidade como obrigatória
                    moedas.Property(m => m.Quantidade)
                          .IsRequired();

                    // Define o nome da tabela que armazenará as moedas associadas ao SaldoApos
                    moedas.ToTable("HistoricoFinanceiroItem_SaldoApos_Moedas");
                });
            });
        }
    }
}
