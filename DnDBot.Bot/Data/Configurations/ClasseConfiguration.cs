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

            builder.HasMany(c => c.Magias)
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

            builder.HasMany(c => c.Itens)
                   .WithOne(i => i.Classe)
                   .HasForeignKey(i => i.ClasseId);

            builder.HasMany(c => c.ItensOpcoes)
                   .WithOne(g => g.Classe)
                   .HasForeignKey(g => g.ClasseId);

            builder.HasMany(c => c.ClasseTags)
                   .WithOne(t => t.Classe)
                   .HasForeignKey(t => t.ClasseId);

            builder.Ignore(c => c.Tags);
            builder.Ignore(c => c.CaracteristicasPorNivel);
            builder.Ignore(c => c.ProgressaoPorNivel);

            builder.Property(c => c.QntOpcoesProficiencias)
                    .HasColumnName("QntOpcoesProficiencias");

            builder.HasMany(c => c.OpcoesProficiencias)
                   .WithOne(o => o.Classe)
                   .HasForeignKey(o => o.ClasseId);

        }
    }

    public class ClasseItensFixosConfiguration : IEntityTypeConfiguration<ClasseItemFixo>
    {
        public void Configure(EntityTypeBuilder<ClasseItemFixo> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ItemId });

            builder.HasOne(x => x.Classe)
                   .WithMany(c => c.Itens)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }

    public class ClassesOpcoesItensGrupoConfiguration : IEntityTypeConfiguration<ClasseOpcaoItemGrupo>
    {
        public void Configure(EntityTypeBuilder<ClasseOpcaoItemGrupo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(x => x.Classe)
                   .WithMany(c => c.ItensOpcoes)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasMany(x => x.Opcoes)
                   .WithOne(o => o.Grupo)
                   .HasForeignKey(o => o.GrupoId);
        }
    }

    public class ClassesOpcoesItensOpcaoConfiguration : IEntityTypeConfiguration<ClasseOpcaoItemOpcao>
    {
        public void Configure(EntityTypeBuilder<ClasseOpcaoItemOpcao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(x => x.Grupo)
                   .WithMany(g => g.Opcoes)
                   .HasForeignKey(x => x.GrupoId);

            builder.HasMany(x => x.Itens)
                   .WithOne(i => i.Opcao)
                   .HasForeignKey(i => i.OpcaoId);
        }
    }

    public class ClasseItensOpcaoItemConfiguration : IEntityTypeConfiguration<ClasseItemOpcaoItem>
    {
        public void Configure(EntityTypeBuilder<ClasseItemOpcaoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Opcao)
                   .WithMany(o => o.Itens)
                   .HasForeignKey(x => x.OpcaoId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }

    public class ClasseMagiaConfiguration : IEntityTypeConfiguration<ClasseMagia>
    {
        public void Configure(EntityTypeBuilder<ClasseMagia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.MagiaId });

            builder.HasOne(x => x.Classe)
                   .WithMany(x=>x.Magias)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
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

    public class ClasseProficienciaConfiguration : IEntityTypeConfiguration<ClasseProficiencia>
    {
        public void Configure(EntityTypeBuilder<ClasseProficiencia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ProficienciaId });

            builder.HasOne(x => x.Classe)
                   .WithMany(x => x.Proficiencias)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }

    public class ClasseOpcaoProficienciaConfiguration : IEntityTypeConfiguration<ClasseOpcaoProficiencia>
    {
        public void Configure(EntityTypeBuilder<ClasseOpcaoProficiencia> builder)
        {
            builder.HasKey(x => new { x.ClasseId, x.ProficienciaId });

            builder.HasOne(x => x.Classe)
                   .WithMany(c => c.OpcoesProficiencias)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }

    public class ClasseOpcaoPericiaConfiguration : IEntityTypeConfiguration<ClasseOpcaoPericia>
    {
        public void Configure(EntityTypeBuilder<ClasseOpcaoPericia> builder)
        {
            // define a chave composta
            builder.HasKey(x => new { x.ClasseId, x.PericiaId });

            // relacionamentos
            builder.HasOne(x => x.Classe)
                   .WithMany(c => c.OpcoesPericias)
                   .HasForeignKey(x => x.ClasseId);

            builder.HasOne(x => x.Pericia)
                   .WithMany()  // ou, se houver coleção inversa
                   .HasForeignKey(x => x.PericiaId);
        }
    }
    public class ClasseProgressaoConfiguration : IEntityTypeConfiguration<ClasseProgressao>
    {
        public void Configure(EntityTypeBuilder<ClasseProgressao> builder)
        {
            builder.HasKey(p => new { p.ClasseId, p.Nivel });

            builder
                .HasOne<Classe>(p => p.Classe)
                .WithMany()
                .HasForeignKey(p => p.ClasseId);
        }
    }

}
