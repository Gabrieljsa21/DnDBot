using DnDBot.Application.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade ClassePericia para o Entity Framework Core.
    /// Representa a tabela intermediária para o relacionamento muitos-para-muitos entre Classe e Pericia.
    /// </summary>
    public class ClassePericiaConfiguration : IEntityTypeConfiguration<ClassePericia>
    {
        /// <summary>
        /// Configura a entidade ClassePericia.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade ClassePericia.</param>
        public void Configure(EntityTypeBuilder<ClassePericia> entity)
        {
            // Define chave primária composta pelos campos ClasseId e PericiaId
            entity.HasKey(cp => new { cp.ClasseId, cp.PericiaId });

            // Configura o relacionamento muitos-para-um com Classe
            // Muitas entradas ClassePericia podem estar relacionadas a uma Classe
            entity.HasOne(cp => cp.Classe)
                  .WithMany()
                  .HasForeignKey(cp => cp.ClasseId);

            // Configura o relacionamento muitos-para-um com Pericia
            // Muitas entradas ClassePericia podem estar relacionadas a uma Pericia
            entity.HasOne(cp => cp.Pericia)
                  .WithMany()
                  .HasForeignKey(cp => cp.PericiaId);
        }
    }
}
