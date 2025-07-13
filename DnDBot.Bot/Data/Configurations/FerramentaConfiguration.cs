using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FerramentaConfiguration : IEntityTypeConfiguration<Ferramenta>
{
    public void Configure(EntityTypeBuilder<Ferramenta> builder)
    {
        builder.Property(f => f.Nome)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(f => f.Descricao)
               .HasMaxLength(1000);

        builder.Property(f => f.RequerProficiencia);

        // Relacionamento um-para-muitos entre Ferramenta e FerramentaPericia
        builder.HasMany(f => f.PericiasAssociadas)
               .WithOne(fp => fp.Ferramenta)
               .HasForeignKey(fp => fp.FerramentaId);

        builder.Ignore(f => f.Tags);

        // Relacionamento Ferramenta ↔ FerramentaTag
        builder.HasMany(f => f.FerramentaTags)
               .WithOne(ft => ft.Ferramenta)
               .HasForeignKey(ft => ft.FerramentaId);
    }
}

public class FerramentaPericiaConfiguration : IEntityTypeConfiguration<FerramentaPericia>
{
    public void Configure(EntityTypeBuilder<FerramentaPericia> builder)
    {
        builder.HasKey(fp => new { fp.FerramentaId, fp.PericiaId });

        builder.HasOne(fp => fp.Ferramenta)
               .WithMany(f => f.PericiasAssociadas)
               .HasForeignKey(fp => fp.FerramentaId);

        builder.HasOne(fp => fp.Pericia)
               .WithMany()
               .HasForeignKey(fp => fp.PericiaId);
    }
}

public class FerramentaTagConfiguration : IEntityTypeConfiguration<FerramentaTag>
{
    public void Configure(EntityTypeBuilder<FerramentaTag> builder)
    {
        builder.HasKey(ft => new { ft.FerramentaId, ft.Tag });

        builder.HasOne(ft => ft.Ferramenta)
               .WithMany(f => f.FerramentaTags)
               .HasForeignKey(ft => ft.FerramentaId);
    }
}
