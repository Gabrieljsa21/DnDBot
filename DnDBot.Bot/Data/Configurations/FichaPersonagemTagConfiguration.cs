using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class FichaPersonagemTagConfiguration : IEntityTypeConfiguration<FichaPersonagemTag>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemTag> builder)
        {
            builder.HasKey(ft => new { ft.FichaPersonagemId, ft.Tag });

            builder.HasOne(ft => ft.FichaPersonagem)
                .WithMany(fp => fp.FichaPersonagemTags)
                .HasForeignKey(ft => ft.FichaPersonagemId);
        }
    }
}
