using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class MagiaDatabaseHelper
{
    private const string CaminhoJson = "Data/magias.json";

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        var magias = await JsonLoaderHelper.CarregarAsync<List<Magia>>(CaminhoJson, "magias", options);

        if (magias == null || magias.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma magia encontrada no JSON.");
            return;
        }

        foreach (var magia in magias)
        {
            if (!ValidarEnums(magia, out string mensagemErro))
            {
                Console.WriteLine($"⛔ Ignorando magia '{magia.Nome}': {mensagemErro}");
                continue;
            }

            if (await RegistroExisteAsync(conn, tx, "Magia", magia.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(magia);
            parametros["Id"] = magia.Id;
            parametros["Nivel"] = magia.Nivel.ToString();
            parametros["Escola"] = magia.Escola.ToString();
            parametros["TempoConjuracao"] = magia.TempoConjuracao.ToString();
            parametros["Alcance"] = magia.Alcance.ToString();
            parametros["Alvo"] = magia.Alvo.ToString();
            parametros["Concentracao"] = magia.Concentracao ? 1 : 0;
            parametros["DuracaoQuantidade"] = magia.DuracaoQuantidade;
            parametros["DuracaoUnidade"] = magia.DuracaoUnidade.ToString();
            parametros["PodeSerRitual"] = magia.PodeSerRitual ? 1 : 0;
            parametros["ComponenteVerbal"] = magia.ComponenteVerbal ? 1 : 0;
            parametros["ComponenteSomatico"] = magia.ComponenteSomatico ? 1 : 0;
            parametros["ComponenteMaterial"] = magia.ComponenteMaterial ? 1 : 0;
            parametros["DetalhesMaterial"] = magia.DetalhesMaterial ?? "";
            parametros["ComponenteMaterialConsumido"] = magia.ComponenteMaterialConsumido ? 1 : 0;
            parametros["CustoComponenteMaterial"] = magia.CustoComponenteMaterial ?? "";
            parametros["TipoDano"] = magia.TipoDano.ToString();
            parametros["DadoDano"] = magia.DadoDano ?? "";
            parametros["Escalonamento"] = magia.Escalonamento ?? "";
            parametros["AtributoTesteResistencia"] = magia.AtributoTesteResistencia.ToString();
            parametros["MetadeNoTeste"] = magia.MetadeNoTeste ? 1 : 0;
            parametros["Recarga"] = magia.Recarga.ToString();
            parametros["TipoUso"] = magia.TipoUso.ToString();
            parametros["RequerLinhaDeVisao"] = magia.RequerLinhaDeVisao ? 1 : 0;
            parametros["RequerLinhaReta"] = magia.RequerLinhaReta ? 1 : 0;
            parametros["NumeroMaximoAlvos"] = magia.NumeroMaximoAlvos ?? (object)DBNull.Value;
            parametros["AreaEfeito"] = magia.AreaEfeito ?? "";
            parametros["FocoNecessario"] = magia.FocoNecessario ?? "";
            parametros["LimiteUso"] = magia.LimiteUso ?? "";
            parametros["EfeitoPorTurno"] = magia.EfeitoPorTurno ?? "";
            parametros["NumeroDeUsos"] = magia.NumeroDeUsos;

            await InserirEntidadeFilhaAsync(conn, tx, "Magia", parametros);
            await InserirTagsAsync(conn, tx, "MagiaTag", "MagiaId", magia.Id, magia.Tags);
        }

        Console.WriteLine("✅ Magias populadas.");
    }

    private static bool ValidarEnums(Magia magia, out string erro)
    {
        erro = "";

        if (!Enum.IsDefined(typeof(TipoDano), magia.TipoDano))
            erro += $"TipoDano inválido: {magia.TipoDano}. ";

        if (!Enum.IsDefined(typeof(NivelMagia), magia.Nivel))
            erro += $"Nivel inválido: {magia.Nivel}. ";

        if (!Enum.IsDefined(typeof(EscolaMagia), magia.Escola))
            erro += $"Escola inválida: {magia.Escola}. ";

        if (!Enum.IsDefined(typeof(TipoUsoMagia), magia.TempoConjuracao))
            erro += $"TempoConjuracao inválido: {magia.TempoConjuracao}. ";

        if (!Enum.IsDefined(typeof(TipoAlcance), magia.Alcance))
            erro += $"Alcance inválido: {magia.Alcance}. ";

        if (!Enum.IsDefined(typeof(Alvo), magia.Alvo))
            erro += $"Alvo inválido: {magia.Alvo}. ";

        if (!Enum.IsDefined(typeof(Atributo), magia.AtributoTesteResistencia))
            erro += $"AtributoTesteResistencia inválido: {magia.AtributoTesteResistencia}. ";

        if (!Enum.IsDefined(typeof(RecargaMagia), magia.Recarga))
            erro += $"Recarga inválida: {magia.Recarga}. ";

        if (!Enum.IsDefined(typeof(TipoUsoMagia), magia.TipoUso))
            erro += $"TipoUso inválido: {magia.TipoUso}. ";

        return string.IsNullOrWhiteSpace(erro);
    }
}
