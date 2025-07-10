using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.MagiaConfig
{
    public class MagiaConfiguration : IEntityTypeConfiguration<Magia>
    {
        public void Configure(EntityTypeBuilder<Magia> entity)
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(m => m.MagiaTags)
                .WithOne(mt => mt.Magia)
                .HasForeignKey(mt => mt.MagiaId);
        }
    }
}
