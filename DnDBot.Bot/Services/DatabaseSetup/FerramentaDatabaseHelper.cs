using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha.Auxiliares;
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
    public static class FerramentaDatabaseHelper
    {
        private const string CaminhoJson = "Data/ferramentas.json";

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            var listaFerramentas = await JsonLoaderHelper.CarregarAsync<List<Ferramenta>>(CaminhoJson, "ferramentas");

            foreach (var ferramenta in listaFerramentas)
            {
                // ✅ Corrige relacionamentos
                ferramenta.NormalizarRelacionamentos();

                await ItemDatabaseHelper.InserirItem(connection, transaction, ferramenta);

                // Inserção da entidade filha 'Ferramenta' 
                var parametrosFerramenta = new Dictionary<string, object>
                {
                    ["Id"] = ferramenta.Id,
                    ["RequerProficiencia"] = ferramenta.RequerProficiencia ? 1 : 0
                };
                await SqliteHelper.InserirEntidadeFilhaAsync(connection, transaction, "Ferramenta", parametrosFerramenta);

                // Inserção das tags
                if (ferramenta.Tags?.Any() == true) 
                    await SqliteHelper.InserirRelacionamentoSimplesAsync(connection, transaction, "FerramentaTag", new[] { "FerramentaId", "Tag" }, ferramenta.Tags, tag => new object[] { ferramenta.Id, tag });

                // Inserção das Pericias
                if (ferramenta.PericiasAssociadas?.Any() == true) 
                    await SqliteHelper.InserirRelacionamentoSimplesAsync(connection, transaction, "FerramentaPericia", new[] { "FerramentaId", "PericiaId" }, ferramenta.PericiasAssociadas, p => new object[] { ferramenta.Id, p.PericiaId });
            }

            Console.WriteLine("✅ Ferramentas populadas.");
        }

    }
}
