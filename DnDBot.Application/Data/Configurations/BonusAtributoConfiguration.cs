using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade BonusAtributo para o Entity Framework Core.
    /// Define como a classe BonusAtributo será mapeada para a tabela no banco de dados.
    /// </summary>
    public class BonusAtributoConfiguration : IEntityTypeConfiguration<BonusAtributo>
    {
        /// <summary>
        /// Configura a entidade BonusAtributo.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade BonusAtributo.</param>
        public void Configure(EntityTypeBuilder<BonusAtributo> entity)
        {
            // Define a propriedade Id como chave primária da tabela BonusAtributo
            entity.HasKey(b => b.Id);

            // Configura a propriedade Atributo como obrigatória (não pode ser nula)
            entity.Property(b => b.Atributo).IsRequired();
        }
    }
}
