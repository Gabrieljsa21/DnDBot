using DnDBot.Application.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configura os relacionamentos entre a entidade Classe e várias outras entidades auxiliares,
    /// usando múltiplas implementações da interface IEntityTypeConfiguration para diferentes tipos.
    /// </summary>
    public class ClasseRelacionamentosConfiguration :
        IEntityTypeConfiguration<ClassePericia>,
        IEntityTypeConfiguration<ClasseProficienciaArma>,
        IEntityTypeConfiguration<ClasseProficienciaArmadura>,
        IEntityTypeConfiguration<ClasseProficienciaMulticlasse>,
        IEntityTypeConfiguration<ClasseSalvaguarda>,
        IEntityTypeConfiguration<ClasseMagia>
    {
        /// <summary>
        /// Configura o relacionamento muitos-para-muitos entre Classe e Pericia.
        /// Define chave primária composta e chaves estrangeiras.
        /// </summary>
        /// <param name="entity">Builder para configuração da entidade ClassePericia.</param>
        public void Configure(EntityTypeBuilder<ClassePericia> entity)
        {
            // Chave primária composta: ClasseId + PericiaId
            entity.HasKey(cp => new { cp.ClasseId, cp.PericiaId });

            // Relacionamento muitos-para-um com Classe
            entity.HasOne(cp => cp.Classe)
                  .WithMany()
                  .HasForeignKey(cp => cp.ClasseId);

            // Relacionamento muitos-para-um com Pericia
            entity.HasOne(cp => cp.Pericia)
                  .WithMany()
                  .HasForeignKey(cp => cp.PericiaId);
        }

        /// <summary>
        /// Configura a entidade ClasseProficienciaArma,
        /// definindo chave primária composta pelos campos ClasseId e ArmaId.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClasseProficienciaArma> entity)
        {
            entity.HasKey(ca => new { ca.ClasseId, ca.ArmaId });
        }

        /// <summary>
        /// Configura a entidade ClasseProficienciaArmadura,
        /// definindo chave primária composta pelos campos ClasseId e ArmaduraId.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClasseProficienciaArmadura> entity)
        {
            entity.HasKey(ca => new { ca.ClasseId, ca.ArmaduraId });
        }

        /// <summary>
        /// Configura a entidade ClasseProficienciaMulticlasse,
        /// definindo chave primária composta pelos campos ClasseId e IdProficiencia.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClasseProficienciaMulticlasse> entity)
        {
            entity.HasKey(cm => new { cm.ClasseId, cm.IdProficiencia });
        }

        /// <summary>
        /// Configura a entidade ClasseSalvaguarda,
        /// definindo chave primária composta pelos campos ClasseId e IdSalvaguarda.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClasseSalvaguarda> entity)
        {
            entity.HasKey(cs => new { cs.ClasseId, cs.IdSalvaguarda });
        }

        /// <summary>
        /// Configura a entidade ClasseMagia,
        /// definindo chave primária composta pelos campos ClasseId e MagiaId.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClasseMagia> entity)
        {
            entity.HasKey(cm => new { cm.ClasseId, cm.MagiaId });
        }
    }
}
