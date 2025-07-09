using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.CaracteristicaConfig
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

            builder.Property(c => c.AcaoRequerida)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.Alvo)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.CondicaoAtivacao)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.Origem)
                   .HasConversion<string>()
                   .IsRequired();

            // Níveis
            builder.Property(c => c.NivelMinimo)
                   .IsRequired();

            builder.Property(c => c.NivelMaximo)
                   .IsRequired(false);

            builder.Property(c => c.DuracaoEmRodadas)
                   .IsRequired(false);

            builder.Property(c => c.UsosPorDescansoCurto)
                   .IsRequired(false);

            builder.Property(c => c.UsosPorDescansoLongo)
                   .IsRequired(false);
        }
    }
}
