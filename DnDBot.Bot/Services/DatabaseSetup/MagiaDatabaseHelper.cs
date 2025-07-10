using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

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
                Concentracao INTEGER,
                DuracaoQuantidade TEXT,
                DuracaoUnidade TEXT,
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

            ["MagiaTags"] = @"
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
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        var magias = JsonSerializer.Deserialize<List<Magia>>(json, options);

        if (magias == null || magias.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma magia encontrada.");
            return;
        }

        foreach (var magia in magias)
        {
            bool valido = true;

            // Exemplo para validar TipoDano (substitua pelo seu enum)
            if (!Enum.IsDefined(typeof(TipoDano), magia.TipoDano))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem TipoDano inválido: '{magia.TipoDano}'");
                valido = false;
            }

            // Validar outros enums usados da mesma forma
            if (!Enum.IsDefined(typeof(NivelMagia), magia.Nivel))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem Nivel inválido: '{magia.Nivel}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(EscolaMagia), magia.Escola))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem Escola inválida: '{magia.Escola}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(TipoUsoMagia), magia.TempoConjuracao))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem TempoConjuracao inválido: '{magia.TempoConjuracao}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(TipoAlcance), magia.Alcance))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem Alcance inválido: '{magia.Alcance}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(Alvo), magia.Alvo))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem Alvo inválido: '{magia.Alvo}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(Atributo), magia.AtributoTesteResistencia))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem AtributoTesteResistencia inválido: '{magia.AtributoTesteResistencia}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(RecargaMagia), magia.Recarga))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem Recarga inválida: '{magia.Recarga}'");
                valido = false;
            }

            if (!Enum.IsDefined(typeof(TipoUsoMagia), magia.TipoUso))
            {
                Console.WriteLine($"⚠ Magia '{magia.Nome}' tem TipoUso inválido: '{magia.TipoUso}'");
                valido = false;
            }

            if (!valido)
            {
                Console.WriteLine($"⛔ Ignorando magia '{magia.Nome}' devido a dados inválidos.");
                continue; // pula essa magia
            }

            // continua com inserção no banco se válido
            if (await RegistroExisteAsync(conn, tx, "Magia", magia.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(magia);
            parametros["nivel"] = magia.Nivel.ToString();
            parametros["escola"] = magia.Escola.ToString();
            parametros["tempoConjuracao"] = magia.TempoConjuracao.ToString();
            parametros["alcance"] = magia.Alcance.ToString();
            parametros["alvo"] = magia.Alvo.ToString();
            parametros["concentracao"] = magia.Concentracao ? 1 : 0;
            parametros["duracaoQuantidade"] = magia.DuracaoQuantidade;
            parametros["duracaoUnidade"] = magia.DuracaoUnidade.ToString();
            parametros["podeSerRitual"] = magia.PodeSerRitual ? 1 : 0;
            parametros["componenteVerbal"] = magia.ComponenteVerbal ? 1 : 0;
            parametros["componenteSomatico"] = magia.ComponenteSomatico ? 1 : 0;
            parametros["componenteMaterial"] = magia.ComponenteMaterial ? 1 : 0;
            parametros["detalhesMaterial"] = magia.DetalhesMaterial ?? "";
            parametros["componenteMaterialConsumido"] = magia.ComponenteMaterialConsumido ? 1 : 0;
            parametros["custoComponenteMaterial"] = magia.CustoComponenteMaterial ?? "";
            parametros["tipoDano"] = magia.TipoDano.ToString();
            parametros["dadoDano"] = magia.DadoDano ?? "";
            parametros["escalonamento"] = magia.Escalonamento ?? "";
            parametros["atributoTesteResistencia"] = magia.AtributoTesteResistencia.ToString();
            parametros["metadeNoTeste"] = magia.MetadeNoTeste ? 1 : 0;
            parametros["recarga"] = magia.Recarga.ToString();
            parametros["tipoUso"] = magia.TipoUso.ToString();
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
                Id, Nivel, Escola, TempoConjuracao, Alcance, Alvo, Concentracao, DuracaoQuantidade, DuracaoUnidade, PodeSerRitual,
                ComponenteVerbal, ComponenteSomatico, ComponenteMaterial, DetalhesMaterial, ComponenteMaterialConsumido,
                CustoComponenteMaterial, TipoDano, DadoDano, Escalonamento, AtributoTesteResistencia, MetadeNoTeste,
                Recarga, TipoUso, RequerLinhaDeVisao, RequerLinhaReta, NumeroMaximoAlvos, AreaEfeito,
                FocoNecessario, LimiteUso, EfeitoPorTurno, NumeroDeUsos,
                {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
            )
            VALUES (
                $id, $nivel, $escola, $tempoConjuracao, $alcance, $alvo, $concentracao, $duracaoQuantidade, $duracaoUnidade, $podeSerRitual,
                $componenteVerbal, $componenteSomatico, $componenteMaterial, $detalhesMaterial, $componenteMaterialConsumido,
                $custoComponenteMaterial, $tipoDano, $dadoDano, $escalonamento, $atributoTesteResistencia, $metadeNoTeste,
                $recarga, $tipoUso, $requerLinhaDeVisao, $requerLinhaReta, $numeroMaximoAlvos, $areaEfeito,
                $focoNecessario, $limiteUso, $efeitoPorTurno, $numeroDeUsos,
                {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
            );";

            var insertCmd = CriarInsertCommand(conn, tx, sql, parametros);
            await insertCmd.ExecuteNonQueryAsync();

            await InserirTagsAsync(conn, tx, "MagiaTags", "MagiaId", magia.Id, magia.Tags);
        }

        Console.WriteLine("✅ Magias populadas.");
    }

}
