using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubRacaIdiomaConfiguration : IEntityTypeConfiguration<SubRacaIdioma>
{
    public void Configure(EntityTypeBuilder<SubRacaIdioma> builder)
    {
        builder.HasKey(x => new { x.SubRacaId, x.IdiomaId });

        builder.HasOne(x => x.SubRaca)
               .WithMany(s => s.Idiomas)
               .HasForeignKey(x => x.SubRacaId);

        builder.HasOne(x => x.Idioma)
               .WithMany()
               .HasForeignKey(x => x.IdiomaId);
    }
}
