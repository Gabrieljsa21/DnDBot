using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseMoedaConfiguration : IEntityTypeConfiguration<ClasseMoeda>
    {
        public void Configure(EntityTypeBuilder<ClasseMoeda> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.MoedaId });

            builder.HasOne(x => x.Classe)
                   .WithMany(c => c.Moedas)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Moeda)
                   .WithMany()
                   .HasForeignKey(x => x.MoedaId);
        }
    }
}
