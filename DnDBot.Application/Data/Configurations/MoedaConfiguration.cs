using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Moeda para o Entity Framework Core.
    /// Define o mapeamento da classe Moeda para a tabela no banco de dados.
    /// </summary>
    public class MoedaConfiguration : IEntityTypeConfiguration<Moeda>
    {
        /// <summary>
        /// Configura a entidade Moeda.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade Moeda.</param>
        public void Configure(EntityTypeBuilder<Moeda> entity)
        {
            // Define a propriedade Id como chave primária da tabela Moeda
            entity.HasKey(e => e.Id);

            // Configura a propriedade Tipo como obrigatória e com tamanho máximo de 50 caracteres
            entity.Property(e => e.Tipo)
                  .IsRequired()
                  .HasMaxLength(50);

            // Configura a propriedade Quantidade como obrigatória
            entity.Property(e => e.Quantidade)
                  .IsRequired();
        }
    }
}
