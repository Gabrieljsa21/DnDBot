using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade FichaPersonagem para o Entity Framework Core.
    /// Define o mapeamento da classe FichaPersonagem para a tabela no banco de dados.
    /// </summary>
    public class FichaPersonagemConfiguration : IEntityTypeConfiguration<FichaPersonagem>
    {
        /// <summary>
        /// Configura a entidade FichaPersonagem.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade FichaPersonagem.</param>
        public void Configure(EntityTypeBuilder<FichaPersonagem> entity)
        {
            // Chave primária
            entity.HasKey(f => f.Id);

            // Propriedade Nome
            entity.Property(f => f.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Relacionamento 1:1 com Inventario
            entity.HasOne(f => f.Inventario)
                  .WithOne(i => i.FichaPersonagem)
                  .HasForeignKey<Inventario>(i => i.FichaPersonagemId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
    public class FichaPersonagemMagiaConfiguration : IEntityTypeConfiguration<FichaPersonagemMagia>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemMagia> builder)
        {
            // Chave composta
            builder.HasKey(fm => new { fm.FichaPersonagemId, fm.MagiaId });

            // Relacionamento com FichaPersonagem
            builder.HasOne(fm => fm.FichaPersonagem)
                   .WithMany(fp => fp.MagiasRaciais)
                   .HasForeignKey(fm => fm.FichaPersonagemId);

            // Relacionamento com Magia
            builder.HasOne(fm => fm.Magia)
                   .WithMany()
                   .HasForeignKey(fm => fm.MagiaId);
        }
    }
    public class FichaPersonagemProficienciaConfiguration : IEntityTypeConfiguration<FichaPersonagemProficiencia>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemProficiencia> builder)
        {
            // Define chave composta
            builder.HasKey(fp => new { fp.FichaPersonagemId, fp.ProficienciaId });

            // Relacionamento com FichaPersonagem
            builder.HasOne(fp => fp.FichaPersonagem)
                   .WithMany(p => p.Proficiencias)
                   .HasForeignKey(fp => fp.FichaPersonagemId);

            // Relacionamento com Proficiencia
            builder.HasOne(fp => fp.Proficiencia)
                   .WithMany()
                   .HasForeignKey(fp => fp.ProficienciaId);
        }
    }
    public class FichaPersonagemResistenciaConfiguration : IEntityTypeConfiguration<FichaPersonagemResistencia>
    {
        /// <summary>
        /// Configura a entidade FichaPersonagemResistencia.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade FichaPersonagemResistencia.</param>
        public void Configure(EntityTypeBuilder<FichaPersonagemResistencia> entity)
        {
            // Chave composta
            entity.HasKey(f => new { f.FichaPersonagemId, f.ResistenciaId });

            // Relacionamento com FichaPersonagem
            entity.HasOne(f => f.FichaPersonagem)
                   .WithMany(p => p.Resistencias)
                   .HasForeignKey(f => f.FichaPersonagemId);

            // Relacionamento com Resistencia
            entity.HasOne(f => f.Resistencia)
                   .WithMany()
                   .HasForeignKey(f => f.ResistenciaId);
        }
    }

    public class FichaPersonagemCaracteristicaConfiguration : IEntityTypeConfiguration<FichaPersonagemCaracteristica>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemCaracteristica> builder)
        {
            builder.HasKey(x => new { x.FichaPersonagemId, x.CaracteristicaId });
        }
    }

    public class FichaPersonagemIdiomaConfiguration : IEntityTypeConfiguration<FichaPersonagemIdioma>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemIdioma> builder)
        {
            builder.HasKey(x => new { x.FichaPersonagemId, x.IdiomaId });
        }
    }

    public class FichaPersonagemTagConfiguration : IEntityTypeConfiguration<FichaPersonagemTag>
    {
        public void Configure(EntityTypeBuilder<FichaPersonagemTag> entity)
        {
            entity.HasKey(x => new { x.FichaPersonagemId, x.Tag });

            entity.HasOne(x => x.FichaPersonagem)
                .WithMany(fp => fp.FichaPersonagemTags)
                .HasForeignKey(ft => ft.FichaPersonagemId);
        }
    }
}
