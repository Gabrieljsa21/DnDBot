using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Inventario para o Entity Framework Core.
    /// Define o mapeamento da relação com os itens equipados.
    /// </summary>
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        /// <summary>
        /// Configura a entidade Inventario.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade Inventario.</param>
        public void Configure(EntityTypeBuilder<Inventario> entity)
        {
            // Chave primária
            entity.HasKey(i => i.Id);

            // Relacionamento 1:N com Equipados
            entity.HasMany(i => i.Equipados)
                  .WithOne(e => e.Inventario)
                  .HasForeignKey(e => e.InventarioId);
        }
    }
}
