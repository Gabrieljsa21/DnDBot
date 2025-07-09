using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubRacaMagiaConfiguration : IEntityTypeConfiguration<SubRacaMagia>
{
    public void Configure(EntityTypeBuilder<SubRacaMagia> builder)
    {
        builder.HasKey(x => new { x.SubRacaId, x.MagiaId });

        builder.HasOne(x => x.SubRaca)
               .WithMany(s => s.MagiasRaciais)
               .HasForeignKey(x => x.SubRacaId);

        builder.HasOne(x => x.Magia)
               .WithMany()
               .HasForeignKey(x => x.MagiaId);
    }
}
