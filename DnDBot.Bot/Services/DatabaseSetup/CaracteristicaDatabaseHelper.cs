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
            Console.WriteLine("❌ Arquivo caracteristicas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de caracteristicas.json...");

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
            if (!await RegistroExisteAsync(conn, tx, "Caracteristica", c.Id))
            {
                var parametros = GerarParametrosEntidadeBase(c);

                // Campos adicionais
                parametros["tipo"] = c.Tipo.ToString();
                parametros["acao"] = c.AcaoRequerida.ToString();
                parametros["alvo"] = c.Alvo.ToString();
                parametros["duracao"] = c.DuracaoEmRodadas ?? (object)DBNull.Value;
                parametros["usosCurto"] = c.UsosPorDescansoCurto ?? (object)DBNull.Value;
                parametros["usosLongo"] = c.UsosPorDescansoLongo ?? (object)DBNull.Value;
                parametros["condicao"] = c.CondicaoAtivacao.ToString();
                parametros["origem"] = c.Origem.ToString();
                parametros["nivelMin"] = c.NivelMinimo;
                parametros["nivelMax"] = c.NivelMaximo ?? (object)DBNull.Value;

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
                    )";

                var cmd = CriarInsertCommand(conn, tx, sql, parametros);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        Console.WriteLine("✅ Características populadas.");
    }
}
