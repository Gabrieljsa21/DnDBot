using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.FichaPersonagemConfig
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
            // Chave primária
            entity.HasKey(f => f.Id);

            // Propriedade Nome
            entity.Property(f => f.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Relacionamento 1:1 com Inventario
            entity.HasOne(f => f.Inventario)
                  .WithOne(i => i.FichaPersonagem)
                  .HasForeignKey<Inventario>(i => i.FichaPersonagemId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
