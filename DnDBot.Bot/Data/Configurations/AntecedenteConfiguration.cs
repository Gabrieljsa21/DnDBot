using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class AntecedenteConfiguration : IEntityTypeConfiguration<Antecedente>
    {
        public void Configure(EntityTypeBuilder<Antecedente> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.Descricao)
                  .HasMaxLength(4000);

            entity.Property(a => a.Fonte)
                  .HasMaxLength(100);

            entity.Property(a => a.Pagina)
                  .HasMaxLength(10);

            entity.Property(a => a.Versao)
                  .HasMaxLength(20);

            entity.Property(a => a.ImagemUrl)
                  .HasMaxLength(500);

            entity.Property(a => a.IconeUrl)
                  .HasMaxLength(500);

            entity.HasMany(a => a.AntecedenteTags)
                  .WithOne(at => at.Antecedente)
                  .HasForeignKey(at => at.AntecedenteId);

            entity.HasMany(a => a.Proficiencias)
                  .WithOne(p => p.Antecedente)
                  .HasForeignKey(p => p.AntecedenteId);

            entity.HasMany(a => a.Itens)
                  .WithOne(i => i.Antecedente)
                  .HasForeignKey(i => i.AntecedenteId);

            entity.HasMany(a => a.Caracteristicas)
                  .WithOne(c => c.Antecedente)
                  .HasForeignKey(c => c.AntecedenteId);

            entity.HasMany(a => a.Narrativas)
                  .WithOne(i => i.Antecedente)
                  .HasForeignKey(i => i.AntecedenteId);

        }
    }

    public class AntecedenteCaracteristicaConfiguration : IEntityTypeConfiguration<AntecedenteCaracteristica>
    {
        public void Configure(EntityTypeBuilder<AntecedenteCaracteristica> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.CaracteristicaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Caracteristicas)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Caracteristica)
                   .WithMany()
                   .HasForeignKey(x => x.CaracteristicaId);
        }
    }

    public class AntecedenteProficienciaConfiguration : IEntityTypeConfiguration<AntecedenteProficiencia>
    {
        public void Configure(EntityTypeBuilder<AntecedenteProficiencia> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ProficienciaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Proficiencias)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }

    public class AntecedenteItemConfiguration : IEntityTypeConfiguration<AntecedenteItem>
    {
        public void Configure(EntityTypeBuilder<AntecedenteItem> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ItemId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Itens)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }
    public class AntecedenteNarrativaConfiguration : IEntityTypeConfiguration<AntecedenteNarrativa>
    {
        public void Configure(EntityTypeBuilder<AntecedenteNarrativa> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Descricao)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(n => n.Tipo)
                   .IsRequired();

            builder.HasOne(n => n.Antecedente)
                   .WithMany(a => a.Narrativas)
                   .HasForeignKey(n => n.AntecedenteId);
        }
    }
    public class AntecedenteItemOpcoesConfiguration : IEntityTypeConfiguration<AntecedenteItemOpcoes>
    {
        public void Configure(EntityTypeBuilder<AntecedenteItemOpcoes> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ItemId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.OpcoesItens)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId);
        }
    }
    public class AntecedenteProficienciaOpcoesConfiguration : IEntityTypeConfiguration<AntecedenteProficienciaOpcoes>
    {
        public void Configure(EntityTypeBuilder<AntecedenteProficienciaOpcoes> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ProficienciaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.OpcoesProficiencia)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }
    public class AntecedenteTagConfiguration : IEntityTypeConfiguration<AntecedenteTag>
    {
        public void Configure(EntityTypeBuilder<AntecedenteTag> builder)
        {
            builder.HasKey(at => new { at.AntecedenteId, at.Tag });

            builder.HasOne(at => at.Antecedente)
                   .WithMany(a => a.AntecedenteTags)
                   .HasForeignKey(at => at.AntecedenteId);
        }
    }
}
