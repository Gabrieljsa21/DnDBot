using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Data.Configurations
{
    public class PropriedadesMagicasConfiguration : IEntityTypeConfiguration<PropriedadesMagicas>
    {
        public void Configure(EntityTypeBuilder<PropriedadesMagicas> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
