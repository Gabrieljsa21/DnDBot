using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class RacaConfiguration : IEntityTypeConfiguration<Raca>
    {
        public void Configure(EntityTypeBuilder<Raca> entity)
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Configura relacionamento com RacaTags (many-to-many via entidade)
            entity.HasMany(r => r.RacaTags)
                .WithOne(rt => rt.Raca)
                .HasForeignKey(rt => rt.RacaId);

        }
    }

    public class SubRacaAlinhamentoConfiguration : IEntityTypeConfiguration<SubRacaAlinhamento>
    {
        /// <summary>
        /// Configura a entidade SubRacaAlinhamento.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade SubRacaAlinhamento.</param>
        public void Configure(EntityTypeBuilder<SubRacaAlinhamento> entity)
        {

            // Chave composta
            entity.HasKey(sa => new { sa.SubRacaId, sa.AlinhamentoId });

            // Relacionamento com SubRaca
            entity.HasOne(sa => sa.SubRaca)
                   .WithMany(s => s.AlinhamentosComuns)
                   .HasForeignKey(sa => sa.SubRacaId);

            // Relacionamento com Alinhamento
            entity.HasOne(sa => sa.Alinhamento)
                   .WithMany()
                   .HasForeignKey(sa => sa.AlinhamentoId);
        }
    }
    public class SubRacaCaracteristicaConfiguration : IEntityTypeConfiguration<SubRacaCaracteristica>
    {
        public void Configure(EntityTypeBuilder<SubRacaCaracteristica> builder)
        {
            // Define chave composta
            builder.HasKey(sc => new { sc.SubRacaId, sc.CaracteristicaId });

            // Relacionamento com SubRaca
            builder.HasOne(sc => sc.SubRaca)
                   .WithMany(s => s.Caracteristicas)
                   .HasForeignKey(sc => sc.SubRacaId);

            // Relacionamento com Caracteristica
            builder.HasOne(sc => sc.Caracteristica)
                   .WithMany()
                   .HasForeignKey(sc => sc.CaracteristicaId);
        }
    }
    public class SubRacaConfiguration : IEntityTypeConfiguration<SubRaca>
    {
        /// <summary>
        /// Configura a entidade SubRaca.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade SubRaca.</param>
        public void Configure(EntityTypeBuilder<SubRaca> entity)
        {
            // Define a propriedade Id como chave primária da tabela SubRaca
            entity.HasKey(sr => sr.Id);

            entity.Property(sr => sr.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(sr => sr.SubRacaTags)
                .WithOne(srt => srt.SubRaca)
                .HasForeignKey(srt => srt.SubRacaId);

            entity.HasMany(sr => sr.AlinhamentosComuns)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Idiomas)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Resistencias)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Proficiencias)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.Caracteristicas)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            entity.HasMany(sr => sr.MagiasRaciais)
                .WithOne(sa => sa.SubRaca)
                .HasForeignKey(sa => sa.SubRacaId);

            // Configura o relacionamento muitos-para-um com Raca
            // Uma SubRaca pertence a uma Raca, que possui muitas SubRacas
            // Exclusão em cascata para as SubRacas ao deletar a Raca relacionada
            entity.HasOne(sr => sr.Raca)
                  .WithMany(r => r.SubRaca)
                  .HasForeignKey(sr => sr.RacaId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
    public class SubRacaIdiomaConfiguration : IEntityTypeConfiguration<SubRacaIdioma>
    {
        public void Configure(EntityTypeBuilder<SubRacaIdioma> builder)
        {
            builder.HasKey(x => new { x.SubRacaId, x.IdiomaId });

            builder.HasOne(x => x.SubRaca)
                   .WithMany(s => s.Idiomas)
                   .HasForeignKey(x => x.SubRacaId);

            builder.HasOne(x => x.Idioma)
                   .WithMany()
                   .HasForeignKey(x => x.IdiomaId);
        }
    }
    public class SubRacaMagiaConfiguration : IEntityTypeConfiguration<SubRacaMagia>
    {
        public void Configure(EntityTypeBuilder<SubRacaMagia> builder)
        {
            builder.HasKey(x => new { x.SubRacaId, x.MagiaId });

            builder.HasOne(x => x.SubRaca)
                   .WithMany(s => s.MagiasRaciais)
                   .HasForeignKey(x => x.SubRacaId);

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
        }
    }
    public class SubRacaProficienciaConfiguration : IEntityTypeConfiguration<SubRacaProficiencia>
    {
        public void Configure(EntityTypeBuilder<SubRacaProficiencia> builder)
        {
            builder.HasKey(x => new { x.SubRacaId, x.ProficienciaId });

            builder.HasOne(x => x.SubRaca)
                   .WithMany(s => s.Proficiencias)
                   .HasForeignKey(x => x.SubRacaId);

            builder.HasOne(x => x.Proficiencia)
                   .WithMany()
                   .HasForeignKey(x => x.ProficienciaId);
        }
    }
    public class SubRacaResistenciaConfiguration : IEntityTypeConfiguration<SubRacaResistencia>
    {
        /// <summary>
        /// Configura a entidade SubRacaResistencia.
        /// </summary>
        /// <param name="builder">Construtor para configuração da entidade SubRacaResistencia.</param>
        public void Configure(EntityTypeBuilder<SubRacaResistencia> builder)
        {
            // Chave composta
            builder.HasKey(x => new { x.SubRacaId, x.ResistenciaId });

            // Relacionamento com SubRaca
            builder.HasOne(x => x.SubRaca)
                   .WithMany(sr => sr.Resistencias)
                   .HasForeignKey(x => x.SubRacaId);

            // Relacionamento com Resistencia
            builder.HasOne(x => x.Resistencia)
                   .WithMany()
                   .HasForeignKey(x => x.ResistenciaId);
        }
    }



}
