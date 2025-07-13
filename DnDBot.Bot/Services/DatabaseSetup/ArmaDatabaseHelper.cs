using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class ArmaDatabaseHelper
{
    private const string CaminhoJsonArmaCorpo = "Data/armascorpoacorpo.json";
    private const string CaminhoJsonArmaDistancia = "Data/armasadistancia.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        await PopularArmasAsync(connection, transaction, CaminhoJsonArmaCorpo, "corpo a corpo");
        await PopularArmasAsync(connection, transaction, CaminhoJsonArmaDistancia, "à distância");
    }

    private static async Task PopularArmasAsync(SqliteConnection connection, SqliteTransaction transaction, string caminhoJson, string tipo)
    {

        var listaArmas = await JsonLoaderHelper.CarregarAsync<List<Arma>>(caminhoJson, $"armas {tipo}.json");

        foreach (var arma in listaArmas)
        {
            var parametrosArma = new Dictionary<string, object>
            {
                ["Id"] = arma.Id,
                ["Tipo"] = (int)arma.Tipo,
                ["CategoriaArma"] = (int)arma.CategoriaArma,
                ["DadoDano"] = arma.DadoDano,
                ["TipoDano"] = (int)arma.TipoDano,
                ["TipoDanoSecundario"] = arma.TipoDanoSecundario.HasValue ? (object)(int)arma.TipoDanoSecundario.Value : DBNull.Value,
                ["EhDuasMaos"] = arma.EhDuasMaos ? 1 : 0,
                ["EhLeve"] = arma.EhLeve ? 1 : 0,
                ["EhVersatil"] = arma.EhVersatil ? 1 : 0,
                ["EhAgil"] = arma.EhAgil ? 1 : 0,
                ["EhPesada"] = arma.EhPesada ? 1 : 0,
                ["DadoDanoVersatil"] = string.IsNullOrEmpty(arma.DadoDanoVersatil) ? DBNull.Value : arma.DadoDanoVersatil,
                ["DurabilidadeAtual"] = arma.DurabilidadeAtual,
                ["DurabilidadeMaxima"] = arma.DurabilidadeMaxima,
                ["AreaAtaque"] = (int)arma.AreaAtaque,
                ["TipoAcao"] = (int)arma.TipoAcao,
                ["RegraCritico"] = arma.RegraCritico ?? string.Empty,
                ["AlcanceMinimo"] = arma.AlcanceMinimo ?? 0,
                ["AlcanceMaximo"] = arma.AlcanceMaximo,
                ["TipoMunicao"] = arma.TipoMunicao ?? string.Empty,
                ["MunicaoPorAtaque"] = arma.MunicaoPorAtaque,
                ["RequerRecarga"] = arma.RequerRecarga ? 1 : 0,
                ["TempoRecargaTurnos"] = arma.TempoRecargaTurnos,
                ["PodeSerArremessada"] = arma.PodeSerArremessada ? 1 : 0,
                ["AlcanceArremesso"] = arma.AlcanceArremesso ?? 0
            };

            await ItemDatabaseHelper.InserirItem(connection, transaction, arma);
            await SqliteHelper.InserirEntidadeFilhaAsync(connection, transaction, "Arma", parametrosArma);
            await SqliteHelper.InserirTagsAsync(connection, transaction, "ArmaTag", "ArmaId", arma.Id, arma.Tags);
            await SqliteHelper.InserirRelacionamentoSimplesAsync(connection, transaction, "ArmaRequisitoAtributo", new[] { "ArmaId", "AtributoId", "ValorMinimo" }, arma.RequisitosAtributos, r => new object[] { arma.Id, r.Atributo, r.Valor });
        }

        Console.WriteLine($"✅ Armas {tipo} populadas.");
    }
}
