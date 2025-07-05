using DnDBot.Application.Helpers;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;

public static class MagiaDatabaseHelper
{
    private const string CaminhoJson = "Data/magias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Magia"] = @"
                Id TEXT PRIMARY KEY,
                Nivel TEXT,
                Escola TEXT,
                TempoConjuracao TEXT,
                Alcance TEXT,
                Alvo TEXT,
                Concentração INTEGER,
                Duracao TEXT,
                PodeSerRitual INTEGER,
                ComponenteVerbal INTEGER,
                ComponenteSomatico INTEGER,
                ComponenteMaterial INTEGER,
                DetalhesMaterial TEXT,
                ComponenteMaterialConsumido INTEGER,
                CustoComponenteMaterial TEXT,
                TipoDano TEXT,
                DadoDano TEXT,
                Escalonamento TEXT,
                AtributoTesteResistencia TEXT,
                MetadeNoTeste INTEGER,
                Recarga TEXT,
                TipoUso TEXT,
                RequerLinhaDeVisao INTEGER,
                RequerLinhaReta INTEGER,
                NumeroMaximoAlvos INTEGER,
                AreaEfeito TEXT,
                FocoNecessario TEXT,
                LimiteUso TEXT,
                EfeitoPorTurno TEXT,
                NumeroDeUsos INTEGER,
                " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

            ["Magia_Tags"] = @"
                MagiaId TEXT NOT NULL,
                Tag TEXT NOT NULL,
                PRIMARY KEY (MagiaId, Tag),
                FOREIGN KEY (MagiaId) REFERENCES Magia(Id) ON DELETE CASCADE"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo magias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo magias do JSON...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var magias = JsonSerializer.Deserialize<List<Magia>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (magias == null || magias.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma magia encontrada.");
            return;
        }

        foreach (var magia in magias)
        {
            if (await RegistroExisteAsync(conn, tx, "Magia", magia.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(magia);
            parametros["nivel"] = magia.Nivel ?? "";
            parametros["escola"] = magia.Escola ?? "";
            parametros["tempoConjuracao"] = magia.TempoConjuracao ?? "";
            parametros["alcance"] = magia.Alcance ?? "";
            parametros["alvo"] = magia.Alvo ?? "";
            parametros["concentracao"] = magia.Concentração ? 1 : 0;
            parametros["duracao"] = magia.Duracao ?? "";
            parametros["podeSerRitual"] = magia.PodeSerRitual ? 1 : 0;
            parametros["componenteVerbal"] = magia.ComponenteVerbal ? 1 : 0;
            parametros["componenteSomatico"] = magia.ComponenteSomatico ? 1 : 0;
            parametros["componenteMaterial"] = magia.ComponenteMaterial ? 1 : 0;
            parametros["detalhesMaterial"] = magia.DetalhesMaterial ?? "";
            parametros["componenteMaterialConsumido"] = magia.ComponenteMaterialConsumido ? 1 : 0;
            parametros["custoComponenteMaterial"] = magia.CustoComponenteMaterial ?? "";
            parametros["tipoDano"] = magia.TipoDano ?? "";
            parametros["dadoDano"] = magia.DadoDano ?? "";
            parametros["escalonamento"] = magia.Escalonamento ?? "";
            parametros["atributoTesteResistencia"] = magia.AtributoTesteResistencia ?? "";
            parametros["metadeNoTeste"] = magia.MetadeNoTeste ? 1 : 0;
            parametros["recarga"] = magia.Recarga ?? "";
            parametros["tipoUso"] = magia.TipoUso ?? "";
            parametros["requerLinhaDeVisao"] = magia.RequerLinhaDeVisao ? 1 : 0;
            parametros["requerLinhaReta"] = magia.RequerLinhaReta ? 1 : 0;
            parametros["numeroMaximoAlvos"] = magia.NumeroMaximoAlvos ?? (object)DBNull.Value;
            parametros["areaEfeito"] = magia.AreaEfeito ?? "";
            parametros["focoNecessario"] = magia.FocoNecessario ?? "";
            parametros["limiteUso"] = magia.LimiteUso ?? "";
            parametros["efeitoPorTurno"] = magia.EfeitoPorTurno ?? "";
            parametros["numeroDeUsos"] = magia.NumeroDeUsos;

            var sql = $@"
                INSERT INTO Magia (
                    Id, Nivel, Escola, TempoConjuracao, Alcance, Alvo, Concentração, Duracao, PodeSerRitual,
                    ComponenteVerbal, ComponenteSomatico, ComponenteMaterial, DetalhesMaterial, ComponenteMaterialConsumido,
                    CustoComponenteMaterial, TipoDano, DadoDano, Escalonamento, AtributoTesteResistencia, MetadeNoTeste,
                    Recarga, TipoUso, RequerLinhaDeVisao, RequerLinhaReta, NumeroMaximoAlvos, AreaEfeito,
                    FocoNecessario, LimiteUso, EfeitoPorTurno, NumeroDeUsos,
                    {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
                )
                VALUES (
                    $id, $nivel, $escola, $tempoConjuracao, $alcance, $alvo, $concentracao, $duracao, $podeSerRitual,
                    $componenteVerbal, $componenteSomatico, $componenteMaterial, $detalhesMaterial, $componenteMaterialConsumido,
                    $custoComponenteMaterial, $tipoDano, $dadoDano, $escalonamento, $atributoTesteResistencia, $metadeNoTeste,
                    $recarga, $tipoUso, $requerLinhaDeVisao, $requerLinhaReta, $numeroMaximoAlvos, $areaEfeito,
                    $focoNecessario, $limiteUso, $efeitoPorTurno, $numeroDeUsos,
                    {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
                );";

            var insertCmd = CriarInsertCommand(conn, tx, sql, parametros);
            await insertCmd.ExecuteNonQueryAsync();

            await InserirTagsAsync(conn, tx, "Magia_Tags", "MagiaId", magia.Id, magia.Tags);
        }

        Console.WriteLine("✅ Magias populadas.");
    }
}
