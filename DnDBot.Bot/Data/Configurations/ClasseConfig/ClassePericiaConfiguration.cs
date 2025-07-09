using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClassePericiaConfiguration : IEntityTypeConfiguration<ClassePericia>
    {
        public void Configure(EntityTypeBuilder<ClassePericia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.PericiaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Pericia)
                   .WithMany()
                   .HasForeignKey(x => x.PericiaId);
        }
    }
}
