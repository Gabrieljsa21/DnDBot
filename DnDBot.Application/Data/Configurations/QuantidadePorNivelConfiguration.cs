using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Ficha.ClasseAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade QuantidadePorNivel para o Entity Framework Core.
    /// Define o mapeamento da classe QuantidadePorNivel para a tabela no banco de dados,
    /// incluindo a chave primária e relacionamento com a entidade Classe.
    /// </summary>
    public class QuantidadePorNivelConfiguration : IEntityTypeConfiguration<QuantidadePorNivel>
    {
        /// <summary>
        /// Configura a entidade QuantidadePorNivel.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade QuantidadePorNivel.</param>
        public void Configure(EntityTypeBuilder<QuantidadePorNivel> entity)
        {
            // Define a propriedade Id como chave primária da tabela QuantidadePorNivel
            entity.HasKey(q => q.Id);

            // Configura o relacionamento muitos-para-um com Classe
            // Cada QuantidadePorNivel pertence a uma Classe
            // A Classe possui uma coleção TruquesConhecidosPorNivelList com muitos QuantidadePorNivel
            entity.HasOne(q => q.Classe)
                  .WithMany(c => c.TruquesConhecidosPorNivelList)
                  .HasForeignKey(q => q.ClasseId);
        }
    }
}
