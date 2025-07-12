using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class CaracteristicaDatabaseHelper
{
    private const string CaminhoJson = "Data/caracteristicasraciais.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicao = @"
            Tipo TEXT NOT NULL,
            AcaoRequerida TEXT NOT NULL,
            Alvo TEXT NOT NULL,
            DuracaoEmRodadas INTEGER,
            UsosPorDescansoCurto INTEGER,
            UsosPorDescansoLongo INTEGER,
            CondicaoAtivacao TEXT NOT NULL,
            Origem TEXT NOT NULL,
            NivelMinimo INTEGER NOT NULL,
            NivelMaximo INTEGER,
            " + SqliteEntidadeBaseHelper.Campos;

        await SqliteHelper.CriarTabelaAsync(cmd, "Caracteristica", definicao);
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo caracteristicasraciais.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de caracteristicasraciais.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var caracteristicas = JsonSerializer.Deserialize<Dictionary<string, Caracteristica>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (caracteristicas == null || caracteristicas.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma característica encontrada no arquivo.");
            return;
        }

        foreach (var c in caracteristicas.Values)
        {
            await InserirCaracteristica(conn, tx, c);
        }

        Console.WriteLine("✅ Características populadas.");
    }
    public static async Task InserirCaracteristica(SqliteConnection conn, SqliteTransaction tx, Caracteristica caracteristica)
    {
        if (string.IsNullOrWhiteSpace(caracteristica?.Id))
            return;

        if (await RegistroExisteAsync(conn, tx, "Caracteristica", caracteristica.Id))
            return;

        var parametros = GerarParametrosEntidadeBase(caracteristica);

        parametros["tipo"] = caracteristica.Tipo.ToString();
        parametros["acao"] = caracteristica.AcaoRequerida.ToString();
        parametros["alvo"] = caracteristica.Alvo.ToString();
        parametros["duracao"] = caracteristica.DuracaoEmRodadas ?? (object)DBNull.Value;
        parametros["usosCurto"] = caracteristica.UsosPorDescansoCurto ?? (object)DBNull.Value;
        parametros["usosLongo"] = caracteristica.UsosPorDescansoLongo ?? (object)DBNull.Value;
        parametros["condicao"] = caracteristica.CondicaoAtivacao.ToString();
        parametros["origem"] = caracteristica.Origem.ToString();
        parametros["nivelMin"] = caracteristica.NivelMinimo;
        parametros["nivelMax"] = caracteristica.NivelMaximo ?? (object)DBNull.Value;

        var sql = $@"
            INSERT INTO Caracteristica (
                Id, Tipo, AcaoRequerida, Alvo, DuracaoEmRodadas,
                UsosPorDescansoCurto, UsosPorDescansoLongo, CondicaoAtivacao,
                Origem, NivelMinimo, NivelMaximo,
                {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
            ) VALUES (
                $id, $tipo, $acao, $alvo, $duracao,
                $usosCurto, $usosLongo, $condicao,
                $origem, $nivelMin, $nivelMax,
                {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
            );";

        await CriarInsertCommand(conn, tx, sql, parametros).ExecuteNonQueryAsync();
    }
}