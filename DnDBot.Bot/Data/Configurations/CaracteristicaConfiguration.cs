using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Caracteristica para o Entity Framework Core.
    /// Define o mapeamento dos campos e restrições da tabela Caracteristica.
    /// </summary>
    public class CaracteristicaConfiguration : IEntityTypeConfiguration<Caracteristica>
    {
        public void Configure(EntityTypeBuilder<Caracteristica> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Descricao)
                   .HasMaxLength(4000);

            builder.Property(c => c.Fonte)
                   .HasMaxLength(200);

            builder.Property(c => c.Pagina)
                   .HasMaxLength(20);

            builder.Property(c => c.Versao)
                   .HasMaxLength(50);

            builder.Property(c => c.ImagemUrl)
                   .HasMaxLength(1000);

            builder.Property(c => c.IconeUrl)
                   .HasMaxLength(1000);

            builder.Property(c => c.CriadoPor)
                   .HasMaxLength(100);

            builder.Property(c => c.ModificadoPor)
                   .HasMaxLength(100);

            // Enums
            builder.Property(c => c.Tipo)
                   .HasConversion<string>()
                   .IsRequired();


            builder.Property(c => c.Origem)
                   .HasConversion<string>()
                   .IsRequired();


            builder.HasMany(c => c.EfeitoEscalonado)
               .WithOne(e => e.Caracteristica)
               .HasForeignKey(e => e.CaracteristicaId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
