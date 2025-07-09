using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AntecedentePericiaConfiguration : IEntityTypeConfiguration<AntecedentePericia>
{
    public void Configure(EntityTypeBuilder<AntecedentePericia> builder)
    {
        builder.HasKey(x => new { x.AntecedenteId, x.PericiaId });

        builder.HasOne(x => x.Antecedente)
               .WithMany(a => a.Pericias) // Supondo coleção na entidade Antecedente
               .HasForeignKey(x => x.AntecedenteId);

        builder.HasOne(x => x.Pericia)
               .WithMany()
               .HasForeignKey(x => x.PericiaId);
    }
}
