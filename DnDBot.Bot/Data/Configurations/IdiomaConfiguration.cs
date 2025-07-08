using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class IdiomaConfiguration : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
                   .HasMaxLength(50)
                   .IsRequired();

            // Relação many-to-many com FichaPersonagem configurada em FichaPersonagemConfiguration

            builder.ToTable("Idioma");
        }
    }
}
