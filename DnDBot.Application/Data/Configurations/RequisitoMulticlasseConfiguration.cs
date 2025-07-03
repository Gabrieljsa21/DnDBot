using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Configuração da entidade RequisitoMulticlasse para o Entity Framework Core.
/// Define o mapeamento da classe RequisitoMulticlasse para a tabela no banco de dados,
/// incluindo a chave primária composta e relacionamento com a entidade Classe.
/// </summary>
public class RequisitoMulticlasseConfiguration : IEntityTypeConfiguration<RequisitoMulticlasse>
{
    /// <summary>
    /// Configura a entidade RequisitoMulticlasse.
    /// </summary>
    /// <param name="entity">Construtor para configuração da entidade RequisitoMulticlasse.</param>
    public void Configure(EntityTypeBuilder<RequisitoMulticlasse> entity)
    {
        // Define chave primária composta pelas propriedades ClasseId e Atributo
        entity.HasKey(r => new { r.ClasseId, r.Atributo });

        // Configura o relacionamento muitos-para-um com Classe
        // Um RequisitoMulticlasse pertence a uma Classe,
        // que possui uma coleção de RequisitosParaMulticlasseEntities
        entity.HasOne(r => r.Classe)
              .WithMany(c => c.RequisitosParaMulticlasseEntities)
              .HasForeignKey(r => r.ClasseId);
    }
}
