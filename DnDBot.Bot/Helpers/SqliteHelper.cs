using DnDBot.Bot.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Helpers
{
    public static class SqliteHelper
    {
        public static class SqliteEntidadeBaseHelper
        {
            public const string Campos = @"
            Id TEXT PRIMARY KEY,
            Fonte TEXT,
            Nome TEXT NOT NULL,
            Descricao TEXT NOT NULL,
            Pagina TEXT,
            Versao TEXT,
            ImagemUrl TEXT,
            IconeUrl TEXT,
            CriadoPor TEXT,
            CriadoEm TEXT,
            ModificadoPor TEXT,
            ModificadoEm TEXT";

            public const string CamposInsert = @"
            Id, Fonte, Nome, Descricao, Pagina, Versao, ImagemUrl, IconeUrl,
            CriadoPor, CriadoEm, ModificadoPor, ModificadoEm";

            public const string ValoresInsert = @"
            $id, $fonte, $nome, $desc, $pagina, $versao, $imgUrl, $iconeUrl,
            $criadoPor, $criadoEm, $modificadoPor, $modificadoEm";
        }

        public static Dictionary<string, object> GerarParametrosEntidadeBase(EntidadeBase entidade)
        {
            return new Dictionary<string, object>
            {
                ["id"] = entidade.Id,
                ["nome"] = entidade.Nome ?? "",
                ["descricao"] = entidade.Descricao ?? "",
                ["fonte"] = entidade.Fonte ?? "",
                ["pagina"] = entidade.Pagina ?? "",
                ["versao"] = entidade.Versao ?? "",
                ["imagemUrl"] = entidade.ImagemUrl ?? "",
                ["iconeUrl"] = entidade.IconeUrl ?? "",
                ["criadoPor"] = entidade.CriadoPor ?? "",
                ["criadoEm"] = entidade.CriadoEm ?? DateTime.UtcNow,
                ["modificadoPor"] = entidade.ModificadoPor ?? "",
                ["modificadoEm"] = entidade.ModificadoEm ?? DateTime.UtcNow
            };
        }

        public static async Task<bool> RegistroExisteAsync(SqliteConnection conn, SqliteTransaction tx, string tabela, string id)
        {
            var colunaCmd = conn.CreateCommand();
            colunaCmd.Transaction = tx;
            colunaCmd.CommandText = $"PRAGMA table_info({tabela})";

            using (var reader = await colunaCmd.ExecuteReaderAsync())
            {
                bool colunaExiste = false;
                while (await reader.ReadAsync())
                {
                    var nomeColuna = reader.GetString(1); // 1 = name da coluna
                    if (string.Equals(nomeColuna, "Id", StringComparison.OrdinalIgnoreCase))
                    {
                        colunaExiste = true;
                        break;
                    }
                }

                if (!colunaExiste)
                {
                    throw new InvalidOperationException($"A tabela '{tabela}' não possui uma coluna chamada 'Id'.");
                }
            }

            // Executa a verificação de existência do registro
            var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = $"SELECT COUNT(*) FROM {tabela} WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);
            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }


        public static async Task CriarTabelaAsync(SqliteCommand cmd, string nomeTabela, string definicaoColunas)
        {
            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {nomeTabela} ({definicaoColunas});";
            await cmd.ExecuteNonQueryAsync();
        }


        public static SqliteCommand CriarInsertCommand(SqliteConnection conn, SqliteTransaction tx, string sql, Dictionary<string, object> parametros)
        {
            var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = sql;

            foreach (var p in parametros)
            {
                cmd.Parameters.AddWithValue($"${p.Key}", p.Value ?? DBNull.Value);
            }

            return cmd;
        }

        public static async Task InserirTagsAsync(SqliteConnection conn, SqliteTransaction tx, string tabela, string chavePrimariaColuna, string entidadeId, List<string> tags)
        {
            if (tags == null) return;

            foreach (var tag in tags)
            {
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = $"INSERT OR IGNORE INTO {tabela} ({chavePrimariaColuna}, Tag) VALUES ($id, $tag)";
                cmd.Parameters.AddWithValue("$id", entidadeId);
                cmd.Parameters.AddWithValue("$tag", tag);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task InserirEntidadeBaseAsync(SqliteConnection conn, SqliteTransaction tx, string tabela, EntidadeBase entidade)
        {
            if (await RegistroExisteAsync(conn, tx, tabela, entidade.Id))
                return;

            var parametros = GerarParametrosEntidadeBase(entidade);

            var sql = $@"
            INSERT INTO {tabela} (
                {SqliteEntidadeBaseHelper.CamposInsert}
            ) VALUES (
                {SqliteEntidadeBaseHelper.ValoresInsert}
            )";

            var cmd = CriarInsertCommand(conn, tx, sql, parametros);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task InserirEntidadeFilhaAsync(SqliteConnection connection, SqliteTransaction transaction, string tabela, Dictionary<string, object> parametros)
        {
            var colunas = string.Join(", ", parametros.Keys);
            var valores = string.Join(", ", parametros.Keys.Select(k => "$" + k));

            var sql = $@"
        INSERT OR IGNORE INTO {tabela}
            ({colunas})
        VALUES
            ({valores});";

            using var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task InserirRelacionamentoSimplesAsync<T>(SqliteConnection conn,SqliteTransaction tx,string tabela,string[] colunas,IEnumerable<T> dados,Func<T, object[]> extratorValores)
        {
            if (dados == null || !dados.Any())
                return;

            var colunasSql = string.Join(", ", colunas);
            var parametrosSql = string.Join(", ", colunas.Select(c => $"${c}"));

            var sql = $"INSERT OR IGNORE INTO {tabela} ({colunasSql}) VALUES ({parametrosSql});";

            foreach (var item in dados)
            {
                var valores = extratorValores(item);
                using var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;

                for (int i = 0; i < colunas.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"${colunas[i]}", valores[i] ?? DBNull.Value);
                }

                await cmd.ExecuteNonQueryAsync();
            }
        }



    }
}
