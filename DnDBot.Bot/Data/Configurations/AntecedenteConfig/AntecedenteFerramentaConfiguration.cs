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
    public class AntecedenteFerramentaConfiguration : IEntityTypeConfiguration<AntecedenteFerramenta>
    {
        public void Configure(EntityTypeBuilder<AntecedenteFerramenta> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.FerramentaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Ferramentas)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Ferramenta)
                   .WithMany()
                   .HasForeignKey(x => x.FerramentaId);
        }
    }
}
