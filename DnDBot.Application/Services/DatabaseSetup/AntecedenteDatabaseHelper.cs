using DnDBot.Application.Models;
using DnDBot.Application.Models.Antecedente;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDBot.Application.Services.DatabaseSetup
{
    /// <summary>
    /// Classe estática auxiliar para criação e povoamento das tabelas relacionadas a antecedentes no banco SQLite.
    /// </summary>
    public static class AntecedenteDatabaseHelper
    {
        /// <summary>
        /// Caminho do arquivo JSON contendo os dados dos antecedentes.
        /// </summary>
        private const string CaminhoJson = "Data/antecedentes.json";

        /// <summary>
        /// Cria as tabelas necessárias para armazenar antecedentes e suas relações auxiliares, caso ainda não existam.
        /// </summary>
        /// <param name="cmd">Comando SQLite associado a uma conexão aberta.</param>
        /// <returns>Tarefa assíncrona que representa a operação de criação das tabelas.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            // Cria tabelas auxiliares de relacionamento muitos-para-muitos entre Antecedente e Perícia, Idioma, Ferramenta.
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Pericia (
                    AntecedenteId TEXT NOT NULL,
                    IdPericia TEXT NOT NULL,
                    PRIMARY KEY (AntecedenteId, IdPericia),
                    FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                    FOREIGN KEY (IdPericia) REFERENCES Pericia(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Idioma (
                    AntecedenteId TEXT NOT NULL,
                    IdIdioma TEXT NOT NULL,
                    PRIMARY KEY (AntecedenteId, IdIdioma),
                    FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                    FOREIGN KEY (IdIdioma) REFERENCES Idioma(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Ferramenta (
                    AntecedenteId TEXT NOT NULL,
                    IdFerramenta TEXT NOT NULL,
                    PRIMARY KEY (AntecedenteId, IdFerramenta),
                    FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE,
                    FOREIGN KEY (IdFerramenta) REFERENCES Ferramenta(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            // Cria tabela para equipamentos detalhados do antecedente.
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Equipamento (
                    AntecedenteId TEXT NOT NULL,
                    Nome TEXT NOT NULL,
                    Quantidade INTEGER NOT NULL,
                    PRIMARY KEY (AntecedenteId, Nome),
                    FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            // Cria tabela para riqueza inicial (dinheiro/recursos) do antecedente.
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_RiquezaInicial (
                    AntecedenteId TEXT NOT NULL,
                    Tipo TEXT NOT NULL,
                    Quantidade INTEGER NOT NULL,
                    PRIMARY KEY (AntecedenteId, Tipo),
                    FOREIGN KEY (AntecedenteId) REFERENCES Antecedente(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            // Cria tabelas para Ideais, Vínculos e Defeitos associados aos antecedentes.
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Ideal (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT,
                    Descricao TEXT,
                    IdAntecedente TEXT NOT NULL,
                    FOREIGN KEY (IdAntecedente) REFERENCES Antecedente(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Vinculo (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT,
                    Descricao TEXT,
                    IdAntecedente TEXT NOT NULL,
                    FOREIGN KEY (IdAntecedente) REFERENCES Antecedente(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente_Defeito (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT,
                    Descricao TEXT,
                    IdAntecedente TEXT NOT NULL,
                    FOREIGN KEY (IdAntecedente) REFERENCES Antecedente(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();

            // Cria tabela principal de Antecedente.
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Antecedente (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Descricao TEXT,
                    Requisitos TEXT,
                    Fonte TEXT,
                    Pagina TEXT,
                    Versao TEXT,
                    IdiomasAdicionais INTEGER
                );
            ";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula as tabelas de antecedentes e suas relações auxiliares com os dados extraídos do arquivo JSON.
        /// Insere somente registros novos para evitar duplicação.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para garantir atomicidade e integridade das operações.</param>
        /// <returns>Tarefa assíncrona representando o processo de povoamento.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJson))
            {
                Console.WriteLine("❌ Arquivo antecedentes.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de antecedentes.json...");

            var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
            var antecedentes = JsonSerializer.Deserialize<List<Antecedente>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (antecedentes == null) return;

            foreach (var antecedente in antecedentes)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Antecedente WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", antecedente.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Antecedente (
                            Id, Nome, Descricao, Requisitos, Fonte, Pagina, Versao, IdiomasAdicionais
                        ) VALUES (
                            $id, $nome, $descricao, $requisitos, $fonte, $pagina, $versao, $idiomasAdicionais
                        )";
                    insertCmd.Parameters.AddWithValue("$id", antecedente.Id);
                    insertCmd.Parameters.AddWithValue("$nome", antecedente.Nome ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$descricao", antecedente.Descricao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$requisitos", antecedente.Requisitos ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$fonte", antecedente.Fonte ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$pagina", antecedente.Pagina ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$versao", antecedente.Versao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$idiomasAdicionais", antecedente.IdiomasAdicionais);
                    await insertCmd.ExecuteNonQueryAsync();
                }

                // Popula relacionamentos muitos-para-muitos e tabelas auxiliares.

                // Perícias
                foreach (var pericia in antecedente.Pericias ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = "INSERT OR IGNORE INTO Antecedente_Pericia (AntecedenteId, IdPericia) VALUES ($aid, $pid)";
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    insert.Parameters.AddWithValue("$pid", pericia.Id);
                    await insert.ExecuteNonQueryAsync();
                }

                // Idiomas
                foreach (var idioma in antecedente.Idiomas ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = "INSERT OR IGNORE INTO Antecedente_Idioma (AntecedenteId, IdIdioma) VALUES ($aid, $iid)";
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    insert.Parameters.AddWithValue("$iid", idioma.Id);
                    await insert.ExecuteNonQueryAsync();
                }

                // Ferramentas
                foreach (var ferramenta in antecedente.Ferramentas ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = "INSERT OR IGNORE INTO Antecedente_Ferramenta (AntecedenteId, IdFerramenta) VALUES ($aid, $fid)";
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    insert.Parameters.AddWithValue("$fid", ferramenta.Id);
                    await insert.ExecuteNonQueryAsync();
                }

                // Equipamentos detalhados
                foreach (var equipamento in antecedente.EquipamentosDetalhados ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = "INSERT OR IGNORE INTO Antecedente_Equipamento (AntecedenteId, Nome, Quantidade) VALUES ($aid, $nome, $qtd)";
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    insert.Parameters.AddWithValue("$nome", equipamento.Nome ?? string.Empty);
                    insert.Parameters.AddWithValue("$qtd", equipamento.Quantidade);
                    await insert.ExecuteNonQueryAsync();
                }

                // Riqueza inicial
                foreach (var moeda in antecedente.RiquezaInicial ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = "INSERT OR IGNORE INTO Antecedente_RiquezaInicial (AntecedenteId, Tipo, Quantidade) VALUES ($aid, $tipo, $qtd)";
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    insert.Parameters.AddWithValue("$tipo", moeda.Tipo.ToString());
                    insert.Parameters.AddWithValue("$qtd", moeda.Quantidade);
                    await insert.ExecuteNonQueryAsync();
                }

                // Ideais
                foreach (var ideal in antecedente.Ideais ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = @"
                        INSERT OR IGNORE INTO Antecedente_Ideal (Id, Nome, Descricao, IdAntecedente)
                        VALUES ($id, $nome, $descricao, $aid)";
                    insert.Parameters.AddWithValue("$id", ideal.Id);
                    insert.Parameters.AddWithValue("$nome", ideal.Nome ?? string.Empty);
                    insert.Parameters.AddWithValue("$descricao", ideal.Descricao ?? string.Empty);
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    await insert.ExecuteNonQueryAsync();
                }

                // Vínculos
                foreach (var vinculo in antecedente.Vinculos ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = @"
                        INSERT OR IGNORE INTO Antecedente_Vinculo (Id, Nome, Descricao, IdAntecedente)
                        VALUES ($id, $nome, $descricao, $aid)";
                    insert.Parameters.AddWithValue("$id", vinculo.Id);
                    insert.Parameters.AddWithValue("$nome", vinculo.Nome ?? string.Empty);
                    insert.Parameters.AddWithValue("$descricao", vinculo.Descricao ?? string.Empty);
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    await insert.ExecuteNonQueryAsync();
                }

                // Defeitos
                foreach (var defeito in antecedente.Defeitos ?? new())
                {
                    var insert = connection.CreateCommand();
                    insert.Transaction = transaction;
                    insert.CommandText = @"
                        INSERT OR IGNORE INTO Antecedente_Defeito (Id, Nome, Descricao, IdAntecedente)
                        VALUES ($id, $nome, $descricao, $aid)";
                    insert.Parameters.AddWithValue("$id", defeito.Id);
                    insert.Parameters.AddWithValue("$nome", defeito.Nome ?? string.Empty);
                    insert.Parameters.AddWithValue("$descricao", defeito.Descricao ?? string.Empty);
                    insert.Parameters.AddWithValue("$aid", antecedente.Id);
                    await insert.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine("✅ Antecedentes populados.");
        }
    }
}
