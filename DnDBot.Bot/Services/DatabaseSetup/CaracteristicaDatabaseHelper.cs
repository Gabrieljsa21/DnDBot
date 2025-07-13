using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class CaracteristicaDatabaseHelper
{
    private const string CaminhoJson = "Data/caracteristicasraciais.json";

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        var caracteristicas = await JsonLoaderHelper.CarregarAsync<Dictionary<string, Caracteristica>>(CaminhoJson, "características raciais");

        if (caracteristicas == null || caracteristicas.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma característica encontrada no arquivo.");
            return;
        }

        foreach (var caracteristica in caracteristicas.Values)
        {
            await InserirCaracteristica(conn, tx, caracteristica);
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

        parametros["id"] = caracteristica.Id;
        parametros["tipo"] = caracteristica.Tipo.ToString();
        parametros["acaoRequerida"] = caracteristica.AcaoRequerida.ToString();
        parametros["alvo"] = caracteristica.Alvo.ToString();
        parametros["duracaoEmRodadas"] = caracteristica.DuracaoEmRodadas ?? (object)DBNull.Value;
        parametros["usosPorDescansoCurto"] = caracteristica.UsosPorDescansoCurto ?? (object)DBNull.Value;
        parametros["usosPorDescansoLongo"] = caracteristica.UsosPorDescansoLongo ?? (object)DBNull.Value;
        parametros["condicaoAtivacao"] = caracteristica.CondicaoAtivacao.ToString();
        parametros["origem"] = caracteristica.Origem.ToString();
        parametros["nivelMinimo"] = caracteristica.NivelMinimo;
        parametros["nivelMaximo"] = caracteristica.NivelMaximo ?? (object)DBNull.Value;

        await InserirEntidadeFilhaAsync(conn, tx, "Caracteristica", parametros);
    }
}
