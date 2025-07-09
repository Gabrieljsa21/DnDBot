using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseItensConfiguration : IEntityTypeConfiguration<ClasseItens>
    {
        public void Configure(EntityTypeBuilder<ClasseItens> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ItemId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }
}
