﻿using DnDBot.Bot.Models;
using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DnDBot.Bot.Data
{
    /// <summary>
    /// Contexto principal do Entity Framework Core para o sistema DnDBot.
    /// Representa a sessão com o banco de dados e fornece acesso às tabelas (DbSets).
    /// </summary>
    public class DnDBotDbContext : DbContext
    {
        /// <summary>
        /// Construtor padrão que aceita opções para configuração do contexto, 
        /// útil para injeção de dependência e configuração externa.
        /// </summary>
        /// <param name="options">Opções para configurar o contexto do EF Core.</param>
        public DnDBotDbContext(DbContextOptions<DnDBotDbContext> options)
            : base(options)
        {
        }

        // DbSets representam as tabelas do banco de dados para cada entidade.

        public DbSet<FichaPersonagem> FichaPersonagem { get; set; }
        public DbSet<Raca> Raca { get; set; }
        public DbSet<SubRaca> SubRaca { get; set; }
        public DbSet<SubRacaAlinhamento> SubRacaAlinhamento { get; set; }
        public DbSet<Alinhamento> Alinhamento { get; set; }
        public DbSet<Antecedente> Antecedente { get; set; }
        public DbSet<Proficiencia> Proficiencia { get; set; }
        public DbSet<Resistencia> Resistencia { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<Magia> Magia { get; set; }
        public DbSet<BonusAtributo> BonusAtributo { get; set; }
        public DbSet<Classe> Classe { get; set; }
        public DbSet<ClassePericia> ClassePericias { get; set; }
        public DbSet<ClasseProficiencia> ClasseProficienciasArmas { get; set; }
        public DbSet<ClasseSalvaguarda> ClasseSalvaguardas { get; set; }
        public DbSet<ClasseMagia> ClasseMagias { get; set; }
        public DbSet<Pericia> Pericia { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Idioma> Idioma { get; set; }
        public DbSet<AntecedenteItemOpcoes> AntecedenteItemOpcoes { get; set; }
        public DbSet<AntecedenteProficienciaOpcoes> AntecedenteProficienciaOpcoes { get; set; }


        // Lista normal (não DbSet) para requisitos de atributos de armas, talvez gerenciada separadamente
        public List<ArmaRequisitoAtributo> RequisitosAtributos { get; set; }

        /// <summary>
        /// Configurações adicionais do modelo e aplicação das configurações automáticas
        /// localizadas neste assembly (ex: arquivos de configuração IEntityTypeConfiguration).
        /// </summary>
        /// <param name="modelBuilder">Builder para configurar o modelo do EF Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama o comportamento padrão para não perder configurações base
            base.OnModelCreating(modelBuilder);

            // Aplica automaticamente todas as configurações de entidades presentes neste assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DnDBotDbContext).Assembly);

            modelBuilder.Entity<RacaTag>()
                .HasKey(rt => new { rt.RacaId, rt.Tag });

            modelBuilder.Entity<RacaTag>()
                .HasOne(rt => rt.Raca)
                .WithMany(r => r.RacaTags)
                .HasForeignKey(rt => rt.RacaId);

            modelBuilder.Entity<SubRacaTag>()
                .HasKey(rt => new { rt.SubRacaId, rt.Tag });

            modelBuilder.Entity<SubRacaTag>()
                .HasOne(rt => rt.SubRaca)
                .WithMany(r => r.SubRacaTags)
                .HasForeignKey(rt => rt.SubRacaId);

            modelBuilder.Entity<MagiaTag>()
    .HasKey(mt => new { mt.MagiaId, mt.Tag });

            modelBuilder.Entity<MagiaTag>()
                .HasOne(mt => mt.Magia)
                .WithMany(m => m.MagiaTags)
                .HasForeignKey(mt => mt.MagiaId);

            modelBuilder.Entity<ClasseTag>()
    .HasKey(ct => new { ct.ClasseId, ct.Tag });

            modelBuilder.Entity<ClasseTag>()
                .HasOne(ct => ct.Classe)
                .WithMany(c => c.ClasseTags)
                .HasForeignKey(ct => ct.ClasseId);

            modelBuilder.Entity<ArmaduraTag>()
    .HasKey(at => new { at.ArmaduraId, at.Tag });

            modelBuilder.Entity<ArmaduraTag>()
                .HasOne(at => at.Armadura)
                .WithMany(a => a.ArmaduraTags)
                .HasForeignKey(at => at.ArmaduraId);

            modelBuilder.Entity<ArmaTag>()
    .HasKey(at => new { at.ArmaId, at.Tag });

            modelBuilder.Entity<ArmaTag>()
                .HasOne(at => at.Arma)
                .WithMany(a => a.ArmaTags)
                .HasForeignKey(at => at.ArmaId);

            modelBuilder.Entity<FichaPersonagemTag>()
    .HasKey(ft => new { ft.FichaPersonagemId, ft.Tag });

            modelBuilder.Entity<FichaPersonagemTag>()
                .HasOne(ft => ft.FichaPersonagem)
                .WithMany(fp => fp.FichaPersonagemTags)
                .HasForeignKey(ft => ft.FichaPersonagemId);

            modelBuilder.Entity<AlinhamentoTag>()
    .HasKey(at => new { at.AlinhamentoId, at.Tag });

            modelBuilder.Entity<AlinhamentoTag>()
                .HasOne(at => at.Alinhamento)
                .WithMany(a => a.AlinhamentoTags)
                .HasForeignKey(at => at.AlinhamentoId);

            modelBuilder.Entity<FerramentaTag>()
                .HasKey(ft => new { ft.FerramentaId, ft.Tag });

            modelBuilder.Entity<FerramentaTag>()
                .HasOne(ft => ft.Ferramenta)
                .WithMany(f => f.FerramentaTags)
                .HasForeignKey(ft => ft.FerramentaId);

            modelBuilder.Entity<AntecedenteTag>()
    .HasKey(at => new { at.AntecedenteId, at.Tag });

            modelBuilder.Entity<AntecedenteTag>()
                .HasOne(at => at.Antecedente)
                .WithMany(a => a.AntecedenteTags)
                .HasForeignKey(at => at.AntecedenteId);

            // FichaPersonagem ↔ Caracteristica
            modelBuilder.Entity<FichaPersonagemCaracteristica>()
                .HasKey(x => new { x.FichaPersonagemId, x.CaracteristicaId });

            // FichaPersonagem ↔ Idioma
            modelBuilder.Entity<FichaPersonagemIdioma>()
                .HasKey(x => new { x.FichaPersonagemId, x.IdiomaId });

            // SubRaca ↔ Proficiencia
            modelBuilder.Entity<SubRacaProficiencia>()
                .HasKey(x => new { x.SubRacaId, x.ProficienciaId });

            modelBuilder.Entity<PropriedadesMagicas>()
                .HasKey(p => p.Id);

        }
    }
}
