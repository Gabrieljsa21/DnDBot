using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class BonusAtributoConfiguration : IEntityTypeConfiguration<BonusAtributo>
    {
        public void Configure(EntityTypeBuilder<BonusAtributo> entity)
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Origem)
                   .HasMaxLength(100);

            entity.Property(b => b.OwnerType)
                   .HasMaxLength(50);

            entity.Property(b => b.Valor)
                   .IsRequired();

            entity.Property(b => b.Atributo)
                   .HasConversion<int>()
                   .IsRequired();

            entity.ToTable("BonusAtributos");
        }
    }
}
