using DnDBot.Bot.Models;
using DnDBot.Bot.Models.ItensInventario;
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
    public void Configure(EntityTypeBuilder<Ferramenta> builder)
    {
        // NÃO DEFINA builder.HasKey()

        builder.Property(f => f.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(f => f.Descricao)
            .HasMaxLength(1000);

        builder.Property(f => f.RequerProficiencia);

        builder.HasMany(f => f.PericiasAssociadas)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "FerramentaPericia",
                j => j.HasOne<Pericia>()
                      .WithMany()
                      .HasForeignKey("PericiaId"),
                j => j.HasOne<Ferramenta>()
                      .WithMany()
                      .HasForeignKey("FerramentaId"));

        builder.Ignore(f => f.Tags); // Ou mapeie com uma tabela se quiser usar FerramentaTag
    }
}

