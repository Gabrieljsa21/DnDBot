using DnDBot.Application.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DnDBot.Application.Services.DatabaseSetup
{
    /// <summary>
    /// Helper estático para criação e povoamento da tabela Magia no banco SQLite.
    /// </summary>
    public static class MagiaDatabaseHelper
    {
        /// <summary>
        /// Caminho do arquivo JSON contendo os dados das magias.
        /// </summary>
        private const string CaminhoJsonMagias = "Data/magias.json";

        /// <summary>
        /// Cria a tabela Magia no banco de dados SQLite, se ela ainda não existir.
        /// </summary>
        /// <param name="cmd">Comando SQLite usado para executar o SQL de criação.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Magia (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT,
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
                    Descricao TEXT,
                    Recarga TEXT,
                    TipoUso TEXT,
                    RequerLinhaDeVisao INTEGER,
                    RequerLinhaReta INTEGER,
                    NumeroMaximoAlvos INTEGER,
                    AreaEfeito TEXT,
                    FocoNecessario TEXT,
                    LimiteUso TEXT,
                    EfeitoPorTurno TEXT,
                    FonteLivro TEXT,
                    PaginaLivro INTEGER,
                    NotasInternas TEXT,
                    NumeroDeUsos INTEGER
                );
            ";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula a tabela Magia com os dados extraídos do arquivo JSON.
        /// Não insere duplicatas, verificando pela chave primária.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para operações atômicas.</param>
        /// <returns>Tarefa assíncrona representando a operação de inserção.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonMagias))
            {
                Console.WriteLine("❌ Arquivo magias.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo magias do JSON...");

            var json = await File.ReadAllTextAsync(CaminhoJsonMagias);
            var magias = JsonSerializer.Deserialize<List<Magia>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (magias == null) return;

            foreach (var magia in magias)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Magia WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", magia.Id);
                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Magia (
                            Id, Nome, Nivel, Escola, TempoConjuracao, Alcance, Alvo, Concentração, Duracao,
                            PodeSerRitual, ComponenteVerbal, ComponenteSomatico, ComponenteMaterial,
                            DetalhesMaterial, ComponenteMaterialConsumido, CustoComponenteMaterial,
                            TipoDano, DadoDano, Escalonamento, AtributoTesteResistencia, MetadeNoTeste,
                            Descricao, Recarga, TipoUso, RequerLinhaDeVisao, RequerLinhaReta,
                            NumeroMaximoAlvos, AreaEfeito, FocoNecessario, LimiteUso,
                            EfeitoPorTurno, FonteLivro, PaginaLivro, NotasInternas, NumeroDeUsos
                        )
                        VALUES (
                            $id, $nome, $nivel, $escola, $tempo, $alcance, $alvo, $conc, $duracao,
                            $ritual, $verbal, $somatico, $material, $detalhes, $consumido, $custo,
                            $dano, $dado, $escalonamento, $resistencia, $metade,
                            $descricao, $recarga, $tipouso, $linhaVisao, $linhaReta,
                            $maxAlvos, $area, $foco, $limite,
                            $porTurno, $fonteLivro, $paginaLivro, $notas, $usos
                        );";

                    insertCmd.Parameters.AddWithValue("$id", magia.Id);
                    insertCmd.Parameters.AddWithValue("$nome", magia.Nome);
                    insertCmd.Parameters.AddWithValue("$nivel", magia.Nivel);
                    insertCmd.Parameters.AddWithValue("$escola", magia.Escola);
                    insertCmd.Parameters.AddWithValue("$tempo", magia.TempoConjuracao);
                    insertCmd.Parameters.AddWithValue("$alcance", magia.Alcance);
                    insertCmd.Parameters.AddWithValue("$alvo", magia.Alvo);
                    insertCmd.Parameters.AddWithValue("$conc", magia.Concentração ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$duracao", magia.Duracao);
                    insertCmd.Parameters.AddWithValue("$ritual", magia.PodeSerRitual ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$verbal", magia.ComponenteVerbal ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$somatico", magia.ComponenteSomatico ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$material", magia.ComponenteMaterial ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$detalhes", magia.DetalhesMaterial ?? "");
                    insertCmd.Parameters.AddWithValue("$consumido", magia.ComponenteMaterialConsumido ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$custo", magia.CustoComponenteMaterial ?? "");
                    insertCmd.Parameters.AddWithValue("$dano", magia.TipoDano ?? "");
                    insertCmd.Parameters.AddWithValue("$dado", magia.DadoDano ?? "");
                    insertCmd.Parameters.AddWithValue("$escalonamento", magia.Escalonamento ?? "");
                    insertCmd.Parameters.AddWithValue("$resistencia", magia.AtributoTesteResistencia ?? "");
                    insertCmd.Parameters.AddWithValue("$metade", magia.MetadeNoTeste ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$descricao", magia.Descricao ?? "");
                    insertCmd.Parameters.AddWithValue("$recarga", magia.Recarga ?? "");
                    insertCmd.Parameters.AddWithValue("$tipouso", magia.TipoUso ?? "");
                    insertCmd.Parameters.AddWithValue("$linhaVisao", magia.RequerLinhaDeVisao ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$linhaReta", magia.RequerLinhaReta ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$maxAlvos", magia.NumeroMaximoAlvos ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("$area", magia.AreaEfeito ?? "");
                    insertCmd.Parameters.AddWithValue("$foco", magia.FocoNecessario ?? "");
                    insertCmd.Parameters.AddWithValue("$limite", magia.LimiteUso ?? "");
                    insertCmd.Parameters.AddWithValue("$porTurno", magia.EfeitoPorTurno ?? "");
                    insertCmd.Parameters.AddWithValue("$fonteLivro", magia.FonteLivro ?? "");
                    insertCmd.Parameters.AddWithValue("$paginaLivro", magia.PaginaLivro ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("$notas", magia.NotasInternas ?? "");
                    insertCmd.Parameters.AddWithValue("$usos", magia.NumeroDeUsos);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine("✅ Magias populadas.");
        }
    }
}
