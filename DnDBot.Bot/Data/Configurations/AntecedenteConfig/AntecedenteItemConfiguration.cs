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
    public class AntecedenteItemConfiguration : IEntityTypeConfiguration<AntecedenteItem>
    {
        public void Configure(EntityTypeBuilder<AntecedenteItem> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ItemId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.itens)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }
}
