using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Ficha.ClasseAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade CaracteristicaPorNivel para o Entity Framework Core.
    /// Define o mapeamento da classe CaracteristicaPorNivel para a tabela no banco de dados.
    /// </summary>
    public class CaracteristicaPorNivelConfiguration : IEntityTypeConfiguration<CaracteristicaPorNivel>
    {
        /// <summary>
        /// Configura a entidade CaracteristicaPorNivel.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade CaracteristicaPorNivel.</param>
        public void Configure(EntityTypeBuilder<CaracteristicaPorNivel> entity)
        {
            // Define a chave primária composta pelos campos ClasseId, Nivel e CaracteristicaId
            entity.HasKey(c => new { c.ClasseId, c.Nivel, c.CaracteristicaId });

            // Configura o relacionamento muitos-para-um com Classe
            // Muitas CaracteristicaPorNivel podem estar relacionadas a uma Classe
            entity.HasOne(c => c.Classe)
                  .WithMany()
                  .HasForeignKey(c => c.ClasseId);

            // Configura o relacionamento muitos-para-um com Caracteristica
            // Muitas CaracteristicaPorNivel podem estar relacionadas a uma Caracteristica
            entity.HasOne(c => c.Caracteristica)
                  .WithMany()
                  .HasForeignKey(c => c.CaracteristicaId);
        }
    }
}
