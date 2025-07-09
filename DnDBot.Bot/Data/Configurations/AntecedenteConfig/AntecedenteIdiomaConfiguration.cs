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
    public class AntecedenteIdiomaConfiguration : IEntityTypeConfiguration<AntecedenteIdioma>
    {
        public void Configure(EntityTypeBuilder<AntecedenteIdioma> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.IdiomaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Idiomas)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Idioma)
                   .WithMany()
                   .HasForeignKey(x => x.IdiomaId);
        }
    }
}
