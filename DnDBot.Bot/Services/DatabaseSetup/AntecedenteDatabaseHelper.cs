using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models;
using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class AntecedenteDatabaseHelper
{
    private const string CaminhoJson = "Data/antecedentes.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Antecedente"] = @"
        Id TEXT PRIMARY KEY,
        IdiomasAdicionais INTEGER,
        Ouro INTEGER,
        " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

            ["AntecedenteTag"] = @"
        AntecedenteId TEXT NOT NULL,
        Tag TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, Tag),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteProficienciaPericia"] = @"
        AntecedenteId TEXT NOT NULL,
        PericiaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, PericiaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (PericiaId) REFERENCES Pericia(Id) ON DELETE CASCADE",

            ["AntecedenteFerramenta"] = @"
        AntecedenteId TEXT NOT NULL,
        FerramentaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, FerramentaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE",

            ["AntecedenteItem"] = @"
        AntecedenteId TEXT NOT NULL,
        ItemId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ItemId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ItemId) REFERENCES Item(Id) ON DELETE CASCADE",

            ["AntecedenteIdeal"] = @"
        Id TEXT PRIMARY KEY,
        Nome TEXT,
        Descricao TEXT,
        AntecedenteId TEXT NOT NULL,
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteVinculo"] = @"
        Id TEXT PRIMARY KEY,
        Nome TEXT,
        Descricao TEXT,
        AntecedenteId TEXT NOT NULL,
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteDefeito"] = @"
        Id TEXT PRIMARY KEY,
        Nome TEXT,
        Descricao TEXT,
        AntecedenteId TEXT NOT NULL,
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteOpcaoFerramenta"] = @"
        AntecedenteId TEXT NOT NULL,
        FerramentaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, FerramentaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE",

            ["AntecedenteOpcaoFerramenta_Meta"] = @"
        AntecedenteId TEXT PRIMARY KEY,
        QuantidadeEscolhas INTEGER NOT NULL,
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteOpcaoItem"] = @"
        AntecedenteId TEXT NOT NULL,
        ItemId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ItemId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ItemId) REFERENCES Item(Id) ON DELETE CASCADE",

            ["AntecedenteOpcaoItem_Meta"] = @"
        AntecedenteId TEXT PRIMARY KEY,
        QuantidadeEscolhas INTEGER NOT NULL,
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
                parametros["idiomas"] = antecedente.IdiomasAdicionais;
                parametros["ouro"] = antecedente.Ouro;

                var sql = $@"
                    INSERT INTO Antecedente (
                        Id, IdiomasAdicionais,Ouro, {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
                    ) VALUES (
                        $id, $idiomas, $ouro, {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
                    )";

                var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
                await cmd.ExecuteNonQueryAsync();
            }
            await InserirRelacionamentoSimples(connection, transaction, "AntecedenteProficienciaPericia", "PericiaId", antecedente.Id, antecedente.ProficienciaPericias.Select(x => x.PericiaId));
            await InserirRelacionamentoSimples(connection, transaction, "AntecedenteFerramenta", "FerramentaId", antecedente.Id, antecedente.ProficienciaFerramentas.Select(x => x.FerramentaId));
            await InserirTagsAsync(connection, transaction, "AntecedenteTag", "AntecedenteId", antecedente.Id, antecedente.Tags);

            await InserirRelacionamentoSimples(connection, transaction, "AntecedenteItem", "ItemId", antecedente.Id, antecedente.Itens.Select(x => x.ItemId));
            await InserirCaracteristicas(connection, transaction, "AntecedenteIdeal", antecedente.Id, antecedente.Ideais.Select(x => x.Ideal));
            await InserirCaracteristicas(connection, transaction, "AntecedenteVinculo", antecedente.Id, antecedente.Vinculos.Select(x => x.Vinculo));
            await InserirCaracteristicas(connection, transaction, "AntecedenteDefeito", antecedente.Id, antecedente.Defeitos.Select(x => x.Defeito));

        }

        Console.WriteLine("✅ Antecedentes populados.");
    }

    private static async Task InserirRelacionamentoSimples(SqliteConnection conn, SqliteTransaction tx, string tabela, string coluna, string antecedenteId, IEnumerable<string> itens)
    {
        if (string.IsNullOrEmpty(antecedenteId))
            throw new ArgumentException("antecedenteId não pode ser nulo ou vazio", nameof(antecedenteId));

        foreach (var item in itens?.Where(i => !string.IsNullOrEmpty(i)) ?? Enumerable.Empty<string>())
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
