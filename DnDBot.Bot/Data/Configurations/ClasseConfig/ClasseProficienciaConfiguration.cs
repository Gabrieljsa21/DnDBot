using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseProficienciaConfiguration : IEntityTypeConfiguration<ClasseProficiencia>
    {
        public void Configure(EntityTypeBuilder<ClasseProficiencia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ProficienciaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }
}
