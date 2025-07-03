using DnDBot.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Configuração da entidade Equipamento para o Entity Framework Core.
/// Define como a classe Equipamento será mapeada para a tabela no banco de dados.
/// </summary>
public class EquipamentoConfiguration : IEntityTypeConfiguration<Equipamento>
{
    /// <summary>
    /// Configura a entidade Equipamento.
    /// </summary>
    /// <param name="entity">Construtor para configuração da entidade Equipamento.</param>
    public void Configure(EntityTypeBuilder<Equipamento> entity)
    {
        // Define a propriedade Id como chave primária da tabela Equipamento
        entity.HasKey(e => e.Id);

        // Configura a propriedade Nome como obrigatória (não pode ser nula)
        entity.Property(e => e.Nome).IsRequired();

        // Configura a propriedade Quantidade como obrigatória
        entity.Property(e => e.Quantidade).IsRequired();
    }
}
