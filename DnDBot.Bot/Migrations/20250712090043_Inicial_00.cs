using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDBot.Bot.Migrations
{
    /// <inheritdoc />
    public partial class Inicial_00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alinhamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alinhamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Antecedente",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    QntOpcoesProficiencia = table.Column<int>(type: "INTEGER", nullable: false),
                    QntOpcoesItem = table.Column<int>(type: "INTEGER", nullable: false),
                    IdiomasAdicionais = table.Column<int>(type: "INTEGER", nullable: false),
                    Ouro = table.Column<int>(type: "INTEGER", nullable: false),
                    CaracteristicaIds = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Versao = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecedente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BolsaDeMoedas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolsaDeMoedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caracteristica",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    AcaoRequerida = table.Column<string>(type: "TEXT", nullable: false),
                    Alvo = table.Column<string>(type: "TEXT", nullable: false),
                    DuracaoEmRodadas = table.Column<int>(type: "INTEGER", nullable: true),
                    UsosPorDescansoCurto = table.Column<int>(type: "INTEGER", nullable: true),
                    UsosPorDescansoLongo = table.Column<int>(type: "INTEGER", nullable: true),
                    CondicaoAtivacao = table.Column<string>(type: "TEXT", nullable: false),
                    Origem = table.Column<string>(type: "TEXT", nullable: false),
                    OrigemId = table.Column<string>(type: "TEXT", nullable: true),
                    NivelMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelMaximo = table.Column<int>(type: "INTEGER", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Versao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DadoVida = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdSalvaguardas = table.Column<string>(type: "TEXT", nullable: true),
                    PapelTatico = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IdHabilidadeConjuracao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    UsaMagiaPreparada = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defeito",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defeito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ideal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idioma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    Escola = table.Column<int>(type: "INTEGER", nullable: false),
                    TempoConjuracao = table.Column<int>(type: "INTEGER", nullable: false),
                    Alcance = table.Column<int>(type: "INTEGER", nullable: false),
                    Alvo = table.Column<int>(type: "INTEGER", nullable: false),
                    Concentracao = table.Column<bool>(type: "INTEGER", nullable: false),
                    DuracaoQuantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    DuracaoUnidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PodeSerRitual = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteVerbal = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteSomatico = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteMaterial = table.Column<bool>(type: "INTEGER", nullable: false),
                    DetalhesMaterial = table.Column<string>(type: "TEXT", nullable: true),
                    ComponenteMaterialConsumido = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustoComponenteMaterial = table.Column<string>(type: "TEXT", nullable: true),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: false),
                    DadoDano = table.Column<string>(type: "TEXT", nullable: true),
                    Escalonamento = table.Column<string>(type: "TEXT", nullable: true),
                    AtributoTesteResistencia = table.Column<int>(type: "INTEGER", nullable: false),
                    MetadeNoTeste = table.Column<bool>(type: "INTEGER", nullable: false),
                    FormaAreaEfeito = table.Column<int>(type: "INTEGER", nullable: true),
                    Recarga = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoUso = table.Column<int>(type: "INTEGER", nullable: false),
                    RequerLinhaDeVisao = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequerLinhaReta = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumeroMaximoAlvos = table.Column<int>(type: "INTEGER", nullable: true),
                    AreaEfeito = table.Column<string>(type: "TEXT", nullable: true),
                    FocoNecessario = table.Column<string>(type: "TEXT", nullable: true),
                    LimiteUso = table.Column<string>(type: "TEXT", nullable: true),
                    EfeitoPorTurno = table.Column<string>(type: "TEXT", nullable: true),
                    NumeroDeUsos = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IgnoraCritico = table.Column<bool>(type: "INTEGER", nullable: false),
                    IgnoraDesvantagemFurtividade = table.Column<bool>(type: "INTEGER", nullable: false),
                    BonusCA = table.Column<int>(type: "INTEGER", nullable: false),
                    BonusAtaque = table.Column<int>(type: "INTEGER", nullable: false),
                    BonusDano = table.Column<int>(type: "INTEGER", nullable: false),
                    ResistenciasDano = table.Column<string>(type: "TEXT", nullable: true),
                    ImunidadesDano = table.Column<string>(type: "TEXT", nullable: true),
                    PesoRelativo = table.Column<double>(type: "REAL", nullable: false),
                    CustoMultiplicador = table.Column<decimal>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pericia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AtributoBase = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Versao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pericia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropriedadesMagicas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Raridade = table.Column<string>(type: "TEXT", nullable: true),
                    BonusMagico = table.Column<int>(type: "INTEGER", nullable: false),
                    Efeitos = table.Column<string>(type: "TEXT", nullable: true),
                    MagiasAssociadas = table.Column<string>(type: "TEXT", nullable: true),
                    CargasMaximas = table.Column<int>(type: "INTEGER", nullable: false),
                    CargasAtuais = table.Column<int>(type: "INTEGER", nullable: false),
                    UsosEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    EhConsumivel = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequerSintonizacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequisitosSintonizacao = table.Column<string>(type: "TEXT", nullable: true),
                    BonusContraTipos = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropriedadesMagicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raca",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resistencia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resistencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vinculo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlinhamentoTag",
                columns: table => new
                {
                    AlinhamentoId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlinhamentoTag", x => new { x.AlinhamentoId, x.Tag });
                    table.ForeignKey(
                        name: "FK_AlinhamentoTag_Alinhamento_AlinhamentoId",
                        column: x => x.AlinhamentoId,
                        principalTable: "Alinhamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteTag",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteTag", x => new { x.AntecedenteId, x.Tag });
                    table.ForeignKey(
                        name: "FK_AntecedenteTag_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moeda",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    BolsaDeMoedasId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moeda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moeda_BolsaDeMoedas_BolsaDeMoedasId",
                        column: x => x.BolsaDeMoedasId,
                        principalTable: "BolsaDeMoedas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteCaracteristica",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    CaracteristicaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteCaracteristica", x => new { x.AntecedenteId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_AntecedenteCaracteristica_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteCaracteristica_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseSalvaguardas",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    IdSalvaguarda = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseSalvaguardas", x => new { x.ClasseId, x.IdSalvaguarda });
                    table.ForeignKey(
                        name: "FK_ClasseSalvaguardas_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseTag",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseTag", x => new { x.ClasseId, x.Tag });
                    table.ForeignKey(
                        name: "FK_ClasseTag_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspacoMagiaPorNivel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId = table.Column<string>(type: "TEXT", nullable: true),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoMagia = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspacoMagiaPorNivel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspacoMagiaPorNivel_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EspacoMagiaPorNivel_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuantidadePorNivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClasseId = table.Column<string>(type: "TEXT", nullable: true),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantidadePorNivel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuantidadePorNivel_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuantidadePorNivel_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subclasse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subclasse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subclasse_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteDefeito",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    DefeitoId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteDefeito", x => new { x.AntecedenteId, x.DefeitoId });
                    table.ForeignKey(
                        name: "FK_AntecedenteDefeito_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteDefeito_Defeito_DefeitoId",
                        column: x => x.DefeitoId,
                        principalTable: "Defeito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteIdeal",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    IdealId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteIdeal", x => new { x.AntecedenteId, x.IdealId });
                    table.ForeignKey(
                        name: "FK_AntecedenteIdeal_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteIdeal_Ideal_IdealId",
                        column: x => x.IdealId,
                        principalTable: "Ideal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JogadorId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experiencia = table.Column<int>(type: "INTEGER", nullable: false),
                    RacaId = table.Column<string>(type: "TEXT", nullable: true),
                    SubracaId = table.Column<string>(type: "TEXT", nullable: true),
                    ClasseId = table.Column<string>(type: "TEXT", nullable: true),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
                    AlinhamentoId = table.Column<string>(type: "TEXT", nullable: true),
                    Forca = table.Column<int>(type: "INTEGER", nullable: false),
                    Destreza = table.Column<int>(type: "INTEGER", nullable: false),
                    Constituicao = table.Column<int>(type: "INTEGER", nullable: false),
                    Inteligencia = table.Column<int>(type: "INTEGER", nullable: false),
                    Sabedoria = table.Column<int>(type: "INTEGER", nullable: false),
                    Carisma = table.Column<int>(type: "INTEGER", nullable: false),
                    Tamanho = table.Column<string>(type: "TEXT", nullable: true),
                    Deslocamento = table.Column<int>(type: "INTEGER", nullable: false),
                    VisaoNoEscuro = table.Column<int>(type: "INTEGER", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    EstaAtivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    BolsaDeMoedasId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IdiomaId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaPersonagem_BolsaDeMoedas_BolsaDeMoedasId",
                        column: x => x.BolsaDeMoedasId,
                        principalTable: "BolsaDeMoedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FichaPersonagem_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClasseMagias",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseMagias", x => new { x.ClasseId, x.MagiaId });
                    table.ForeignKey(
                        name: "FK_ClasseMagias_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseMagias_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClasseMagias_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MagiaClassePermitida",
                columns: table => new
                {
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    Classe = table.Column<int>(type: "INTEGER", nullable: false),
                    MagiaId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagiaClassePermitida", x => new { x.MagiaId, x.Classe });
                    table.ForeignKey(
                        name: "FK_MagiaClassePermitida_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagiaClassePermitida_Magia_MagiaId1",
                        column: x => x.MagiaId1,
                        principalTable: "Magia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MagiaCondicaoAplicada",
                columns: table => new
                {
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    Condicao = table.Column<int>(type: "INTEGER", nullable: false),
                    MagiaId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagiaCondicaoAplicada", x => new { x.MagiaId, x.Condicao });
                    table.ForeignKey(
                        name: "FK_MagiaCondicaoAplicada_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagiaCondicaoAplicada_Magia_MagiaId1",
                        column: x => x.MagiaId1,
                        principalTable: "Magia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MagiaCondicaoRemovida",
                columns: table => new
                {
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    Condicao = table.Column<int>(type: "INTEGER", nullable: false),
                    MagiaId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagiaCondicaoRemovida", x => new { x.MagiaId, x.Condicao });
                    table.ForeignKey(
                        name: "FK_MagiaCondicaoRemovida_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagiaCondicaoRemovida_Magia_MagiaId1",
                        column: x => x.MagiaId1,
                        principalTable: "Magia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MagiaTag",
                columns: table => new
                {
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagiaTag", x => new { x.MagiaId, x.Tag });
                    table.ForeignKey(
                        name: "FK_MagiaTag_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassePericias",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassePericias", x => new { x.ClasseId, x.PericiaId });
                    table.ForeignKey(
                        name: "FK_ClassePericias_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassePericias_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassePericias_Pericia_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DificuldadePericia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: true),
                    PericiaId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DificuldadePericia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DificuldadePericia_Pericia_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DificuldadePericia_Pericia_PericiaId1",
                        column: x => x.PericiaId1,
                        principalTable: "Pericia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proficiencia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: true),
                    TemEspecializacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    BonusAdicional = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Versao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proficiencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proficiencia_Pericia_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategoria = table.Column<int>(type: "INTEGER", nullable: false),
                    Empilhavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValorCobre = table.Column<int>(type: "INTEGER", nullable: false),
                    Equipavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    AnatomiasPermitidas = table.Column<string>(type: "TEXT", nullable: true),
                    PropriedadesMagicasId = table.Column<string>(type: "TEXT", nullable: true),
                    Raridade = table.Column<int>(type: "INTEGER", nullable: false),
                    Fabricante = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    MaterialId = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoriaArma = table.Column<int>(type: "INTEGER", nullable: true),
                    DadoDano = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoDanoSecundario = table.Column<int>(type: "INTEGER", nullable: true),
                    EhDuasMaos = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhLeve = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhVersatil = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhAgil = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhPesada = table.Column<bool>(type: "INTEGER", nullable: true),
                    DadoDanoVersatil = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Requisitos = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_PropriedadesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    BonusContraTipos = table.Column<string>(type: "TEXT", nullable: true),
                    MagiasAssociadas = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_DurabilidadeAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    Arma_DurabilidadeMaxima = table.Column<int>(type: "INTEGER", nullable: true),
                    AreaAtaque = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoAcao = table.Column<int>(type: "INTEGER", nullable: true),
                    RegraCritico = table.Column<string>(type: "TEXT", nullable: true),
                    AtaquesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    AlcanceMaximo = table.Column<int>(type: "INTEGER", nullable: true),
                    AlcanceMinimo = table.Column<int>(type: "INTEGER", nullable: true),
                    MunicaoPorAtaque = table.Column<int>(type: "INTEGER", nullable: true),
                    RequerRecarga = table.Column<bool>(type: "INTEGER", nullable: true),
                    TempoRecargaTurnos = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoMunicao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    AlcanceEmMetros = table.Column<int>(type: "INTEGER", nullable: true),
                    PodeSerArremessada = table.Column<bool>(type: "INTEGER", nullable: true),
                    AlcanceArremesso = table.Column<int>(type: "INTEGER", nullable: true),
                    ClasseArmadura = table.Column<int>(type: "INTEGER", nullable: true),
                    ImpedeFurtividade = table.Column<bool>(type: "INTEGER", nullable: true),
                    BonusDestrezaMaximo = table.Column<int>(type: "INTEGER", nullable: true),
                    RequisitoForca = table.Column<int>(type: "INTEGER", nullable: true),
                    PropriedadesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    Armadura_DurabilidadeAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    Armadura_DurabilidadeMaxima = table.Column<int>(type: "INTEGER", nullable: true),
                    ResistenciasDano = table.Column<string>(type: "TEXT", nullable: true),
                    ImunidadesDano = table.Column<string>(type: "TEXT", nullable: true),
                    BonusCA = table.Column<int>(type: "INTEGER", nullable: true, defaultValue: 2),
                    DurabilidadeAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    DurabilidadeMaxima = table.Column<int>(type: "INTEGER", nullable: true),
                    PericiasIds = table.Column<string>(type: "TEXT", nullable: true),
                    RequerProficiencia = table.Column<bool>(type: "INTEGER", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_PropriedadesMagicas_PropriedadesMagicasId",
                        column: x => x.PropriedadesMagicasId,
                        principalTable: "PropriedadesMagicas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RacaTag",
                columns: table => new
                {
                    RacaId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacaTag", x => new { x.RacaId, x.Tag });
                    table.ForeignKey(
                        name: "FK_RacaTag_Raca_RacaId",
                        column: x => x.RacaId,
                        principalTable: "Raca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRaca",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tamanho = table.Column<int>(type: "INTEGER", nullable: false),
                    Deslocamento = table.Column<int>(type: "INTEGER", nullable: false),
                    VisaoNoEscuro = table.Column<int>(type: "INTEGER", nullable: false),
                    RacaId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubRaca_Raca_RacaId",
                        column: x => x.RacaId,
                        principalTable: "Raca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteVinculo",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    VinculoId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteVinculo", x => new { x.AntecedenteId, x.VinculoId });
                    table.ForeignKey(
                        name: "FK_AntecedenteVinculo_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteVinculo_Vinculo_VinculoId",
                        column: x => x.VinculoId,
                        principalTable: "Vinculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseMoeda",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    MoedaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseMoeda", x => new { x.ClasseId, x.MoedaId });
                    table.ForeignKey(
                        name: "FK_ClasseMoeda_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseMoeda_Moeda_MoedaId",
                        column: x => x.MoedaId,
                        principalTable: "Moeda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaracteristicaPorNivel",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    CaracteristicaId = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true),
                    SubclasseId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicaPorNivel", x => new { x.ClasseId, x.Nivel, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_CaracteristicaPorNivel_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaracteristicaPorNivel_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaracteristicaPorNivel_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaracteristicaPorNivel_Subclasse_SubclasseId",
                        column: x => x.SubclasseId,
                        principalTable: "Subclasse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemCaracteristica",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CaracteristicaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemCaracteristica", x => new { x.FichaPersonagemId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemCaracteristica_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagemCaracteristica_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemIdioma",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdiomaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemIdioma", x => new { x.FichaPersonagemId, x.IdiomaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemIdioma_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagemIdioma_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemMagia",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemMagia", x => new { x.FichaPersonagemId, x.MagiaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemMagia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagemMagia_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemResistencia",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ResistenciaId = table.Column<string>(type: "TEXT", nullable: false),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemResistencia", x => new { x.FichaPersonagemId, x.ResistenciaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemResistencia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagemResistencia_Resistencia_ResistenciaId",
                        column: x => x.ResistenciaId,
                        principalTable: "Resistencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemTag",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemTag", x => new { x.FichaPersonagemId, x.Tag });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemTag_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PesoMaximo = table.Column<double>(type: "REAL", nullable: false),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteProficiencia",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteProficiencia", x => new { x.AntecedenteId, x.ProficienciaId });
                    table.ForeignKey(
                        name: "FK_AntecedenteProficiencia_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteProficiencia_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteProficienciaOpcoes",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteProficienciaOpcoes", x => new { x.AntecedenteId, x.ProficienciaId });
                    table.ForeignKey(
                        name: "FK_AntecedenteProficienciaOpcoes_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteProficienciaOpcoes_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseProficienciasArmas",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseProficienciasArmas", x => new { x.ClasseId, x.ProficienciaId });
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmas_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmas_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmas_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemProficiencia",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false),
                    TemEspecializacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    BonusAdicional = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemProficiencia", x => new { x.FichaPersonagemId, x.ProficienciaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemProficiencia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagemProficiencia_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteItem",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteItem", x => new { x.AntecedenteId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_AntecedenteItem_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedenteItemOpcoes",
                columns: table => new
                {
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedenteItemOpcoes", x => new { x.AntecedenteId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_AntecedenteItemOpcoes_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedenteItemOpcoes_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArmaduraTag",
                columns: table => new
                {
                    ArmaduraId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmaduraTag", x => new { x.ArmaduraId, x.Tag });
                    table.ForeignKey(
                        name: "FK_ArmaduraTag_Item_ArmaduraId",
                        column: x => x.ArmaduraId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArmaRequisitoAtributo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArmaId = table.Column<string>(type: "TEXT", nullable: true),
                    Atributo = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmaRequisitoAtributo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArmaRequisitoAtributo_Item_ArmaId",
                        column: x => x.ArmaId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArmaTag",
                columns: table => new
                {
                    ArmaId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmaTag", x => new { x.ArmaId, x.Tag });
                    table.ForeignKey(
                        name: "FK_ArmaTag_Item_ArmaId",
                        column: x => x.ArmaId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseItens",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseItens", x => new { x.ClasseId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ClasseItens_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseItens_Classe_ClasseId1",
                        column: x => x.ClasseId1,
                        principalTable: "Classe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClasseItens_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscudoPropriedadeEspecial",
                columns: table => new
                {
                    EscudoId = table.Column<string>(type: "TEXT", nullable: false),
                    Propriedade = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EscudoId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscudoPropriedadeEspecial", x => new { x.EscudoId, x.Propriedade });
                    table.ForeignKey(
                        name: "FK_EscudoPropriedadeEspecial_Item_EscudoId",
                        column: x => x.EscudoId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscudoPropriedadeEspecial_Item_EscudoId1",
                        column: x => x.EscudoId1,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FerramentaPericia",
                columns: table => new
                {
                    FerramentaId = table.Column<string>(type: "TEXT", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FerramentaPericia", x => new { x.FerramentaId, x.PericiaId });
                    table.ForeignKey(
                        name: "FK_FerramentaPericia_Item_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FerramentaPericia_Pericia_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FerramentaTag",
                columns: table => new
                {
                    FerramentaId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FerramentaTag", x => new { x.FerramentaId, x.Tag });
                    table.ForeignKey(
                        name: "FK_FerramentaTag_Item_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemMaterial",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    MaterialId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMaterial", x => new { x.ItemId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_ItemMaterial_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMaterial_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemRaca",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    RacaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRaca", x => new { x.ItemId, x.RacaId });
                    table.ForeignKey(
                        name: "FK_ItemRaca_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemRaca_Raca_RacaId",
                        column: x => x.RacaId,
                        principalTable: "Raca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonusAtributos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Atributo = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false),
                    Origem = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    OwnerType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusAtributos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusAtributos_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BonusAtributos_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubRacaAlinhamento",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    AlinhamentoId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaAlinhamento", x => new { x.SubRacaId, x.AlinhamentoId });
                    table.ForeignKey(
                        name: "FK_SubRacaAlinhamento_Alinhamento_AlinhamentoId",
                        column: x => x.AlinhamentoId,
                        principalTable: "Alinhamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaAlinhamento_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaCaracteristica",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    CaracteristicaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaCaracteristica", x => new { x.SubRacaId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_SubRacaCaracteristica_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaCaracteristica_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaIdioma",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    IdiomaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaIdioma", x => new { x.SubRacaId, x.IdiomaId });
                    table.ForeignKey(
                        name: "FK_SubRacaIdioma_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaIdioma_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaMagia",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaMagia", x => new { x.SubRacaId, x.MagiaId });
                    table.ForeignKey(
                        name: "FK_SubRacaMagia_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaMagia_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaProficiencia",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaProficiencia", x => new { x.SubRacaId, x.ProficienciaId });
                    table.ForeignKey(
                        name: "FK_SubRacaProficiencia_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaProficiencia_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaResistencia",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    ResistenciaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaResistencia", x => new { x.SubRacaId, x.ResistenciaId });
                    table.ForeignKey(
                        name: "FK_SubRacaResistencia_Resistencia_ResistenciaId",
                        column: x => x.ResistenciaId,
                        principalTable: "Resistencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRacaResistencia_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRacaTag",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaTag", x => new { x.SubRacaId, x.Tag });
                    table.ForeignKey(
                        name: "FK_SubRacaTag_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventarioItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemBaseId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    InventarioId = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Fonte = table.Column<string>(type: "TEXT", nullable: true),
                    Pagina = table.Column<string>(type: "TEXT", nullable: true),
                    Versao = table.Column<string>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IconeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModificadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioItem_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventarioItem_Item_ItemBaseId",
                        column: x => x.ItemBaseId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipamentoItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Slot = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemInventarioId = table.Column<string>(type: "TEXT", nullable: true),
                    InventarioId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamentoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipamentoItem_InventarioItem_ItemInventarioId",
                        column: x => x.ItemInventarioId,
                        principalTable: "InventarioItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipamentoItem_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteCaracteristica_CaracteristicaId",
                table: "AntecedenteCaracteristica",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteDefeito_DefeitoId",
                table: "AntecedenteDefeito",
                column: "DefeitoId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteIdeal_IdealId",
                table: "AntecedenteIdeal",
                column: "IdealId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteItem_ItemId",
                table: "AntecedenteItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteItemOpcoes_ItemId",
                table: "AntecedenteItemOpcoes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteProficiencia_ProficienciaId",
                table: "AntecedenteProficiencia",
                column: "ProficienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteProficienciaOpcoes_ProficienciaId",
                table: "AntecedenteProficienciaOpcoes",
                column: "ProficienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedenteVinculo_VinculoId",
                table: "AntecedenteVinculo",
                column: "VinculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArmaRequisitoAtributo_ArmaId",
                table: "ArmaRequisitoAtributo",
                column: "ArmaId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusAtributos_FichaPersonagemId",
                table: "BonusAtributos",
                column: "FichaPersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusAtributos_SubRacaId",
                table: "BonusAtributos",
                column: "SubRacaId");

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicaPorNivel_CaracteristicaId",
                table: "CaracteristicaPorNivel",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicaPorNivel_ClasseId1",
                table: "CaracteristicaPorNivel",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicaPorNivel_SubclasseId",
                table: "CaracteristicaPorNivel",
                column: "SubclasseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseItens_ClasseId1",
                table: "ClasseItens",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseItens_ItemId",
                table: "ClasseItens",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseMagias_ClasseId1",
                table: "ClasseMagias",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseMagias_MagiaId",
                table: "ClasseMagias",
                column: "MagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseMoeda_MoedaId",
                table: "ClasseMoeda",
                column: "MoedaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassePericias_ClasseId1",
                table: "ClassePericias",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassePericias_PericiaId",
                table: "ClassePericias",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseProficienciasArmas_ClasseId1",
                table: "ClasseProficienciasArmas",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseProficienciasArmas_ProficienciaId",
                table: "ClasseProficienciasArmas",
                column: "ProficienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_DificuldadePericia_PericiaId",
                table: "DificuldadePericia",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_DificuldadePericia_PericiaId1",
                table: "DificuldadePericia",
                column: "PericiaId1");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentoItem_InventarioId",
                table: "EquipamentoItem",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentoItem_ItemInventarioId",
                table: "EquipamentoItem",
                column: "ItemInventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EscudoPropriedadeEspecial_EscudoId1",
                table: "EscudoPropriedadeEspecial",
                column: "EscudoId1");

            migrationBuilder.CreateIndex(
                name: "IX_EspacoMagiaPorNivel_ClasseId",
                table: "EspacoMagiaPorNivel",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_EspacoMagiaPorNivel_ClasseId1",
                table: "EspacoMagiaPorNivel",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_FerramentaPericia_PericiaId",
                table: "FerramentaPericia",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagem_BolsaDeMoedasId",
                table: "FichaPersonagem",
                column: "BolsaDeMoedasId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagem_IdiomaId",
                table: "FichaPersonagem",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagemCaracteristica_CaracteristicaId",
                table: "FichaPersonagemCaracteristica",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagemIdioma_IdiomaId",
                table: "FichaPersonagemIdioma",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagemMagia_MagiaId",
                table: "FichaPersonagemMagia",
                column: "MagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagemProficiencia_ProficienciaId",
                table: "FichaPersonagemProficiencia",
                column: "ProficienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagemResistencia_ResistenciaId",
                table: "FichaPersonagemResistencia",
                column: "ResistenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioItem_InventarioId",
                table: "InventarioItem",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioItem_ItemBaseId",
                table: "InventarioItem",
                column: "ItemBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_FichaPersonagemId",
                table: "Inventarios",
                column: "FichaPersonagemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_PropriedadesMagicasId",
                table: "Item",
                column: "PropriedadesMagicasId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMaterial_ItemId",
                table: "ItemMaterial",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemMaterial_MaterialId",
                table: "ItemMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRaca_RacaId",
                table: "ItemRaca",
                column: "RacaId");

            migrationBuilder.CreateIndex(
                name: "IX_MagiaClassePermitida_MagiaId1",
                table: "MagiaClassePermitida",
                column: "MagiaId1");

            migrationBuilder.CreateIndex(
                name: "IX_MagiaCondicaoAplicada_MagiaId1",
                table: "MagiaCondicaoAplicada",
                column: "MagiaId1");

            migrationBuilder.CreateIndex(
                name: "IX_MagiaCondicaoRemovida_MagiaId1",
                table: "MagiaCondicaoRemovida",
                column: "MagiaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Moeda_BolsaDeMoedasId",
                table: "Moeda",
                column: "BolsaDeMoedasId");

            migrationBuilder.CreateIndex(
                name: "IX_Proficiencia_PericiaId",
                table: "Proficiencia",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantidadePorNivel_ClasseId",
                table: "QuantidadePorNivel",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantidadePorNivel_ClasseId1",
                table: "QuantidadePorNivel",
                column: "ClasseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subclasse_ClasseId",
                table: "Subclasse",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRaca_RacaId",
                table: "SubRaca",
                column: "RacaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaAlinhamento_AlinhamentoId",
                table: "SubRacaAlinhamento",
                column: "AlinhamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaCaracteristica_CaracteristicaId",
                table: "SubRacaCaracteristica",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaIdioma_IdiomaId",
                table: "SubRacaIdioma",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaMagia_MagiaId",
                table: "SubRacaMagia",
                column: "MagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaProficiencia_ProficienciaId",
                table: "SubRacaProficiencia",
                column: "ProficienciaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaResistencia_ResistenciaId",
                table: "SubRacaResistencia",
                column: "ResistenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlinhamentoTag");

            migrationBuilder.DropTable(
                name: "AntecedenteCaracteristica");

            migrationBuilder.DropTable(
                name: "AntecedenteDefeito");

            migrationBuilder.DropTable(
                name: "AntecedenteIdeal");

            migrationBuilder.DropTable(
                name: "AntecedenteItem");

            migrationBuilder.DropTable(
                name: "AntecedenteItemOpcoes");

            migrationBuilder.DropTable(
                name: "AntecedenteProficiencia");

            migrationBuilder.DropTable(
                name: "AntecedenteProficienciaOpcoes");

            migrationBuilder.DropTable(
                name: "AntecedenteTag");

            migrationBuilder.DropTable(
                name: "AntecedenteVinculo");

            migrationBuilder.DropTable(
                name: "ArmaduraTag");

            migrationBuilder.DropTable(
                name: "ArmaRequisitoAtributo");

            migrationBuilder.DropTable(
                name: "ArmaTag");

            migrationBuilder.DropTable(
                name: "BonusAtributos");

            migrationBuilder.DropTable(
                name: "CaracteristicaPorNivel");

            migrationBuilder.DropTable(
                name: "ClasseItens");

            migrationBuilder.DropTable(
                name: "ClasseMagias");

            migrationBuilder.DropTable(
                name: "ClasseMoeda");

            migrationBuilder.DropTable(
                name: "ClassePericias");

            migrationBuilder.DropTable(
                name: "ClasseProficienciasArmas");

            migrationBuilder.DropTable(
                name: "ClasseSalvaguardas");

            migrationBuilder.DropTable(
                name: "ClasseTag");

            migrationBuilder.DropTable(
                name: "DificuldadePericia");

            migrationBuilder.DropTable(
                name: "EquipamentoItem");

            migrationBuilder.DropTable(
                name: "EscudoPropriedadeEspecial");

            migrationBuilder.DropTable(
                name: "EspacoMagiaPorNivel");

            migrationBuilder.DropTable(
                name: "FerramentaPericia");

            migrationBuilder.DropTable(
                name: "FerramentaTag");

            migrationBuilder.DropTable(
                name: "FichaPersonagemCaracteristica");

            migrationBuilder.DropTable(
                name: "FichaPersonagemIdioma");

            migrationBuilder.DropTable(
                name: "FichaPersonagemMagia");

            migrationBuilder.DropTable(
                name: "FichaPersonagemProficiencia");

            migrationBuilder.DropTable(
                name: "FichaPersonagemResistencia");

            migrationBuilder.DropTable(
                name: "FichaPersonagemTag");

            migrationBuilder.DropTable(
                name: "ItemMaterial");

            migrationBuilder.DropTable(
                name: "ItemRaca");

            migrationBuilder.DropTable(
                name: "MagiaClassePermitida");

            migrationBuilder.DropTable(
                name: "MagiaCondicaoAplicada");

            migrationBuilder.DropTable(
                name: "MagiaCondicaoRemovida");

            migrationBuilder.DropTable(
                name: "MagiaTag");

            migrationBuilder.DropTable(
                name: "QuantidadePorNivel");

            migrationBuilder.DropTable(
                name: "RacaTag");

            migrationBuilder.DropTable(
                name: "SubRacaAlinhamento");

            migrationBuilder.DropTable(
                name: "SubRacaCaracteristica");

            migrationBuilder.DropTable(
                name: "SubRacaIdioma");

            migrationBuilder.DropTable(
                name: "SubRacaMagia");

            migrationBuilder.DropTable(
                name: "SubRacaProficiencia");

            migrationBuilder.DropTable(
                name: "SubRacaResistencia");

            migrationBuilder.DropTable(
                name: "SubRacaTag");

            migrationBuilder.DropTable(
                name: "Defeito");

            migrationBuilder.DropTable(
                name: "Ideal");

            migrationBuilder.DropTable(
                name: "Antecedente");

            migrationBuilder.DropTable(
                name: "Vinculo");

            migrationBuilder.DropTable(
                name: "Subclasse");

            migrationBuilder.DropTable(
                name: "Moeda");

            migrationBuilder.DropTable(
                name: "InventarioItem");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Alinhamento");

            migrationBuilder.DropTable(
                name: "Caracteristica");

            migrationBuilder.DropTable(
                name: "Magia");

            migrationBuilder.DropTable(
                name: "Proficiencia");

            migrationBuilder.DropTable(
                name: "Resistencia");

            migrationBuilder.DropTable(
                name: "SubRaca");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Pericia");

            migrationBuilder.DropTable(
                name: "Raca");

            migrationBuilder.DropTable(
                name: "FichaPersonagem");

            migrationBuilder.DropTable(
                name: "PropriedadesMagicas");

            migrationBuilder.DropTable(
                name: "BolsaDeMoedas");

            migrationBuilder.DropTable(
                name: "Idioma");
        }
    }
}
