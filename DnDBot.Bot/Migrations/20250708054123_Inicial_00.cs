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
                    IdiomasAdicionais = table.Column<int>(type: "INTEGER", nullable: false),
                    Requisitos = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Antecedente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DadoVida = table.Column<string>(type: "TEXT", nullable: true),
                    IdSalvaguardas = table.Column<string>(type: "TEXT", nullable: true),
                    PapelTatico = table.Column<string>(type: "TEXT", nullable: true),
                    IdHabilidadeConjuracao = table.Column<string>(type: "TEXT", nullable: true),
                    UsaMagiaPreparada = table.Column<bool>(type: "INTEGER", nullable: false),
                    SubclassePorNivel = table.Column<int>(type: "INTEGER", nullable: true),
                    IdItensIniciais = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Classe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcaoEscolha<Idioma>",
                columns: table => new
                {
                    QuantidadeEscolhas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OpcaoEscolha<Pericia>",
                columns: table => new
                {
                    QuantidadeEscolhas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Pericia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AtributoBase = table.Column<string>(type: "TEXT", nullable: false),
                    AtributosAlternativos = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    EhProficiente = table.Column<bool>(type: "INTEGER", nullable: false),
                    TemEspecializacao = table.Column<bool>(type: "INTEGER", nullable: false),
                    BonusBase = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_Pericia", x => x.Id);
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
                name: "Antecedente_RiquezaInicial",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecedente_RiquezaInicial", x => x.Tipo);
                    table.ForeignKey(
                        name: "FK_Antecedente_RiquezaInicial_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
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
                    table.ForeignKey(
                        name: "FK_Defeito_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_Ideal_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Idioma_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: true),
                    Empilhavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValorCobre = table.Column<int>(type: "INTEGER", nullable: false),
                    Equipavel = table.Column<bool>(type: "INTEGER", nullable: false),
                    RacasPermitidas = table.Column<string>(type: "TEXT", nullable: true),
                    AnatomiasPermitidas = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Arma_Tipo = table.Column<int>(type: "INTEGER", nullable: true),
                    DadoDano = table.Column<string>(type: "TEXT", nullable: true),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoDanoSecundario = table.Column<int>(type: "INTEGER", nullable: true),
                    Alcance = table.Column<int>(type: "INTEGER", nullable: true),
                    EhDuasMaos = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhLeve = table.Column<bool>(type: "INTEGER", nullable: true),
                    EhVersatil = table.Column<bool>(type: "INTEGER", nullable: true),
                    DadoDanoVersatil = table.Column<string>(type: "TEXT", nullable: true),
                    PodeSerArremessada = table.Column<bool>(type: "INTEGER", nullable: true),
                    AlcanceArremesso = table.Column<int>(type: "INTEGER", nullable: true),
                    Requisitos = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_PropriedadesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_BonusMagico = table.Column<int>(type: "INTEGER", nullable: true),
                    BonusContraTipos = table.Column<string>(type: "TEXT", nullable: true),
                    MagiasAssociadas = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_DurabilidadeAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    Arma_DurabilidadeMaxima = table.Column<int>(type: "INTEGER", nullable: true),
                    AreaAtaque = table.Column<string>(type: "TEXT", nullable: true),
                    TipoAcao = table.Column<string>(type: "TEXT", nullable: true),
                    RegraCritico = table.Column<string>(type: "TEXT", nullable: true),
                    TipoMunicao = table.Column<string>(type: "TEXT", nullable: true),
                    MunicaoPorAtaque = table.Column<int>(type: "INTEGER", nullable: true),
                    RequerRecarga = table.Column<bool>(type: "INTEGER", nullable: true),
                    TempoRecargaTurnos = table.Column<int>(type: "INTEGER", nullable: true),
                    AtaquesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    CustoReparo = table.Column<decimal>(type: "TEXT", nullable: true),
                    Arma_EMagica = table.Column<bool>(type: "INTEGER", nullable: true),
                    Arma_Raridade = table.Column<string>(type: "TEXT", nullable: true),
                    Arma_Fabricante = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: true),
                    ClasseArmadura = table.Column<int>(type: "INTEGER", nullable: true),
                    PermiteFurtividade = table.Column<bool>(type: "INTEGER", nullable: true),
                    PenalidadeFurtividade = table.Column<int>(type: "INTEGER", nullable: true),
                    Custo = table.Column<decimal>(type: "TEXT", nullable: true),
                    RequisitoForca = table.Column<int>(type: "INTEGER", nullable: true),
                    PropriedadesEspeciais = table.Column<string>(type: "TEXT", nullable: true),
                    DurabilidadeAtual = table.Column<int>(type: "INTEGER", nullable: true),
                    DurabilidadeMaxima = table.Column<int>(type: "INTEGER", nullable: true),
                    EMagica = table.Column<bool>(type: "INTEGER", nullable: true),
                    BonusMagico = table.Column<int>(type: "INTEGER", nullable: true),
                    Raridade = table.Column<string>(type: "TEXT", nullable: true),
                    Fabricante = table.Column<string>(type: "TEXT", nullable: true),
                    Material = table.Column<string>(type: "TEXT", nullable: true),
                    ResistenciasDano = table.Column<string>(type: "TEXT", nullable: true),
                    ImunidadesDano = table.Column<string>(type: "TEXT", nullable: true),
                    RequerProficiencia = table.Column<bool>(type: "INTEGER", nullable: true),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
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
                        name: "FK_Item_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_Vinculo_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Classe_RiquezaInicial",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe_RiquezaInicial", x => x.Tipo);
                    table.ForeignKey(
                        name: "FK_Classe_RiquezaInicial_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseProficienciasMulticlasse",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    IdProficiencia = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseProficienciasMulticlasse", x => new { x.ClasseId, x.IdProficiencia });
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasMulticlasse_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
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
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspacoMagiaPorNivel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspacoMagiaPorNivel_Classe_ClasseId",
                        column: x => x.ClasseId,
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
                name: "RequisitoMulticlasse",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    Atributo = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitoMulticlasse", x => new { x.ClasseId, x.Atributo });
                    table.ForeignKey(
                        name: "FK_RequisitoMulticlasse_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "AntecedentePericia",
                columns: table => new
                {
                    AntecedentesId = table.Column<string>(type: "TEXT", nullable: false),
                    PericiasId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedentePericia", x => new { x.AntecedentesId, x.PericiasId });
                    table.ForeignKey(
                        name: "FK_AntecedentePericia_Antecedente_AntecedentesId",
                        column: x => x.AntecedentesId,
                        principalTable: "Antecedente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedentePericia_Pericia_PericiasId",
                        column: x => x.PericiasId,
                        principalTable: "Pericia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classe_Pericia",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe_Pericia", x => new { x.ClasseId, x.PericiaId });
                    table.ForeignKey(
                        name: "FK_Classe_Pericia_Classe",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classe_Pericia_Pericia",
                        column: x => x.PericiaId,
                        principalTable: "Pericia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassePericias",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    PericiaId = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "FichaPersonagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JogadorId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    ResistenciaId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichaPersonagem_Resistencia_ResistenciaId",
                        column: x => x.ResistenciaId,
                        principalTable: "Resistencia",
                        principalColumn: "Id");
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
                name: "ClasseProficienciasArmaduras",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    ArmaduraId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseProficienciasArmaduras", x => new { x.ClasseId, x.ArmaduraId });
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmaduras_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmaduras_Item_ArmaduraId",
                        column: x => x.ArmaduraId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasseProficienciasArmas",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    ArmaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasseProficienciasArmas", x => new { x.ClasseId, x.ArmaId });
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmas_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasseProficienciasArmas_Item_ArmaId",
                        column: x => x.ArmaId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SubRaca_Idioma",
                columns: table => new
                {
                    IdiomaId = table.Column<string>(type: "TEXT", nullable: false),
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRaca_Idioma", x => new { x.IdiomaId, x.SubRacaId });
                    table.ForeignKey(
                        name: "FK_SubRaca_Idioma_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRaca_Idioma_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SubRacaResistencia",
                columns: table => new
                {
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRacaResistencia", x => new { x.SubRacaId, x.TipoDano });
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
                name: "BolsasDeMoedas",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolsasDeMoedas", x => x.FichaPersonagemId);
                    table.ForeignKey(
                        name: "FK_BolsasDeMoedas_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
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
                name: "Caracteristica",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    AcaoRequerida = table.Column<int>(type: "INTEGER", nullable: false),
                    Alvo = table.Column<int>(type: "INTEGER", nullable: false),
                    DuracaoEmRodadas = table.Column<int>(type: "INTEGER", nullable: true),
                    UsosPorDescansoCurto = table.Column<int>(type: "INTEGER", nullable: true),
                    UsosPorDescansoLongo = table.Column<int>(type: "INTEGER", nullable: true),
                    CondicaoAtivacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Origem = table.Column<int>(type: "INTEGER", nullable: false),
                    OrigemId = table.Column<string>(type: "TEXT", nullable: true),
                    NivelMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelMaximo = table.Column<int>(type: "INTEGER", nullable: true),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Caracteristica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caracteristica_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Caracteristica_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagem_Idiomas",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdiomaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagem_Idiomas", x => new { x.FichaPersonagemId, x.IdiomaId });
                    table.ForeignKey(
                        name: "FK_FichaPersonagem_Idiomas_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaPersonagem_Idiomas_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FichaPersonagemResistencia",
                columns: table => new
                {
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TipoDano = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaPersonagemResistencia", x => new { x.FichaPersonagemId, x.TipoDano });
                    table.ForeignKey(
                        name: "FK_FichaPersonagemResistencia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
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
                name: "Magia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<string>(type: "TEXT", nullable: true),
                    Escola = table.Column<string>(type: "TEXT", nullable: true),
                    TempoConjuracao = table.Column<string>(type: "TEXT", nullable: true),
                    Alcance = table.Column<string>(type: "TEXT", nullable: true),
                    Alvo = table.Column<string>(type: "TEXT", nullable: true),
                    Concentração = table.Column<bool>(type: "INTEGER", nullable: false),
                    Duracao = table.Column<string>(type: "TEXT", nullable: true),
                    PodeSerRitual = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteVerbal = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteSomatico = table.Column<bool>(type: "INTEGER", nullable: false),
                    ComponenteMaterial = table.Column<bool>(type: "INTEGER", nullable: false),
                    DetalhesMaterial = table.Column<string>(type: "TEXT", nullable: true),
                    ComponenteMaterialConsumido = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustoComponenteMaterial = table.Column<string>(type: "TEXT", nullable: true),
                    TipoDano = table.Column<string>(type: "TEXT", nullable: true),
                    DadoDano = table.Column<string>(type: "TEXT", nullable: true),
                    Escalonamento = table.Column<string>(type: "TEXT", nullable: true),
                    AtributoTesteResistencia = table.Column<string>(type: "TEXT", nullable: true),
                    MetadeNoTeste = table.Column<bool>(type: "INTEGER", nullable: false),
                    CondicoesAplicadas = table.Column<string>(type: "TEXT", nullable: true),
                    CondicoesRemovidas = table.Column<string>(type: "TEXT", nullable: true),
                    ClassesPermitidas = table.Column<string>(type: "TEXT", nullable: true),
                    Recarga = table.Column<string>(type: "TEXT", nullable: true),
                    TipoUso = table.Column<string>(type: "TEXT", nullable: true),
                    RequerLinhaDeVisao = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequerLinhaReta = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumeroMaximoAlvos = table.Column<int>(type: "INTEGER", nullable: true),
                    AreaEfeito = table.Column<string>(type: "TEXT", nullable: true),
                    FocoNecessario = table.Column<string>(type: "TEXT", nullable: true),
                    LimiteUso = table.Column<string>(type: "TEXT", nullable: true),
                    EfeitoPorTurno = table.Column<string>(type: "TEXT", nullable: true),
                    NumeroDeUsos = table.Column<int>(type: "INTEGER", nullable: false),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Magia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proficiencia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Proficiencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proficiencia_FichaPersonagem_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "FichaPersonagem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Moedas",
                columns: table => new
                {
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    FichaPersonagemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moedas", x => new { x.FichaPersonagemId, x.Tipo });
                    table.ForeignKey(
                        name: "FK_Moedas_BolsasDeMoedas_FichaPersonagemId",
                        column: x => x.FichaPersonagemId,
                        principalTable: "BolsasDeMoedas",
                        principalColumn: "FichaPersonagemId",
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
                name: "SubRaca_Caracteristicas",
                columns: table => new
                {
                    CaracteristicaId = table.Column<string>(type: "TEXT", nullable: false),
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRaca_Caracteristicas", x => new { x.CaracteristicaId, x.SubRacaId });
                    table.ForeignKey(
                        name: "FK_SubRaca_Caracteristicas_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRaca_Caracteristicas_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInventario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemBaseId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    AntecedenteId = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_ItemInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInventario_Antecedente_AntecedenteId",
                        column: x => x.AntecedenteId,
                        principalTable: "Antecedente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemInventario_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemInventario_Item_ItemBaseId",
                        column: x => x.ItemBaseId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClasseMagias",
                columns: table => new
                {
                    ClasseId = table.Column<string>(type: "TEXT", nullable: false),
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false)
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
                        name: "FK_ClasseMagias_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SubRaca_MagiasRaciais",
                columns: table => new
                {
                    MagiaId = table.Column<string>(type: "TEXT", nullable: false),
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRaca_MagiasRaciais", x => new { x.MagiaId, x.SubRacaId });
                    table.ForeignKey(
                        name: "FK_SubRaca_MagiasRaciais_Magia_MagiaId",
                        column: x => x.MagiaId,
                        principalTable: "Magia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRaca_MagiasRaciais_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRaca_Proficiencias",
                columns: table => new
                {
                    ProficienciaId = table.Column<string>(type: "TEXT", nullable: false),
                    SubRacaId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRaca_Proficiencias", x => new { x.ProficienciaId, x.SubRacaId });
                    table.ForeignKey(
                        name: "FK_SubRaca_Proficiencias_Proficiencia_ProficienciaId",
                        column: x => x.ProficienciaId,
                        principalTable: "Proficiencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubRaca_Proficiencias_SubRaca_SubRacaId",
                        column: x => x.SubRacaId,
                        principalTable: "SubRaca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_EquipamentoItem_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipamentoItem_ItemInventario_ItemInventarioId",
                        column: x => x.ItemInventarioId,
                        principalTable: "ItemInventario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Antecedente_RiquezaInicial_AntecedenteId",
                table: "Antecedente_RiquezaInicial",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentePericia_PericiasId",
                table: "AntecedentePericia",
                column: "PericiasId");

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
                name: "IX_Caracteristica_AntecedenteId",
                table: "Caracteristica",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristica_FichaPersonagemId",
                table: "Caracteristica",
                column: "FichaPersonagemId");

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
                name: "IX_Classe_Pericia_PericiaId",
                table: "Classe_Pericia",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Classe_RiquezaInicial_ClasseId",
                table: "Classe_RiquezaInicial",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseMagias_MagiaId",
                table: "ClasseMagias",
                column: "MagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassePericias_PericiaId",
                table: "ClassePericias",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseProficienciasArmaduras_ArmaduraId",
                table: "ClasseProficienciasArmaduras",
                column: "ArmaduraId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasseProficienciasArmas_ArmaId",
                table: "ClasseProficienciasArmas",
                column: "ArmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Defeito_AntecedenteId",
                table: "Defeito",
                column: "AntecedenteId");

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
                name: "IX_EspacoMagiaPorNivel_ClasseId",
                table: "EspacoMagiaPorNivel",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_FerramentaPericia_PericiaId",
                table: "FerramentaPericia",
                column: "PericiaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagem_ResistenciaId",
                table: "FichaPersonagem",
                column: "ResistenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_FichaPersonagem_Idiomas_IdiomaId",
                table: "FichaPersonagem_Idiomas",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideal_AntecedenteId",
                table: "Ideal",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Idioma_AntecedenteId",
                table: "Idioma",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_FichaPersonagemId",
                table: "Inventarios",
                column: "FichaPersonagemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_AntecedenteId",
                table: "Item",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventario_AntecedenteId",
                table: "ItemInventario",
                column: "AntecedenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventario_InventarioId",
                table: "ItemInventario",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventario_ItemBaseId",
                table: "ItemInventario",
                column: "ItemBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Magia_FichaPersonagemId",
                table: "Magia",
                column: "FichaPersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Proficiencia_FichaPersonagemId",
                table: "Proficiencia",
                column: "FichaPersonagemId");

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
                name: "IX_SubRaca_Caracteristicas_SubRacaId",
                table: "SubRaca_Caracteristicas",
                column: "SubRacaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRaca_Idioma_SubRacaId",
                table: "SubRaca_Idioma",
                column: "SubRacaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRaca_MagiasRaciais_SubRacaId",
                table: "SubRaca_MagiasRaciais",
                column: "SubRacaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRaca_Proficiencias_SubRacaId",
                table: "SubRaca_Proficiencias",
                column: "SubRacaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRacaAlinhamento_AlinhamentoId",
                table: "SubRacaAlinhamento",
                column: "AlinhamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vinculo_AntecedenteId",
                table: "Vinculo",
                column: "AntecedenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlinhamentoTag");

            migrationBuilder.DropTable(
                name: "Antecedente_RiquezaInicial");

            migrationBuilder.DropTable(
                name: "AntecedentePericia");

            migrationBuilder.DropTable(
                name: "AntecedenteTag");

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
                name: "Classe_Pericia");

            migrationBuilder.DropTable(
                name: "Classe_RiquezaInicial");

            migrationBuilder.DropTable(
                name: "ClasseMagias");

            migrationBuilder.DropTable(
                name: "ClassePericias");

            migrationBuilder.DropTable(
                name: "ClasseProficienciasArmaduras");

            migrationBuilder.DropTable(
                name: "ClasseProficienciasArmas");

            migrationBuilder.DropTable(
                name: "ClasseProficienciasMulticlasse");

            migrationBuilder.DropTable(
                name: "ClasseSalvaguardas");

            migrationBuilder.DropTable(
                name: "ClasseTag");

            migrationBuilder.DropTable(
                name: "Defeito");

            migrationBuilder.DropTable(
                name: "DificuldadePericia");

            migrationBuilder.DropTable(
                name: "EquipamentoItem");

            migrationBuilder.DropTable(
                name: "EspacoMagiaPorNivel");

            migrationBuilder.DropTable(
                name: "FerramentaPericia");

            migrationBuilder.DropTable(
                name: "FerramentaTag");

            migrationBuilder.DropTable(
                name: "FichaPersonagem_Idiomas");

            migrationBuilder.DropTable(
                name: "FichaPersonagemResistencia");

            migrationBuilder.DropTable(
                name: "FichaPersonagemTag");

            migrationBuilder.DropTable(
                name: "Ideal");

            migrationBuilder.DropTable(
                name: "MagiaTag");

            migrationBuilder.DropTable(
                name: "Moedas");

            migrationBuilder.DropTable(
                name: "OpcaoEscolha<Idioma>");

            migrationBuilder.DropTable(
                name: "OpcaoEscolha<Pericia>");

            migrationBuilder.DropTable(
                name: "QuantidadePorNivel");

            migrationBuilder.DropTable(
                name: "RacaTag");

            migrationBuilder.DropTable(
                name: "RequisitoMulticlasse");

            migrationBuilder.DropTable(
                name: "SubRaca_Caracteristicas");

            migrationBuilder.DropTable(
                name: "SubRaca_Idioma");

            migrationBuilder.DropTable(
                name: "SubRaca_MagiasRaciais");

            migrationBuilder.DropTable(
                name: "SubRaca_Proficiencias");

            migrationBuilder.DropTable(
                name: "SubRacaAlinhamento");

            migrationBuilder.DropTable(
                name: "SubRacaResistencia");

            migrationBuilder.DropTable(
                name: "SubRacaTag");

            migrationBuilder.DropTable(
                name: "Vinculo");

            migrationBuilder.DropTable(
                name: "Subclasse");

            migrationBuilder.DropTable(
                name: "ItemInventario");

            migrationBuilder.DropTable(
                name: "Pericia");

            migrationBuilder.DropTable(
                name: "BolsasDeMoedas");

            migrationBuilder.DropTable(
                name: "Caracteristica");

            migrationBuilder.DropTable(
                name: "Idioma");

            migrationBuilder.DropTable(
                name: "Magia");

            migrationBuilder.DropTable(
                name: "Proficiencia");

            migrationBuilder.DropTable(
                name: "Alinhamento");

            migrationBuilder.DropTable(
                name: "SubRaca");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Raca");

            migrationBuilder.DropTable(
                name: "FichaPersonagem");

            migrationBuilder.DropTable(
                name: "Antecedente");

            migrationBuilder.DropTable(
                name: "Resistencia");
        }
    }
}
