using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ItemRacaConfiguration : IEntityTypeConfiguration<ItemRaca>
{
    public void Configure(EntityTypeBuilder<ItemRaca> builder)
    {
        // Define chave primária composta
        builder.HasKey(ir => new { ir.ItemId, ir.RacaId });

        // Configura relacionamentos
        builder.HasOne(ir => ir.Item)
               .WithMany(i => i.RacasPermitidas)
               .HasForeignKey(ir => ir.ItemId);

        builder.HasOne(ir => ir.Raca)
               .WithMany()
               .HasForeignKey(ir => ir.RacaId);
    }
}
public class ItemMaterialConfiguration : IEntityTypeConfiguration<ItemMaterial>
{
    public void Configure(EntityTypeBuilder<ItemMaterial> builder)
    {
        // Define chave primária composta
        builder.HasKey(im => new { im.ItemId, im.MaterialId });

        builder.HasOne<Material>(im => im.Material)
               .WithMany()
               .HasForeignKey(im => im.MaterialId);

    }
}
