using DnDBot.Application.Helpers;
using DnDBot.Application.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;
using static System.Net.Mime.MediaTypeNames;

public static class ArmaDatabaseHelper
{
    private const string CaminhoJson = "Data/armas.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        // Monta definição da tabela Arma sem repetir campos do EntidadeBase (que já estão em SqliteEntidadeBaseHelper.Campos)
        var definicaoArma = string.Join(",\n", new[]
        {
                    "Id TEXT PRIMARY KEY",
            "Tipo INTEGER NOT NULL",
            "Categoria INTEGER NOT NULL",
            "DadoDano TEXT",
            "TipoDano INTEGER NOT NULL",
            "TipoDanoSecundario INTEGER",
            "Peso REAL",
            "Custo DECIMAL",
            "Alcance INTEGER",
            "EhDuasMaos INTEGER",
            "EhLeve INTEGER",
            "EhVersatil INTEGER",
            "DadoDanoVersatil TEXT",
            "PodeSerArremessada INTEGER",
            "AlcanceArremesso INTEGER",
            "BonusMagico INTEGER",
            "DurabilidadeAtual INTEGER",
            "DurabilidadeMaxima INTEGER",
            "Raridade TEXT",
            "Fabricante TEXT",
            SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim().TrimEnd(',')
        });

        var definicoes = new Dictionary<string, string>
        {
            ["Arma"] = definicaoArma,
            ["Arma_Requisitos"] = @"
                ArmaId TEXT NOT NULL,
                Requisito TEXT NOT NULL,
                PRIMARY KEY (ArmaId, Requisito),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            ",
            ["Arma_Tags"] = @"
                ArmaId TEXT NOT NULL,
                Tag TEXT NOT NULL,
                PRIMARY KEY (ArmaId, Tag),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            ",
            ["Arma_PropriedadesEspeciais"] = @"
                ArmaId TEXT NOT NULL,
                Propriedade TEXT NOT NULL,
                PRIMARY KEY (ArmaId, Propriedade),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            ",
            ["Arma_BonusContraTipos"] = @"
                ArmaId TEXT NOT NULL,
                TipoContra TEXT NOT NULL,
                PRIMARY KEY (ArmaId, TipoContra),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            ",
            ["Arma_MagiasAssociadas"] = @"
                ArmaId TEXT NOT NULL,
                MagiaId TEXT NOT NULL,
                PRIMARY KEY (ArmaId, MagiaId),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            ",
            ["Arma_RequisitosAtributos"] = @"
                ArmaId TEXT NOT NULL,
                Atributo INTEGER NOT NULL,
                Valor INTEGER NOT NULL,
                PRIMARY KEY (ArmaId, Atributo),
                FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
            "
        };

        foreach (var tabela in definicoes)
        {
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo armas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de armas.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var armas = JsonSerializer.Deserialize<List<Arma>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (armas == null || armas.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma arma encontrada no JSON.");
            return;
        }

        foreach (var arma in armas)
        {
            await InserirArma(connection, transaction, arma);
            await SqliteHelper.InserirTagsAsync(connection, transaction, "ArmaTag", "ArmaId", arma.Id, arma.Tags);

            await InserirListaTexto(connection, transaction, "Arma_Requisitos", "Requisito", arma.Id, arma.Requisitos);
            await InserirListaTexto(connection, transaction, "Arma_Tags", "Tag", arma.Id, arma.Tags);
            await InserirListaTexto(connection, transaction, "Arma_PropriedadesEspeciais", "Propriedade", arma.Id, arma.PropriedadesEspeciais);
            await InserirListaTexto(connection, transaction, "Arma_BonusContraTipos", "TipoContra", arma.Id, arma.BonusContraTipos);
            await InserirListaTexto(connection, transaction, "Arma_MagiasAssociadas", "MagiaId", arma.Id, arma.MagiasAssociadas);

            if (arma.RequisitosAtributos != null)
            {
                foreach (var r in arma.RequisitosAtributos)
                {
                    var cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"
                        INSERT OR IGNORE INTO Arma_RequisitosAtributos (ArmaId, Atributo, Valor)
                        VALUES ($armaId, $atributo, $valor)";
                    cmd.Parameters.AddWithValue("$armaId", arma.Id);
                    cmd.Parameters.AddWithValue("$atributo", (int)r.Atributo);
                    cmd.Parameters.AddWithValue("$valor", r.Valor);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        Console.WriteLine("✅ Armas e dados relacionados populados.");
    }

    private static async Task InserirArma(SqliteConnection conn, SqliteTransaction tx, Arma arma)
    {
        if (await SqliteHelper.RegistroExisteAsync(conn, tx, "Arma", arma.Id))
            return;

        var parametros = GerarParametrosArma(arma);

        var sql = $@"
            INSERT INTO Arma (
                {SqliteEntidadeBaseHelper.CamposInsert},
                Tipo, Categoria, DadoDano, TipoDano, TipoDanoSecundario, Peso, Custo, Alcance,
                EhDuasMaos, EhLeve, EhVersatil, DadoDanoVersatil, PodeSerArremessada, AlcanceArremesso,
                BonusMagico, DurabilidadeAtual, DurabilidadeMaxima, Raridade, Fabricante
            ) VALUES (
                {SqliteEntidadeBaseHelper.ValoresInsert},
                $tipo, $categoria, $dadoDano, $tipoDano, $tipoDanoSecundario, $peso, $custo, $alcance,
                $ehDuasMaos, $ehLeve, $ehVersatil, $dadoDanoVersatil, $podeSerArremessada, $alcanceArremesso,
                $bonusMagico, $durabilidadeAtual, $durabilidadeMaxima, $raridade, $fabricante
            )";

        var cmd = CriarInsertCommand(conn, tx, sql, parametros);
        await cmd.ExecuteNonQueryAsync();
    }

    private static Dictionary<string, object> GerarParametrosArma(Arma arma)
    {
        var dict = SqliteHelper.GerarParametrosEntidadeBase(arma);

        dict["$tipo"] = (int)arma.Tipo;
        dict["$categoria"] = (int)arma.Categoria;
        dict["$dadoDano"] = arma.DadoDano ?? "";
        dict["$tipoDano"] = (int)arma.TipoDano;
        dict["$tipoDanoSecundario"] = arma.TipoDanoSecundario.HasValue ? (object)(int)arma.TipoDanoSecundario.Value : DBNull.Value;
        dict["$peso"] = arma.PesoUnitario;
        dict["$custo"] = arma.ValorCobre;
        dict["$alcance"] = arma.Alcance ?? (object)DBNull.Value;
        dict["$ehDuasMaos"] = arma.EhDuasMaos ? 1 : 0;
        dict["$ehLeve"] = arma.EhLeve ? 1 : 0;
        dict["$ehVersatil"] = arma.EhVersatil ? 1 : 0;
        dict["$dadoDanoVersatil"] = arma.DadoDanoVersatil ?? "";
        dict["$podeSerArremessada"] = arma.PodeSerArremessada ? 1 : 0;
        dict["$alcanceArremesso"] = arma.AlcanceArremesso ?? (object)DBNull.Value;
        dict["$bonusMagico"] = arma.BonusMagico;
        dict["$durabilidadeAtual"] = arma.DurabilidadeAtual;
        dict["$durabilidadeMaxima"] = arma.DurabilidadeMaxima;
        dict["$raridade"] = arma.Raridade ?? "";
        dict["$fabricante"] = arma.Fabricante ?? "";

        return dict;
    }

    private static async Task InserirListaTexto(SqliteConnection conn, SqliteTransaction tx, string tabela, string coluna, string armaId, IEnumerable<string> itens)
    {
        foreach (var item in itens ?? new List<string>())
        {
            var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = $"INSERT OR IGNORE INTO {tabela} (ArmaId, {coluna}) VALUES ($armaId, $valor)";
            cmd.Parameters.AddWithValue("$armaId", armaId);
            cmd.Parameters.AddWithValue("$valor", item);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
