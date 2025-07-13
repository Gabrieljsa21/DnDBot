using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models;
using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class AntecedenteDatabaseHelper
{
    private const string CaminhoJson = "Data/antecedentes.json";
    private const string CaminhoJsonAntecedenteItens = "Data/antecedenteitens.json";
    private const string CaminhoJsonAntecedenteOpcoesItens = "Data/antecedenteopcoesitem.json";
    private const string CaminhoJsonAntecedenteOpcoesProficiencias = "Data/antecedenteopcoesproficiencia.json";
    private const string CaminhoJsonAntecedenteNarrativas = "Data/antecedentenarrativa.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var antecedentes = await JsonLoaderHelper.CarregarAsync<List<Antecedente>>(CaminhoJson, "antecedentes.json");
        var itensFixosDict = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonAntecedenteItens, "antecedenteitens.json");
        var opcoesItensDict = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonAntecedenteOpcoesItens, "antecedenteopcoesitem.json");
        var opcoesProficienciasDict = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonAntecedenteOpcoesProficiencias, "antecedenteopcoesproficiencia.json");
        var narrativas = await JsonLoaderHelper.CarregarAsync<List<AntecedenteNarrativa>>(CaminhoJsonAntecedenteNarrativas, "antecedentenarrativa.json") ?? new();

        if (antecedentes == null) return;

        foreach (var antecedente in antecedentes)
        {
            if (!await RegistroExisteAsync(connection, transaction, "Antecedente", antecedente.Id))
            {
                var parametros = GerarParametrosEntidadeBase(antecedente);
                parametros["IdiomasAdicionais"] = antecedente.IdiomasAdicionais;
                parametros["QntOpcoesItem"] = antecedente.QntOpcoesItem;
                parametros["QntOpcoesProficiencia"] = antecedente.QntOpcoesProficiencia;
                parametros["Ouro"] = antecedente.Ouro;

                try
                {
                    await InserirEntidadeFilhaAsync(connection, transaction, "Antecedente", parametros);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao inserir antecedente '{antecedente.Id}': {ex.Message}");
                }
            }

            // Características
            var idsCaracteristicas = antecedente.Caracteristicas
                .Select(x => x.CaracteristicaId)
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .ToList();

            var idsExistentes = new List<string>();
            foreach (var id in idsCaracteristicas)
            {
                if (await RegistroExisteAsync(connection, transaction, "Caracteristica", id))
                    idsExistentes.Add(id);
                else
                    Console.WriteLine($"⚠ Característica não encontrada: '{id}' para antecedente '{antecedente.Id}'");
            }

            await InserirRelacionamentoSimplesAsync(connection,transaction,"AntecedenteCaracteristica",new[] { "AntecedenteId", "CaracteristicaId" },idsExistentes,id => new object[] { antecedente.Id, id }
);

            if (antecedente.Tags?.Any() == true)
                await InserirTagsAsync(connection, transaction, "AntecedenteTag", "AntecedenteId", antecedente.Id, antecedente.Tags);

            if (itensFixosDict != null && itensFixosDict.TryGetValue(antecedente.Id, out var itensFixos))
                await InserirRelacionamentoSimplesAsync(connection, transaction, "AntecedenteItem", new[] { "AntecedenteId", "ItemId" }, itensFixos, itemId => new object[] { antecedente.Id, itemId });

            if (opcoesItensDict != null && opcoesItensDict.TryGetValue(antecedente.Id, out var itens))
                await InserirRelacionamentoSimplesAsync(connection, transaction, "AntecedenteItemOpcoes", new[] { "AntecedenteId", "ItemId" }, itens, itemId => new object[] { antecedente.Id, itemId });

            if (antecedente.Proficiencias?.Any() == true)
                await InserirRelacionamentoSimplesAsync(connection, transaction, "AntecedenteProficiencia", new[] { "AntecedenteId", "ProficienciaId" }, antecedente.Proficiencias, p => new object[] { antecedente.Id, p.ProficienciaId });

            if (opcoesProficienciasDict != null && opcoesProficienciasDict.TryGetValue(antecedente.Id, out var proficiencias))
                await InserirRelacionamentoSimplesAsync(connection, transaction, "AntecedenteProficienciaOpcoes", new[] { "AntecedenteId", "ProficienciaId" }, proficiencias, profId => new object[] { antecedente.Id, profId });


            // Narrativas (Ideais, Vínculos, Defeitos)
            var narrativasDoAntecedente = narrativas
                .Where(n => n.AntecedenteId == antecedente.Id)
                .ToList();

            foreach (var narrativa in narrativasDoAntecedente)
            {
                var parametrosNarrativa = new Dictionary<string, object>
                {
                    ["Id"] = narrativa.Id,
                    ["AntecedenteId"] = narrativa.AntecedenteId,
                    ["Descricao"] = narrativa.Descricao,
                    ["Tipo"] = (int)narrativa.Tipo,

                };
                // Insere a narrativa
                await InserirEntidadeFilhaAsync(connection, transaction, "AntecedenteNarrativa", parametrosNarrativa);

                // Insere as tags da narrativa
                if (narrativa.Tags?.Any() == true)
                {
                    await InserirTagsAsync(connection,transaction,"AntecedenteNarrativaTag","AntecedenteNarrativaId",narrativa.Id,narrativa.Tags);
                }
            }
        }

        Console.WriteLine("✅ Antecedentes populados.");
    }
}
