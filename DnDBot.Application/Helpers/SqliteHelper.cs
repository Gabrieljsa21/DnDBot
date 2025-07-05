using DnDBot.Application.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDBot.Application.Helpers
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
                ["desc"] = entidade.Descricao ?? "",
                ["fonte"] = entidade.Fonte ?? "",
                ["pagina"] = entidade.Pagina ?? "",
                ["versao"] = entidade.Versao ?? "",
                ["imgUrl"] = entidade.ImagemUrl ?? "",
                ["iconeUrl"] = entidade.IconeUrl ?? "",
                ["criadoPor"] = entidade.CriadoPor ?? "",
                ["criadoEm"] = entidade.CriadoEm.HasValue ? entidade.CriadoEm.Value.ToString("o") : DBNull.Value,
                ["modificadoPor"] = entidade.ModificadoPor ?? "",
                ["modificadoEm"] = entidade.ModificadoEm.HasValue ? entidade.ModificadoEm.Value.ToString("o") : DBNull.Value
            };
        }

        public static async Task<bool> RegistroExisteAsync(SqliteConnection conn, SqliteTransaction tx, string tabela, string id)
        {
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

    }
}
