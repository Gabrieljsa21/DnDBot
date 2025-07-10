using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MagiaCondicaoRemovidaConfiguration : IEntityTypeConfiguration<MagiaCondicaoRemovida>
{
    public void Configure(EntityTypeBuilder<MagiaCondicaoRemovida> builder)
    {
        builder.HasKey(x => new { x.MagiaId, x.Condicao });

        builder.HasOne(x => x.Magia)
               .WithMany()
               .HasForeignKey(x => x.MagiaId);
    }
}
