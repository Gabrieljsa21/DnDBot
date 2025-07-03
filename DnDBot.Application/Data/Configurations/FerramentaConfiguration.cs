using DnDBot.Application.Models;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Configuração da entidade Ferramenta para o Entity Framework Core.
/// Define como a classe Ferramenta será mapeada para a tabela no banco de dados.
/// </summary>
public class FerramentaConfiguration : IEntityTypeConfiguration<Ferramenta>
{
    /// <summary>
    /// Configura a entidade Ferramenta.
    /// </summary>
    /// <param name="builder">Construtor para configuração da entidade Ferramenta.</param>
    public void Configure(EntityTypeBuilder<Ferramenta> builder)
    {
        // Define a propriedade Id como chave primária da tabela Ferramenta
        builder.HasKey(f => f.Id);

        // Configura a propriedade Nome como obrigatória e com tamanho máximo de 150 caracteres
        builder.Property(f => f.Nome)
            .IsRequired()
            .HasMaxLength(150);

        // Configura a propriedade Descricao com tamanho máximo de 1000 caracteres (pode ser nula)
        builder.Property(f => f.Descricao)
            .HasMaxLength(1000);

        // Configura a propriedade Peso (sem restrição específica)
        builder.Property(f => f.Peso);

        // Configura a propriedade Custo com tipo decimal e precisão de 18,2 casas decimais
        builder.Property(f => f.Custo)
            .HasColumnType("decimal(18,2)");

        // Configura a propriedade EMagica (indica se a ferramenta é mágica)
        builder.Property(f => f.EMagica);

        // Configura a propriedade RequerProficiencia (indica se requer proficiência para uso)
        builder.Property(f => f.RequerProficiencia);

        // Configura relacionamento muitos-para-muitos entre Ferramenta e Pericia,
        // usando tabela intermediária "FerramentaPericia"
        builder.HasMany(f => f.PericiasAssociadas)
            .WithMany() // Ajuste caso exista propriedade de navegação em Pericia
            .UsingEntity<Dictionary<string, object>>(
                "FerramentaPericia",
                j => j.HasOne<Pericia>()
                      .WithMany()
                      .HasForeignKey("PericiaId"),
                j => j.HasOne<Ferramenta>()
                      .WithMany()
                      .HasForeignKey("FerramentaId"));

        // Configura a propriedade Tags como lista de strings,
        // armazenando-a no banco como uma string separada por ponto e vírgula
        builder.Property(f => f.Tags)
            .HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', System.StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}
