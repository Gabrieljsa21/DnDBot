using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EscudoConfiguration : IEntityTypeConfiguration<Escudo>
{
    public void Configure(EntityTypeBuilder<Escudo> builder)
    {
        builder.Property(e => e.Nome)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(e => e.Descricao)
               .HasMaxLength(1000);

        builder.Property(e => e.BonusCA)
               .IsRequired()
               .HasDefaultValue(2);

        builder.Property(e => e.Fabricante)
               .HasMaxLength(200);

        // Propriedades especiais podem estar em uma tabela separada
        builder.HasMany<EscudoPropriedadeEspecial>()
               .WithOne()
               .HasForeignKey(p => p.EscudoId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class EscudoPropriedadeEspecialConfiguration : IEntityTypeConfiguration<EscudoPropriedadeEspecial>
{
    public void Configure(EntityTypeBuilder<EscudoPropriedadeEspecial> builder)
    {
        builder.HasKey(p => new { p.EscudoId, p.Propriedade });

        builder.Property(p => p.Propriedade)
               .HasMaxLength(200)
               .IsRequired();
    }
}

