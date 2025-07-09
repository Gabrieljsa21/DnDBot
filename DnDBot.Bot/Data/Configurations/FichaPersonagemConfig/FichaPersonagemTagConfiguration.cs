using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.FichaPersonagemConfig
{
    public class FichaPersonagemTagConfiguration : IEntityTypeConfiguration<FichaPersonagemTag>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemTag> entity)
        {
            entity.HasKey(x => new { x.FichaPersonagemId, x.Tag });

            entity.HasOne(x => x.FichaPersonagem)
                .WithMany(fp => fp.FichaPersonagemTags)
                .HasForeignKey(ft => ft.FichaPersonagemId);
        }
    }
}
