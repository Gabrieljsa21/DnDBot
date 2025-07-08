using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models
{
    public static class SqliteCommandExtensions
    {
        public static void AddEntidadeBaseParameters(this SqliteCommand cmd, EntidadeBase entidade)
        {
            cmd.Parameters.AddWithValue("$id", entidade.Id);
            cmd.Parameters.AddWithValue("$nome", entidade.Nome ?? "");
            cmd.Parameters.AddWithValue("$desc", entidade.Descricao ?? "");
            cmd.Parameters.AddWithValue("$fonte", entidade.Fonte ?? "");
            cmd.Parameters.AddWithValue("$pagina", entidade.Pagina ?? "");
            cmd.Parameters.AddWithValue("$versao", entidade.Versao ?? "");
            cmd.Parameters.AddWithValue("$imgUrl", entidade.ImagemUrl ?? "");
            cmd.Parameters.AddWithValue("$iconeUrl", entidade.IconeUrl ?? "");
            cmd.Parameters.AddWithValue("$criadoPor", entidade.CriadoPor ?? "");
            cmd.Parameters.AddWithValue("$criadoEm", entidade.CriadoEm ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("$modificadoPor", entidade.ModificadoPor ?? "");
            cmd.Parameters.AddWithValue("$modificadoEm", entidade.ModificadoEm ?? (object)DBNull.Value);
        }
    }
}
