using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class ClasseDatabaseHelper
    {
        private const string CaminhoJson = "Data/classes.json";

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            var classes = await JsonLoaderHelper.CarregarAsync<List<Classe>>(CaminhoJson, "classes");
            if (classes == null || classes.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma classe encontrada no JSON.");
                return;
            }

            foreach (var classe in classes)
            {
                // 1) Insere a tabela principal Classe
                if (!await RegistroExisteAsync(connection, transaction, "Classe", classe.Id))
                {
                    var parametros = GerarParametrosEntidadeBase(classe);
                    parametros["Id"] = classe.Id;
                    parametros["DadoVida"] = classe.DadoVida ?? "";
                    parametros["PapelTatico"] = classe.PapelTatico ?? "";
                    parametros["IdHabilidadeConjuracao"] = classe.IdHabilidadeConjuracao ?? "";
                    parametros["UsaMagiaPreparada"] = classe.UsaMagiaPreparada ? 1 : 0;
                    parametros["QntOpcoesPericias"] = classe.QntOpcoesPericias;
                    parametros["QntOpcoesProficiencias"] = classe.QntOpcoesProficiencias;
                    parametros["Ouro"] = classe.Ouro;
                    parametros["FocoConjuracao"] = classe.FocoConjuracao;

                    await InserirEntidadeFilhaAsync(connection, transaction, "Classe", parametros);
                }

                // 2) Tags
                await InserirTagsAsync(connection, transaction, "ClasseTag", "ClasseId", classe.Id, classe.Tags);

                // 4) Opções de perícia (ClasseOpcaoPericia) com validação
                foreach (var op in classe.OpcoesPericias ?? Enumerable.Empty<ClasseOpcaoPericia>())
                {
                    if (string.IsNullOrWhiteSpace(op.PericiaId))
                    {
                        Console.WriteLine($"⚠️ Perícia opcional com ID nulo/whitespace (Classe: '{classe.Id}'). Ignorada.");
                        continue;
                    }

                    if (await RegistroExisteAsync(connection, transaction, "Pericia", op.PericiaId))
                    {
                        await InserirEntidadeFilhaAsync(
                            connection, transaction,
                            tabela: "ClasseOpcaoPericia",
                            new Dictionary<string, object>
                            {
                                ["ClasseId"] = classe.Id,
                                ["PericiaId"] = op.PericiaId
                            }
                        );
                    }
                    else
                    {
                        Console.WriteLine($"❌ Perícia opcional não encontrada: '{op.PericiaId}' (Classe: '{classe.Id}')");
                    }
                }

                // 5) Proficiências fixas (ClasseProficiencia)
                foreach (var prof in classe.Proficiencias ?? Enumerable.Empty<ClasseProficiencia>())
                {
                    if (string.IsNullOrWhiteSpace(prof.ProficienciaId))
                    {
                        Console.WriteLine($"⚠️ Proficiência fixa com ID nulo/whitespace (Classe: '{classe.Id}'). Ignorada.");
                        continue;
                    }

                    if (await RegistroExisteAsync(connection, transaction, "Proficiencia", prof.ProficienciaId))
                    {
                        await InserirEntidadeFilhaAsync(
                            connection, transaction,
                            tabela: "ClasseProficiencia",
                            new Dictionary<string, object>
                            {
                                ["ClasseId"] = classe.Id,
                                ["ProficienciaId"] = prof.ProficienciaId
                            }
                        );
                    }
                    else
                    {
                        Console.WriteLine($"❌ Proficiência fixa não encontrada: '{prof.ProficienciaId}' (Classe: '{classe.Id}')");
                    }
                }

                // 6) Opções de proficiência (ClasseOpcaoProficiencia)
                foreach (var prof in classe.OpcoesProficiencias ?? Enumerable.Empty<ClasseOpcaoProficiencia>())
                {
                    if (string.IsNullOrWhiteSpace(prof.ProficienciaId))
                    {
                        Console.WriteLine($"⚠️ Proficiência opcional com ID nulo/whitespace (Classe: '{classe.Id}'). Ignorada.");
                        continue;
                    }

                    if (await RegistroExisteAsync(connection, transaction, "Proficiencia", prof.ProficienciaId))
                    {
                        await InserirEntidadeFilhaAsync(
                            connection, transaction,
                            tabela: "ClasseOpcaoProficiencia",
                            new Dictionary<string, object>
                            {
                                ["ClasseId"] = classe.Id,
                                ["ProficienciaId"] = prof.ProficienciaId
                            }
                        );
                    }
                    else
                    {
                        Console.WriteLine($"❌ Proficiência opcional não encontrada: '{prof.ProficienciaId}' (Classe: '{classe.Id}')");
                    }
                }

                // 7) Salvaguardas (ClasseSalvaguarda)
                foreach (var salv in classe.IdSalvaguardas ?? new List<Atributo>())
                {
                    await InserirEntidadeFilhaAsync(
                        connection, transaction,
                        tabela: "ClasseSalvaguarda",
                        new Dictionary<string, object>
                        {
                            ["ClasseId"] = classe.Id,
                            ["IdSalvaguarda"] = salv.ToString().ToLowerInvariant()
                        }
                    );
                }


                // 8) Itens Fixos
                foreach (var item in classe.Itens ?? Enumerable.Empty<ClasseItemFixo>())
                {
                    if (string.IsNullOrWhiteSpace(item.ItemId))
                    {
                        Console.WriteLine($"⚠️ Item fixo com ID nulo/whitespace (Classe: '{classe.Id}'). Ignorado.");
                        continue;
                    }

                    if (await RegistroExisteAsync(connection, transaction, "Item", item.ItemId))
                    {
                        int? quantidade = item.Quantidade > 0 ? item.Quantidade : 1;  // padrão 1 caso Quantidade não seja >0

                        await InserirEntidadeFilhaAsync(
                            connection, transaction,
                            tabela: "ClasseItemFixo",
                            new Dictionary<string, object>
                            {
                                ["ClasseId"] = classe.Id,
                                ["ItemId"] = item.ItemId,
                                ["Quantidade"] = quantidade
                            }
                        );
                    }
                    else
                    {
                        Console.WriteLine($"❌ Item fixo não encontrado na tabela 'Item': '{item.ItemId}' (Classe: '{classe.Id}')");
                    }
                }



                // 9) Grupos de opções de itens
                for (int gi = 0; gi < (classe.ItensOpcoesBrutas?.Count ?? 0); gi++)
                {
                    var grupo = classe.ItensOpcoesBrutas[gi];

                    // insere grupo e obtém id gerado
                    var grupoId = await InserirEntidadeFilhaRetornandoIdAsync(
                        connection, transaction,
                        tabela: "ClasseOpcaoItemGrupo",
                        parametros: new Dictionary<string, object>
                        {
                            ["ClasseId"] = classe.Id,
                            ["Nome"] = $"Grupo {gi + 1}"
                        }
                    );

                    // para cada opção dentro do grupo
                    for (int oi = 0; oi < grupo.Count; oi++)
                    {
                        var opcao = grupo[oi];

                        var opcaoId = await InserirEntidadeFilhaRetornandoIdAsync(
                            connection, transaction,
                            tabela: "ClasseOpcaoItemOpcao",
                            parametros: new Dictionary<string, object>
                            {
                                ["GrupoId"] = grupoId,
                                ["Nome"] = opcao.Nome ?? $"Opção {oi + 1}"
                            }
                        );

                        // e cada item dessa opção
                        foreach (var itemId in opcao.Itens ?? Enumerable.Empty<string>())
                        {
                            await InserirEntidadeFilhaAsync(
                                connection, transaction,
                                tabela: "ClasseItemOpcaoItem",
                                new Dictionary<string, object>
                                {
                                    ["OpcaoId"] = opcaoId,
                                    ["ItemId"] = itemId
                                }
                            );
                        }
                    }
                }
            }

            Console.WriteLine("✅ Classes populadas com sucesso.");
        }
    }
}
