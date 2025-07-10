using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MagiaClassePermitidaConfiguration : IEntityTypeConfiguration<MagiaClassePermitida>
{
    public void Configure(EntityTypeBuilder<MagiaClassePermitida> builder)
    {
        builder.HasKey(x => new { x.MagiaId, x.Classe });

        builder.HasOne(x => x.Magia)
               .WithMany()
               .HasForeignKey(x => x.MagiaId);
    }
}
