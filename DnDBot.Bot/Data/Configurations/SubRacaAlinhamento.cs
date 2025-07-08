using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class SubRacaAlinhamentoConfiguration : IEntityTypeConfiguration<SubRacaAlinhamento>
    {
        public void Configure(EntityTypeBuilder<SubRacaAlinhamento> builder)
        {
            builder.HasKey(sa => new { sa.SubRacaId, sa.AlinhamentoId });

            builder.HasOne(sa => sa.SubRaca)
                .WithMany(sr => sr.AlinhamentosComuns)
                .HasForeignKey(sa => sa.SubRacaId);

            builder.HasOne(sa => sa.Alinhamento)
                .WithMany()
                .HasForeignKey(sa => sa.AlinhamentoId);
        }
    }
}
