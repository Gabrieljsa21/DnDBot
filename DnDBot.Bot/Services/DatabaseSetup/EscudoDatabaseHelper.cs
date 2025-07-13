using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class EscudoDatabaseHelper
    {
        private const string CaminhoJson = "Data/escudos.json";

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            var escudos = await JsonLoaderHelper.CarregarAsync<List<Escudo>>(CaminhoJson, "escudos");

            foreach (var escudo in escudos)
            {
                // Insere o item base
                await ItemDatabaseHelper.InserirItem(connection, transaction, escudo);

                // Insere os campos específicos do escudo
                var parametros = new Dictionary<string, object>
                {
                    ["Id"] = escudo.Id,
                    ["BonusCA"] = escudo.BonusCA,
                    ["DurabilidadeAtual"] = escudo.DurabilidadeAtual,
                    ["DurabilidadeMaxima"] = escudo.DurabilidadeMaxima
                };
                await InserirEntidadeFilhaAsync(connection, transaction, "Escudo", parametros);

                // Insere propriedades especiais
                if (escudo.PropriedadesEspeciais?.Any() == true)
                    await InserirTagsAsync(connection, transaction, "EscudoPropriedadeEspecial", "EscudoId", escudo.Id, escudo.PropriedadesEspeciais.Select(p => p.Propriedade).ToList());
            }

            Console.WriteLine("✅ Escudos populados.");
        }
    }
}
