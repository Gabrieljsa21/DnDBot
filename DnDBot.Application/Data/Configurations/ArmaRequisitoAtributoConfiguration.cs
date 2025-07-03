using DnDBot.Application.Models.Ficha.ArmaAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade ArmaRequisitoAtributo para o Entity Framework Core.
    /// Define como a classe ArmaRequisitoAtributo será mapeada para a tabela no banco de dados.
    /// </summary>
    public class ArmaRequisitoAtributoConfiguration : IEntityTypeConfiguration<ArmaRequisitoAtributo>
    {
        /// <summary>
        /// Configura a entidade ArmaRequisitoAtributo.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade ArmaRequisitoAtributo.</param>
        public void Configure(EntityTypeBuilder<ArmaRequisitoAtributo> entity)
        {
            // Define a propriedade Id como chave primária da tabela ArmaRequisitoAtributo
            entity.HasKey(e => e.Id);

            // Configura a propriedade Atributo como obrigatória
            entity.Property(e => e.Atributo).IsRequired();

            // Configura a propriedade Valor como obrigatória
            entity.Property(e => e.Valor).IsRequired();

            // Configura o relacionamento muitos-para-um entre ArmaRequisitoAtributo e Arma
            // Uma arma pode ter vários requisitos de atributo
            entity.HasOne(e => e.Arma)
                  .WithMany(a => a.RequisitosAtributos)
                  .HasForeignKey(e => e.ArmaId);
        }
    }
}
