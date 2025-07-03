using DnDBot.Application.Models;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Ficha.ArmaAuxiliares;
using DnDBot.Application.Models.Ficha.ClasseAuxiliares;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DnDBot.Application.Data
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
        public DbSet<Alinhamento> Alinhamento { get; set; }
        public DbSet<Antecedente> Antecedente { get; set; }
        public DbSet<Idioma> Idioma { get; set; }
        public DbSet<Proficiencia> Proficiencia { get; set; }
        public DbSet<Resistencia> Resistencia { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<Magia> Magia { get; set; }
        public DbSet<HistoricoFinanceiroItem> HistoricoFinanceiroItem { get; set; }
        public DbSet<BonusAtributo> BonusAtributo { get; set; }
        public DbSet<Classe> Classe { get; set; }
        public DbSet<ClassePericia> ClassePericias { get; set; }
        public DbSet<ClasseProficienciaArma> ClasseProficienciasArmas { get; set; }
        public DbSet<ClasseProficienciaArmadura> ClasseProficienciasArmaduras { get; set; }
        public DbSet<ClasseProficienciaMulticlasse> ClasseProficienciasMulticlasse { get; set; }
        public DbSet<ClasseSalvaguarda> ClasseSalvaguardas { get; set; }
        public DbSet<ClasseMagia> ClasseMagias { get; set; }
        public DbSet<Pericia> Pericia { get; set; }

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

            // Configurações para entidades que não possuem chave primária definida (tipos usados para consultas específicas)
            modelBuilder.Entity<OpcaoEscolha<Equipamento>>().HasNoKey();
            modelBuilder.Entity<OpcaoEscolha<Idioma>>().HasNoKey();
            modelBuilder.Entity<OpcaoEscolha<Pericia>>().HasNoKey();
        }
    }
}
