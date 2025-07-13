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

            // Inserir dados básicos da magia
            var parametros = GerarParametrosEntidadeBase(magia);
            parametros["Id"] = magia.Id;
            parametros["Nivel"] = magia.Nivel;
            parametros["Escola"] = magia.Escola.ToString();
            parametros["PodeSerRitual"] = magia.PodeSerRitual ? 1 : 0;
            parametros["ComponenteVerbal"] = magia.ComponenteVerbal ? 1 : 0;
            parametros["ComponenteSomatico"] = magia.ComponenteSomatico ? 1 : 0;
            parametros["ComponenteMaterial"] = magia.ComponenteMaterial ? 1 : 0;
            parametros["DetalhesMaterial"] = magia.DetalhesMaterial ?? "";
            parametros["ComponenteMaterialConsumido"] = magia.ComponenteMaterialConsumido ? 1 : 0;
            parametros["CustoComponenteMaterial"] = magia.CustoComponenteMaterial ?? "";

            await InserirEntidadeFilhaAsync(conn, tx, "Magia", parametros);

            // Inserir tags
            await InserirTagsAsync(conn, tx, "MagiaTag", "MagiaId", magia.Id, magia.Tags);

            // Inserir classes permitidas
            await InserirRelacionamentoSimplesAsync(conn,tx,"MagiaClassePermitida",new[] { "MagiaId", "Classe" },magia.ClassesPermitidas,c => new object[] { magia.Id, c.Classe.ToString() });

            // Inserir efeitos escalonados
            foreach (var efeito in magia.EfeitosEscalonados)
            {
                efeito.MagiaId = magia.Id;

                var parametrosEfeito = new Dictionary<string, object>
                {
                    ["Id"] = efeito.Id,
                    ["MagiaId"] = efeito.MagiaId,
                    ["NivelMinimo"] = efeito.NivelMinimo,
                    ["NivelMaximo"] = (object)efeito.NivelMaximo ?? DBNull.Value,
                    ["UsosPorDescansoCurto"] = (object)efeito.UsosPorDescansoCurto ?? DBNull.Value,
                    ["UsosPorDescansoLongo"] = (object)efeito.UsosPorDescansoLongo ?? DBNull.Value,
                    ["DuracaoEmRodadas"] = (object)efeito.DuracaoEmRodadas ?? DBNull.Value,
                    ["DescricaoEfeito"] = efeito.DescricaoEfeito ?? "",
                    ["AcaoRequerida"] = efeito.AcaoRequerida.ToString(),
                    ["CondicaoAtivacao"] = efeito.CondicaoAtivacao.ToString(),
                    ["FormaAreaEfeito"] = efeito.FormaAreaEfeito?.ToString() ?? "",
                    ["Alcance"] = efeito.Alcance.ToString(),
                    ["Alvo"] = efeito.Alvo.ToString(),
                    ["Concentracao"] = efeito.Concentracao ? 1 : 0,
                    ["DuracaoQuantidade"] = (object)efeito.DuracaoQuantidade ?? DBNull.Value,
                    ["DuracaoUnidade"] = efeito.DuracaoUnidade?.ToString() ?? "",
                    ["Recarga"] = efeito.Recarga.ToString(),
                    ["TipoUso"] = efeito.TipoUso.ToString(),
                    ["RequerLinhaDeVisao"] = efeito.RequerLinhaDeVisao ? 1 : 0,
                    ["RequerLinhaReta"] = efeito.RequerLinhaReta ? 1 : 0,
                    ["NumeroMaximoAlvos"] = (object)efeito.NumeroMaximoAlvos ?? DBNull.Value,
                    ["FocoNecessario"] = efeito.FocoNecessario ?? "",
                    ["AtributoTesteResistencia"] = efeito.AtributoTesteResistencia.ToString(),
                    ["MetadeNoTeste"] = efeito.MetadeNoTeste ? 1 : 0,
                    ["TempoConjuracao"] = efeito.TempoConjuracao.ToString()
                };

                await InserirEntidadeFilhaAsync(conn, tx, "EfeitoEscalonado", parametrosEfeito);

                // Inserir danos do efeito
                await InserirRelacionamentoSimplesAsync(conn,tx,"EfeitoDano",new[] { "Id", "EfeitoEscalonadoId", "DadoDano", "TipoDano" },efeito.Danos,dano => new object[] { dano.Id, efeito.Id, dano.DadoDano, dano.TipoDano.ToString() });

                // Inserir condições aplicadas
                await InserirRelacionamentoSimplesAsync(conn,tx,"MagiaCondicaoAplicada",new[] { "MagiaId", "Condicao", "EfeitoEscalonadoId" },efeito.CondicoesAplicadas,c => new object[] { magia.Id, c.Condicao, efeito.Id });

                // Inserir condições removidas
                await InserirRelacionamentoSimplesAsync(conn,tx,"MagiaCondicaoRemovida",new[] { "MagiaId", "Condicao", "EfeitoEscalonadoId" },efeito.CondicoesRemovidas,c => new object[] { magia.Id, c.Condicao, efeito.Id });
            }
        }

        Console.WriteLine("✅ Magias populadas.");
    }



    private static bool ValidarEnums(Magia magia, out string erro)
    {
        erro = "";

        if (!Enum.IsDefined(typeof(EscolaMagia), magia.Escola))
            erro += $"Escola inválida: {magia.Escola}. ";

        return string.IsNullOrWhiteSpace(erro);
    }
}
