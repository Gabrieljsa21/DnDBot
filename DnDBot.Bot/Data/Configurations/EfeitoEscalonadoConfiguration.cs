using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class EfeitoEscalonadoConfiguration : IEntityTypeConfiguration<EfeitoEscalonado>
    {
        public void Configure(EntityTypeBuilder<EfeitoEscalonado> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.DescricaoEfeito)
                   .HasMaxLength(4000);

            builder.Property(e => e.FocoNecessario)
                   .HasMaxLength(200);

            // Enums armazenados como strings
            builder.Property(e => e.AcaoRequerida)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.CondicaoAtivacao)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.FormaAreaEfeito)
                   .HasConversion<string>()
                   .IsRequired(false);

            builder.Property(e => e.Alcance)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.Alvo)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.DuracaoUnidade)
                   .HasConversion<string>()
                   .IsRequired(false);

            builder.Property(e => e.Recarga)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.TipoUso)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.AtributoTesteResistencia)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.TempoConjuracao)
                   .HasConversion<string>()
                   .IsRequired();

            // Relação com Caracteristica (opcional)
            builder.HasOne(e => e.Caracteristica)
                   .WithMany(c => c.EfeitoEscalonado)
                   .HasForeignKey(e => e.CaracteristicaId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relação com Magia (opcional)
            builder.HasOne(e => e.Magia)
                   .WithMany(m => m.EfeitosEscalonados)
                   .HasForeignKey(e => e.MagiaId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relação com EfeitoDano - 1 para muitos
            builder.HasMany(e => e.Danos)
                   .WithOne(d => d.EfeitoEscalonado)
                   .HasForeignKey(d => d.EfeitoEscalonadoId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Outros campos simples (int?, bool, etc) não precisam de configurações especiais
        }
    }

    public class EfeitoDanoConfiguration : IEntityTypeConfiguration<EfeitoDano>
    {
        public void Configure(EntityTypeBuilder<EfeitoDano> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.DadoDano)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.TipoDano)
                   .HasConversion<string>()
                   .IsRequired();

            // Relação com EfeitoEscalonado
            builder.HasOne(d => d.EfeitoEscalonado)
                   .WithMany(e => e.Danos)
                   .HasForeignKey(d => d.EfeitoEscalonadoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
