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
            entity.HasKey(h => h.Id);

            entity.OwnsOne(h => h.Valor, valor =>
            {
                valor.OwnsMany(v => v.Moedas, moedas =>
                {
                    moedas.WithOwner(); // o EF cuida da FK

                    moedas.HasKey("Id");

                    moedas.Property(m => m.Tipo)
                          .HasConversion<string>()
                          .IsRequired();

                    moedas.Property(m => m.Quantidade)
                          .IsRequired();

                    moedas.ToTable("HistoricoFinanceiroItem_Valor_Moedas");
                });
            });

            entity.OwnsOne(h => h.SaldoApos, saldo =>
            {
                saldo.OwnsMany(s => s.Moedas, moedas =>
                {
                    moedas.WithOwner(); // aqui também

                    moedas.HasKey("Id");

                    moedas.Property(m => m.Tipo)
                          .HasConversion<string>()
                          .IsRequired();

                    moedas.Property(m => m.Quantidade)
                          .IsRequired();

                    moedas.ToTable("HistoricoFinanceiroItem_SaldoApos_Moedas");
                });
            });
        }

    }
}
