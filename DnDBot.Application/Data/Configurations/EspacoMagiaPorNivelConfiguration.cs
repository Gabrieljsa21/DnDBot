using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade EspacoMagiaPorNivel para o Entity Framework Core.
    /// Define o mapeamento da classe EspacoMagiaPorNivel para a tabela no banco de dados.
    /// </summary>
    public class EspacoMagiaPorNivelConfiguration : IEntityTypeConfiguration<EspacoMagiaPorNivel>
    {
        /// <summary>
        /// Configura a entidade EspacoMagiaPorNivel.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade EspacoMagiaPorNivel.</param>
        public void Configure(EntityTypeBuilder<EspacoMagiaPorNivel> entity)
        {
            // Define a propriedade Id como chave primária da tabela EspacoMagiaPorNivel
            entity.HasKey(e => e.Id);
        }
    }
}
