using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
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
        parametros["origem"] = caracteristica.Origem.ToString();
        parametros["origemId"] = caracteristica.OrigemId ?? (object)DBNull.Value;

        await InserirEntidadeFilhaAsync(conn, tx, "Caracteristica", parametros);

        // Agora insere as escalas que estão na lista EscalasPorNivel
        foreach (var escala in caracteristica.EscalasPorNivel)
        {
            if (string.IsNullOrEmpty(escala.Id))
                escala.Id = Guid.NewGuid().ToString();

            await InserirCaracteristicaEscala(conn, tx, caracteristica.Id, escala);
        }
    }

    private static async Task InserirCaracteristicaEscala(SqliteConnection conn, SqliteTransaction tx, string caracteristicaId, CaracteristicaEscala escala)
    {
        var parametros = new Dictionary<string, object>
        {
            ["id"] = escala.Id,
            ["nivelMinimo"] = escala.NivelMinimo,
            ["nivelMaximo"] = escala.NivelMaximo ?? (object)DBNull.Value,
            ["usosPorDescansoCurto"] = escala.UsosPorDescansoCurto ?? (object)DBNull.Value,
            ["usosPorDescansoLongo"] = escala.UsosPorDescansoLongo ?? (object)DBNull.Value,
            ["duracaoEmRodadas"] = escala.DuracaoEmRodadas ?? (object)DBNull.Value,
            ["acaoRequerida"] = escala.AcaoRequerida.ToString(),
            ["alvo"] = escala.Alvo.ToString(),
            ["condicaoAtivacao"] = escala.CondicaoAtivacao.ToString(),
            ["caracteristicaId"] = caracteristicaId
        };

        int escalaId = await SqliteHelper.InserirEntidadeFilhaRetornandoIdAsync(conn, tx, "CaracteristicaEscala", parametros);

        foreach (var dano in escala.Danos)
        {
            var danoParams = new Dictionary<string, object>
            {
                ["dadoDano"] = dano.DadoDano,
                ["tipoDano"] = dano.TipoDano.ToString(),
                ["caracteristicaEscalaId"] = escalaId
            };

            await InserirEntidadeFilhaAsync(conn, tx, "CaracteristicaEscalaDano", danoParams);
        }
    }

}
