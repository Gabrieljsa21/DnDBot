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
    public class AntecedenteCaracteristicaConfiguration : IEntityTypeConfiguration<AntecedenteCaracteristica>
    {
        public void Configure(EntityTypeBuilder<AntecedenteCaracteristica> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.CaracteristicaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Caracteristicas)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Caracteristica)
                   .WithMany()
                   .HasForeignKey(x => x.CaracteristicaId);
        }
    }
}
