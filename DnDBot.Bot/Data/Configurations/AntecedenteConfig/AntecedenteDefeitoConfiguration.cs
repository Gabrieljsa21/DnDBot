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
    public class AntecedenteDefeitoConfiguration : IEntityTypeConfiguration<AntecedenteDefeito>
    {
        public void Configure(EntityTypeBuilder<AntecedenteDefeito> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.DefeitoId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Defeitos)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Defeito)
                   .WithMany()
                   .HasForeignKey(x => x.DefeitoId);
        }
    }
}
