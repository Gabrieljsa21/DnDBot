using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class MagiaConfiguration : IEntityTypeConfiguration<Magia>
    {
        public void Configure(EntityTypeBuilder<Magia> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(m => m.MagiaTags)
                .WithOne(mt => mt.Magia)
                .HasForeignKey(mt => mt.MagiaId);
        }
    }
}
