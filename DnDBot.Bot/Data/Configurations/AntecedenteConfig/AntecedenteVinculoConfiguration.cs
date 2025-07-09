using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Data.Configurations.AntecedenteConfig
{
    public class AntecedenteVinculoConfiguration : IEntityTypeConfiguration<AntecedenteVinculo>
    {
        public void Configure(EntityTypeBuilder<AntecedenteVinculo> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.VinculoId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Vinculos)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Vinculo)
                   .WithMany()
                   .HasForeignKey(x => x.VinculoId);
        }
    }
}
