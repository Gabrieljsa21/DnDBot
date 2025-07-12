using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models;
using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
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
    private const string CaminhoJsonAntecedenteItens = "Data/antecedenteitens.json";
    private const string CaminhoJsonAntecedenteOpcoesItens = "Data/antecedenteopcoesitem.json";
    private const string CaminhoJsonAntecedenteOpcoesProficiencias = "Data/antecedenteopcoesproficiencia.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Antecedente"] = @"
        Id TEXT PRIMARY KEY,
        IdiomasAdicionais INTEGER,
        QntOpcoesProficiencia INTEGER,
        QntOpcoesItem INTEGER,
        Ouro INTEGER,
        " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

            ["AntecedenteTag"] = @"
        AntecedenteId TEXT NOT NULL,
        Tag TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, Tag),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE",

            ["AntecedenteProficiencia"] = @"
        AntecedenteId TEXT NOT NULL,
        ProficienciaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ProficienciaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ProficienciaId) REFERENCES Proficiencia(Id) ON DELETE CASCADE",

            ["AntecedenteItem"] = @"
        AntecedenteId TEXT NOT NULL,
        ItemId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ItemId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ItemId) REFERENCES Item(Id) ON DELETE CASCADE",

            ["AntecedenteCaracteristica"] = @"
        AntecedenteId TEXT NOT NULL,
        CaracteristicaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, CaracteristicaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (CaracteristicaId) REFERENCES Caracteristica(Id) ON DELETE CASCADE",

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

            ["AntecedenteProficienciaOpcoes"] = @"
        AntecedenteId TEXT NOT NULL,
        ProficienciaId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ProficienciaId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ProficienciaId) REFERENCES Proficiencia(Id) ON DELETE CASCADE",

            ["AntecedenteItemOpcoes"] = @"
        AntecedenteId TEXT NOT NULL,
        ItemId TEXT NOT NULL,
        PRIMARY KEY (AntecedenteId, ItemId),
        FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
        FOREIGN KEY (ItemId) REFERENCES Item(Id) ON DELETE CASCADE",
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

        // Lê itens fixos
        Dictionary<string, List<string>> itensFixosDict = null;
        if (File.Exists(CaminhoJsonAntecedenteItens))
        {
            Console.WriteLine("📥 Lendo dados de antecedenteitens.json...");
            var jsonItensFixos = await File.ReadAllTextAsync(CaminhoJsonAntecedenteItens);
            itensFixosDict = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonItensFixos);
        }
        else
        {
            Console.WriteLine("❌ Arquivo antecedenteitens.json não encontrado.");
        }

        // Lê opções de itens
        Dictionary<string, List<string>> opcoesItensDict = null;
        if (File.Exists(CaminhoJsonAntecedenteOpcoesItens))
        {
            Console.WriteLine("📥 Lendo dados de antecedenteopcoesitem.json...");
            var jsonItens = await File.ReadAllTextAsync(CaminhoJsonAntecedenteOpcoesItens);
            opcoesItensDict = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonItens);
        }
        else
        {
            Console.WriteLine("❌ Arquivo antecedenteopcoesitem.json não encontrado.");
        }

        // Lê opções de proficiências
        Dictionary<string, List<string>> opcoesProficienciasDict = null;
        if (File.Exists(CaminhoJsonAntecedenteOpcoesProficiencias))
        {
            Console.WriteLine("📥 Lendo dados de antecedenteopcoesproficiencia.json...");
            var jsonProficiencias = await File.ReadAllTextAsync(CaminhoJsonAntecedenteOpcoesProficiencias);
            opcoesProficienciasDict = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonProficiencias);
        }
        else
        {
            Console.WriteLine("❌ Arquivo antecedenteopcoesproficiencia.json não encontrado.");
        }

        if (antecedentes == null) return;

        foreach (var antecedente in antecedentes)
        {
            // Insere o Antecedente se não existir
            if (!await RegistroExisteAsync(connection, transaction, "Antecedente", antecedente.Id))
            {
                var parametros = GerarParametrosEntidadeBase(antecedente);
                parametros["idiomas"] = antecedente.IdiomasAdicionais;
                parametros["qtdItem"] = antecedente.QntOpcoesItem;
                parametros["qtdProf"] = antecedente.QntOpcoesProficiencia;
                parametros["ouro"] = antecedente.Ouro;

                var sql = $@"
                INSERT INTO Antecedente (
                    Id, IdiomasAdicionais, QntOpcoesItem, QntOpcoesProficiencia, Ouro, {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
                ) VALUES (
                    $id, $idiomas, $qtdItem, $qtdProf, $ouro, {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
                )";

                var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
                await cmd.ExecuteNonQueryAsync();
            }

            // Características
            var idsCaracteristicas = antecedente.Caracteristicas
                .Select(x => x.CaracteristicaId)
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .ToList();

            var idsExistentes = new List<string>();
            foreach (var id in idsCaracteristicas)
            {
                if (await RegistroExisteAsync(connection, transaction, "Caracteristica", id))
                    idsExistentes.Add(id);
                else
                    Console.WriteLine($"⚠ Característica não encontrada: '{id}' para antecedente '{antecedente.Id}'");
            }

            await InserirRelacionamentoSimples(connection, transaction, "AntecedenteCaracteristica", "CaracteristicaId", antecedente.Id, idsExistentes);
            await InserirRelacionamentoSimples(connection, transaction, "AntecedenteProficiencia", "ProficienciaId", antecedente.Id, antecedente.Proficiencia.Select(x => x.ProficienciaId));

            await InserirTagsAsync(connection, transaction, "AntecedenteTag", "AntecedenteId", antecedente.Id, antecedente.Tags);
            await InserirBase(connection, transaction, "AntecedenteIdeal", antecedente.Id, antecedente.Ideais.Select(x => x.Ideal));
            await InserirBase(connection, transaction, "AntecedenteVinculo", antecedente.Id, antecedente.Vinculos.Select(x => x.Vinculo));
            await InserirBase(connection, transaction, "AntecedenteDefeito", antecedente.Id, antecedente.Defeitos.Select(x => x.Defeito));

            // Itens fixos
            if (itensFixosDict != null && itensFixosDict.TryGetValue(antecedente.Id, out var itensFixos))
            {
                await InserirRelacionamentoSimples(connection, transaction, "AntecedenteItem", "ItemId", antecedente.Id, itensFixos);
            }

            // Opções de itens
            if (opcoesItensDict != null && opcoesItensDict.TryGetValue(antecedente.Id, out var itens))
            {
                await InserirRelacionamentoSimples(connection, transaction, "AntecedenteItemOpcoes", "ItemId", antecedente.Id, itens);
            }

            // Opções de proficiência
            if (opcoesProficienciasDict != null && opcoesProficienciasDict.TryGetValue(antecedente.Id, out var proficiencias))
            {
                await InserirRelacionamentoSimples(connection, transaction, "AntecedenteProficienciaOpcoes", "ProficienciaId", antecedente.Id, proficiencias);
            }
        }

        Console.WriteLine("✅ Antecedentes populados.");
    }


    private static async Task InserirRelacionamentoSimples(SqliteConnection conn, SqliteTransaction tx, string tabela, string coluna, string antecedenteId, IEnumerable<string> itens)
    {
        if (string.IsNullOrEmpty(antecedenteId))
            throw new ArgumentException("antecedenteId não pode ser nulo ou vazio", nameof(antecedenteId));

        // Determina a tabela de referência com base na coluna
        var tabelaReferencia = coluna switch
        {
            "ItemId" => "Item",
            "ProficienciaId" => "Proficiencia",
            "CaracteristicaId" => "Caracteristica",
            _ => throw new InvalidOperationException($"Coluna não suportada para verificação de existência: {coluna}")
        };

        foreach (var item in itens?.Where(i => !string.IsNullOrEmpty(i)) ?? Enumerable.Empty<string>())
        {
            // Verifica se o item existe na tabela de referência
            var checkCmd = conn.CreateCommand();
            checkCmd.Transaction = tx;
            checkCmd.CommandText = $"SELECT 1 FROM {tabelaReferencia} WHERE Id = $id";
            checkCmd.Parameters.AddWithValue("$id", item);
            var exists = await checkCmd.ExecuteScalarAsync();

            if (exists == null)
            {
                Console.WriteLine($"❌ ID inexistente na tabela {tabelaReferencia}: '{item}' (para {tabela}, AntecedenteId='{antecedenteId}')");
                continue;
            }

            // Insere o relacionamento
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

    private static async Task InserirBase(SqliteConnection conn, SqliteTransaction tx, string tabela, string antecedenteId, IEnumerable<EntidadeBase> lista)
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
