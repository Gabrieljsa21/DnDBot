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

            entity.HasMany(a => a.Proficiencia)
                  .WithOne(p => p.Antecedente)
                  .HasForeignKey(p => p.AntecedenteId);

            entity.HasMany(a => a.Itens)
                  .WithOne(i => i.Antecedente)
                  .HasForeignKey(i => i.AntecedenteId);

            entity.HasMany(a => a.Caracteristicas)
                  .WithOne(c => c.Antecedente)
                  .HasForeignKey(c => c.AntecedenteId);

            entity.HasMany(a => a.Ideais)
                  .WithOne(i => i.Antecedente)
                  .HasForeignKey(i => i.AntecedenteId);

            entity.HasMany(a => a.Vinculos)
                  .WithOne(v => v.Antecedente)
                  .HasForeignKey(v => v.AntecedenteId);

            entity.HasMany(a => a.Defeitos)
                  .WithOne(d => d.Antecedente)
                  .HasForeignKey(d => d.AntecedenteId);

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

    public class AntecedenteDefeitoConfiguration : IEntityTypeConfiguration<AntecedenteDefeito>
    {
        public void Configure(EntityTypeBuilder<AntecedenteDefeito> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.DefeitoId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Defeitos)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Defeito)
                   .WithMany()
                   .HasForeignKey(x => x.DefeitoId);
        }
    }

    public class AntecedenteProficienciaConfiguration : IEntityTypeConfiguration<AntecedenteProficiencia>
    {
        public void Configure(EntityTypeBuilder<AntecedenteProficiencia> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.ProficienciaId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Proficiencia)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }

    public class AntecedenteIdealConfiguration : IEntityTypeConfiguration<AntecedenteIdeal>
    {
        public void Configure(EntityTypeBuilder<AntecedenteIdeal> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.IdealId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Ideais)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Ideal)
                   .WithMany()
                   .HasForeignKey(x => x.IdealId);
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

    public class AntecedenteVinculoConfiguration : IEntityTypeConfiguration<AntecedenteVinculo>
    {
        public void Configure(EntityTypeBuilder<AntecedenteVinculo> builder)
        {
            builder.HasKey(x => new { x.AntecedenteId, x.VinculoId });

            builder.HasOne(x => x.Antecedente)
                   .WithMany(a => a.Vinculos)
                   .HasForeignKey(x => x.AntecedenteId);

            builder.HasOne(x => x.Vinculo)
                   .WithMany()
                   .HasForeignKey(x => x.VinculoId);
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
}
