using DnDBot.Bot.Models.AntecedenteModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.AntecedenteConfig
{
    public class AntecedenteConfiguration : IEntityTypeConfiguration<Antecedente>
    {
        public void Configure(EntityTypeBuilder<Antecedente> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Configura o relacionamento com AntecedenteTag
            entity.HasMany(a => a.AntecedenteTags)
                  .WithOne(at => at.Antecedente)
                  .HasForeignKey(at => at.AntecedenteId);
        }
    }
}
