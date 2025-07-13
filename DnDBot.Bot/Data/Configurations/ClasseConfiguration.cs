using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
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
    public class ClasseMagiaConfiguration : IEntityTypeConfiguration<ClasseMagia>
    {
        public void Configure(EntityTypeBuilder<ClasseMagia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.MagiaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
        }
    }
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
    public class ClassePericiaConfiguration : IEntityTypeConfiguration<ClassePericia>
    {
        public void Configure(EntityTypeBuilder<ClassePericia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.PericiaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Pericia)
                   .WithMany()
                   .HasForeignKey(x => x.PericiaId);
        }
    }
    public class ClasseProficienciaConfiguration : IEntityTypeConfiguration<ClasseProficiencia>
    {
        public void Configure(EntityTypeBuilder<ClasseProficiencia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ProficienciaId });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }
    public class ClasseSalvaguardaConfiguration : IEntityTypeConfiguration<ClasseSalvaguarda>
    {
        public void Configure(EntityTypeBuilder<ClasseSalvaguarda> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.IdSalvaguarda });

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.ClasseId);
        }
    }
    public class ClasseTagConfiguration : IEntityTypeConfiguration<ClasseTag>
    {
        public void Configure(EntityTypeBuilder<ClasseTag> builder)
        {
            builder.HasKey(ct => new { ct.ClasseId, ct.Tag });

            builder.HasOne(ct => ct.Classe)
                   .WithMany(c => c.ClasseTags)
                   .HasForeignKey(ct => ct.ClasseId);
        }
    }
}
