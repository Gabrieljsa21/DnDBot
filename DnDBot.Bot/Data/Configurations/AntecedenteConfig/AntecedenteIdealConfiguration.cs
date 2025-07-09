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
    public class AntecedenteIdealConfiguration : IEntityTypeConfiguration<AntecedenteIdeal>
    {
        public void Configure(EntityTypeBuilder<AntecedenteIdeal> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.IdealId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Ideais)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Ideal)
                   .WithMany()
                   .HasForeignKey(x => x.IdealId);
        }
    }
}
