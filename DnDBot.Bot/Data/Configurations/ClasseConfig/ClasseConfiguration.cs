using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations.ClasseConfig
{
    public class ClasseConfiguration : IEntityTypeConfiguration<Classe>
    {
        public void Configure(EntityTypeBuilder<Classe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DadoVida)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(c => c.PapelTatico)
                   .HasMaxLength(50);

            builder.Property(c => c.IdHabilidadeConjuracao)
                   .HasMaxLength(50);

            builder.Property(c => c.UsaMagiaPreparada)
                   .IsRequired();

            builder.HasMany(c => c.Proficiencias)
                   .WithOne(p => p.Classe)
                   .HasForeignKey(p => p.ClasseId);

            builder.HasMany(c => c.PericiasRelacionadas)
                   .WithOne(p => p.Classe)
                   .HasForeignKey(p => p.ClasseId);

            builder.HasMany(c => c.MagiasDisponiveis)
                   .WithOne(m => m.Classe)
                   .HasForeignKey(m => m.ClasseId);

            builder.HasMany(c => c.TruquesConhecidosPorNivelList)
                   .WithOne()
                   .HasForeignKey("ClasseId");

            builder.HasMany(c => c.MagiasConhecidasPorNivelList)
                   .WithOne()
                   .HasForeignKey("ClasseId");

            builder.HasMany(c => c.EspacosMagia)
                   .WithOne()
                   .HasForeignKey("ClasseId");

            builder.HasMany(c => c.Subclasses)
                   .WithOne()
                   .HasForeignKey("ClasseId");

            builder.HasMany(c => c.CaracteristicasPorNivelList)
                   .WithOne()
                   .HasForeignKey("ClasseId");

            builder.HasMany(c => c.IdItensIniciais)
                   .WithOne(i => i.Classe)
                   .HasForeignKey(i => i.ClasseId);

            builder.HasMany(c => c.Moedas)
                   .WithOne(m => m.Classe)
                   .HasForeignKey(m => m.ClasseId);

            builder.HasMany(c => c.ClasseTags)
                   .WithOne(t => t.Classe)
                   .HasForeignKey(t => t.ClasseId);

            builder.Ignore(c => c.Tags);
            builder.Ignore(c => c.CaracteristicasPorNivel);
            builder.Ignore(c => c.ProgressaoPorNivel);
        }
    }
}
