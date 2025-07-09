using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseMagiaConfiguration : IEntityTypeConfiguration<ClasseMagia>
    {
        public void Configure(EntityTypeBuilder<ClasseMagia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.MagiaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
        }
    }
}
