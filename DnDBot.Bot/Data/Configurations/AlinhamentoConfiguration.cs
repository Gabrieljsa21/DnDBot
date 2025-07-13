using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Data.Configurations
{
    public class AlinhamentoTagConfiguration : IEntityTypeConfiguration<AlinhamentoTag>
    {
        public void Configure(EntityTypeBuilder<AlinhamentoTag> builder)
        {
            builder.HasKey(at => new { at.AlinhamentoId, at.Tag });

            builder.HasOne(at => at.Alinhamento)
                   .WithMany(a => a.AlinhamentoTags)
                   .HasForeignKey(at => at.AlinhamentoId);
        }
    }
}
