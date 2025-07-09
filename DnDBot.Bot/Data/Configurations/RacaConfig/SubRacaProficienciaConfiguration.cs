using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubRacaProficienciaConfiguration : IEntityTypeConfiguration<SubRacaProficiencia>
{
    public void Configure(EntityTypeBuilder<SubRacaProficiencia> builder)
    {
        builder.HasKey(x => new { x.SubRacaId, x.ProficienciaId });

        builder.HasOne(x => x.SubRaca)
               .WithMany(s => s.Proficiencias)
               .HasForeignKey(x => x.SubRacaId);

        builder.HasOne(x => x.Proficiencia)
               .WithMany()
               .HasForeignKey(x => x.ProficienciaId);
    }
}
