using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class IdiomaConfiguration : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> entity)
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Nome)
                   .HasMaxLength(50)
                   .IsRequired();

            // Relação many-to-many com FichaPersonagem configurada em FichaPersonagemConfiguration

            entity.ToTable("Idioma");
        }
    }
}
