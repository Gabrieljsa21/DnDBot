using DnDBot.Application.Helpers;
using DnDBot.Application.Models;
using DnDBot.Application.Models.AntecedenteModels;
using DnDBot.Application.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;

public static class AntecedenteDatabaseHelper
{
    private const string CaminhoJson = "Data/antecedentes.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Antecedente"] = @"
                Id TEXT PRIMARY KEY,
                Requisitos TEXT,
                IdiomasAdicionais INTEGER,
                " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

            ["Antecedente_Tag"] = @"
                AntecedenteId TEXT NOT NULL,
                Tag TEXT NOT NULL,
                PRIMARY KEY (AntecedenteId, Tag),
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["Antecedente_Pericia"] = @"
                AntecedenteId TEXT NOT NULL,
                IdPericia TEXT NOT NULL,
                PRIMARY KEY (AntecedenteId, IdPericia),
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                FOREIGN KEY (IdPericia) REFERENCES Pericia(Id) ON DELETE CASCADE",

            ["Antecedente_Idioma"] = @"
                AntecedenteId TEXT NOT NULL,
                IdIdioma TEXT NOT NULL,
                PRIMARY KEY (AntecedenteId, IdIdioma),
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                FOREIGN KEY (IdIdioma) REFERENCES Idioma(Id) ON DELETE CASCADE",

            ["Antecedente_Ferramenta"] = @"
                AntecedenteId TEXT NOT NULL,
                IdFerramenta TEXT NOT NULL,
                PRIMARY KEY (AntecedenteId, IdFerramenta),
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                FOREIGN KEY (IdFerramenta) REFERENCES Ferramenta(Id) ON DELETE CASCADE",

            ["Antecedente_RiquezaInicial"] = @"
                AntecedenteId TEXT NOT NULL,
                Tipo TEXT NOT NULL,
                Quantidade INTEGER NOT NULL,
                PRIMARY KEY (AntecedenteId, Tipo),
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["Antecedente_Ideal"] = @"
                Id TEXT PRIMARY KEY,
                Nome TEXT,
                Descricao TEXT,
                AntecedenteId TEXT NOT NULL,
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["Antecedente_Vinculo"] = @"
                Id TEXT PRIMARY KEY,
                Nome TEXT,
                Descricao TEXT,
                AntecedenteId TEXT NOT NULL,
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["Antecedente_Defeito"] = @"
                Id TEXT PRIMARY KEY,
                Nome TEXT,
                Descricao TEXT,
                AntecedenteId TEXT NOT NULL,
                FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo antecedentes.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de antecedentes.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var antecedentes = JsonSerializer.Deserialize<List<Antecedente>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (antecedentes == null) return;

        foreach (var antecedente in antecedentes)
        {
            if (!await RegistroExisteAsync(connection, transaction, "Antecedente", antecedente.Id))
            {
                var parametros = GerarParametrosEntidadeBase(antecedente);
                parametros["req"] = antecedente.Requisitos ?? "";
                parametros["idiomas"] = antecedente.IdiomasAdicionais;

                var sql = $@"
                    INSERT INTO Antecedente (
                        Id, Requisitos, IdiomasAdicionais, {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
                    ) VALUES (
                        $id, $req, $idiomas, {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
                    )";

                var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
                await cmd.ExecuteNonQueryAsync();
            }

            await InserirRelacionamentoSimples(connection, transaction, "Antecedente_Pericia", "IdPericia", antecedente.Id, antecedente.Pericias);
            await InserirRelacionamentoSimples(connection, transaction, "Antecedente_Idioma", "IdIdioma", antecedente.Id, antecedente.Idiomas);
            await InserirRelacionamentoSimples(connection, transaction, "Antecedente_Ferramenta", "IdFerramenta", antecedente.Id, antecedente.Ferramentas);
            await InserirTagsAsync(connection, transaction, "Antecedente_Tag", "AntecedenteId", antecedente.Id, antecedente.Tags);

            await InserirRiqueza(connection, transaction, antecedente.Id, antecedente.RiquezaInicial);
            await InserirCaracteristicas(connection, transaction, "Antecedente_Ideal", antecedente.Id, antecedente.Ideais);
            await InserirCaracteristicas(connection, transaction, "Antecedente_Vinculo", antecedente.Id, antecedente.Vinculos);
            await InserirCaracteristicas(connection, transaction, "Antecedente_Defeito", antecedente.Id, antecedente.Defeitos);
        }

        Console.WriteLine("✅ Antecedentes populados.");
    }

    private static async Task InserirRelacionamentoSimples(SqliteConnection conn, SqliteTransaction tx, string tabela, string coluna, string antecedenteId, IEnumerable<string> itens)
    {
        foreach (var item in itens ?? new List<string>())
        {
            var insert = conn.CreateCommand();
            insert.Transaction = tx;
            insert.CommandText = $"INSERT OR IGNORE INTO {tabela} (AntecedenteId, {coluna}) VALUES ($aid, $valor)";
            insert.Parameters.AddWithValue("$aid", antecedenteId);
            insert.Parameters.AddWithValue("$valor", item);
            await insert.ExecuteNonQueryAsync();
        }
    }

    private static async Task InserirRelacionamentoSimples(SqliteConnection conn, SqliteTransaction tx, string tabela, string coluna, string antecedenteId, IEnumerable<EntidadeBase> itens)
    {
        foreach (var item in itens ?? new List<EntidadeBase>())
            await InserirRelacionamentoSimples(conn, tx, tabela, coluna, antecedenteId, new[] { item.Id });
    }

    private static async Task InserirRiqueza(SqliteConnection conn, SqliteTransaction tx, string antecedenteId, IEnumerable<Moeda> moedas)
    {
        foreach (var m in moedas ?? new List<Moeda>())
        {
            var insert = conn.CreateCommand();
            insert.Transaction = tx;
            insert.CommandText = "INSERT OR IGNORE INTO Antecedente_RiquezaInicial (AntecedenteId, Tipo, Quantidade) VALUES ($aid, $tipo, $qtd)";
            insert.Parameters.AddWithValue("$aid", antecedenteId);
            insert.Parameters.AddWithValue("$tipo", m.Tipo.ToString());
            insert.Parameters.AddWithValue("$qtd", m.Quantidade);
            await insert.ExecuteNonQueryAsync();
        }
    }

    private static async Task InserirCaracteristicas(SqliteConnection conn, SqliteTransaction tx, string tabela, string antecedenteId, IEnumerable<EntidadeBase> lista)
    {
        foreach (var item in lista ?? new List<EntidadeBase>())
        {
            var insert = conn.CreateCommand();
            insert.Transaction = tx;
            insert.CommandText = $@"
                INSERT OR IGNORE INTO {tabela} (Id, Nome, Descricao, AntecedenteId)
                VALUES ($id, $nome, $desc, $aid)";
            insert.Parameters.AddWithValue("$id", item.Id);
            insert.Parameters.AddWithValue("$nome", item.Nome ?? "");
            insert.Parameters.AddWithValue("$desc", item.Descricao ?? "");
            insert.Parameters.AddWithValue("$aid", antecedenteId);
            await insert.ExecuteNonQueryAsync();
        }
    }
}
