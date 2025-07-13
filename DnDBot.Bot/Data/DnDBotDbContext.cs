using DnDBot.Bot.Models;
using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
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
        #region Personagem e Ficha
        public DbSet<FichaPersonagem> FichaPersonagem { get; set; }
        public DbSet<FichaPersonagemIdioma> FichaPersonagemIdioma { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        #endregion

        #region Raças e Sub-raças
        public DbSet<Raca> Raca { get; set; }
        public DbSet<RacaTag> RacaTag { get; set; }
        public DbSet<SubRaca> SubRaca { get; set; }
        public DbSet<SubRacaResistencia> SubRacaResistencia { get; set; }
        public DbSet<SubRacaIdioma> SubRacaIdioma { get; set; }
        public DbSet<SubRacaCaracteristica> SubRacaCaracteristica { get; set; }
        public DbSet<SubRacaProficiencia> SubRacaProficiencia { get; set; }
        public DbSet<SubRacaMagia> SubRacaMagia { get; set; }
        public DbSet<SubRacaAlinhamento> SubRacaAlinhamento { get; set; }
        #endregion

        #region Antecedentes
        public DbSet<Antecedente> Antecedente { get; set; }
        public DbSet<AntecedenteTag> AntecedenteTag { get; set; }
        public DbSet<AntecedenteProficiencia> AntecedenteProficiencia { get; set; }
        public DbSet<AntecedenteProficienciaOpcoes> AntecedenteProficienciaOpcoes { get; set; }
        public DbSet<AntecedenteItem> AntecedenteItem { get; set; }
        public DbSet<AntecedenteItemOpcoes> AntecedenteItemOpcoes { get; set; }
        public DbSet<AntecedenteCaracteristica> AntecedenteCaracteristica { get; set; }
        public DbSet<AntecedenteNarrativa> AntecedenteNarrativa { get; set; }
        #endregion

        #region Classes
        public DbSet<Classe> Classe { get; set; }
        public DbSet<ClasseTag> ClasseTag { get; set; }
        public DbSet<ClassePericia> ClassePericias { get; set; }
        public DbSet<ClasseProficiencia> ClasseProficienciasArmas { get; set; }
        public DbSet<ClasseSalvaguarda> ClasseSalvaguardas { get; set; }
        public DbSet<ClasseMagia> ClasseMagias { get; set; }
        #endregion

        #region Sistema de Regras
        public DbSet<Proficiencia> Proficiencia { get; set; }
        public DbSet<Resistencia> Resistencia { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<BonusAtributo> BonusAtributo { get; set; }
        public DbSet<Pericia> Pericia { get; set; }
        public DbSet<DificuldadePericia> DificuldadePericia { get; set; }
        public DbSet<Idioma> Idioma { get; set; }
        public DbSet<Alinhamento> Alinhamento { get; set; }
        public DbSet<AlinhamentoTag> AlinhamentoTag { get; set; }
        #endregion

        #region Magias
        public DbSet<Magia> Magia { get; set; }
        public DbSet<MagiaTag> MagiaTag { get; set; }
        #endregion

        #region Itens
        public DbSet<Item> Item { get; set; }
        #endregion

        #region Armas
        public DbSet<Arma> Arma { get; set; }
        public DbSet<ArmaTag> ArmaTag { get; set; }
        public DbSet<ArmaRequisitoAtributo> ArmaRequisitoAtributo { get; set; }
        #endregion

        #region Armaduras
        public DbSet<Armadura> Armadura { get; set; }
        public DbSet<ArmaduraTag> ArmaduraTag { get; set; }
        public DbSet<ArmaduraPropriedadeEspecial> ArmaduraPropriedadeEspecial { get; set; }
        public DbSet<ArmaduraResistencia> ArmaduraResistencia { get; set; }
        public DbSet<ArmaduraImunidade> ArmaduraImunidadeDano { get; set; }
        #endregion

        #region Ferramentas
        public DbSet<Ferramenta> Ferramenta { get; set; }
        public DbSet<FerramentaTag> FerramentaTag { get; set; }
        public DbSet<FerramentaPericia> FerramentaPericia { get; set; }
        #endregion

        #region Escudos
        public DbSet<Escudo> Escudo { get; set; }
        public DbSet<EscudoPropriedadeEspecial> EscudoPropriedadeEspecial { get; set; }
        #endregion

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

        }
    }
}
