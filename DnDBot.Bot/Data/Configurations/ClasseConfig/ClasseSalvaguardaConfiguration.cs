using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseSalvaguardaConfiguration : IEntityTypeConfiguration<ClasseSalvaguarda>
    {
        public void Configure(EntityTypeBuilder<ClasseSalvaguarda> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.IdSalvaguarda });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);
        }
    }
}
