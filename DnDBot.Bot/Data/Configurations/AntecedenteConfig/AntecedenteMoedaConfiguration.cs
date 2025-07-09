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
    public class AntecedenteMoedaConfiguration : IEntityTypeConfiguration<AntecedenteMoeda>
    {
        public void Configure(EntityTypeBuilder<AntecedenteMoeda> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.MoedaId });

            builder.Property(x => x.Quantidade).IsRequired();

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Moedas)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Moeda)
                   .WithMany()
                   .HasForeignKey(x => x.MoedaId);
        }
    }
}
