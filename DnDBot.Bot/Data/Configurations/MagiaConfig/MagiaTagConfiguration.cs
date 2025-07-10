using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MagiaTagConfiguration : IEntityTypeConfiguration<MagiaTag>
{
    public void Configure(EntityTypeBuilder<MagiaTag> builder)
    {
        builder.HasKey(x => new { x.MagiaId, x.Tag });

        builder.HasOne(x => x.Magia)
               .WithMany(m => m.MagiaTags)
               .HasForeignKey(x => x.MagiaId);
    }
}
