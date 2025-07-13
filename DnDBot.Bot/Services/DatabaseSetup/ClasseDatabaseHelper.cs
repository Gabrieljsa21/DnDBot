using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class ClasseDatabaseHelper
    {
        private const string CaminhoJson = "Data/classes.json";

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            var classes = await JsonLoaderHelper.CarregarAsync<List<Classe>>(CaminhoJson, "classes");

            if (classes == null || classes.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma classe encontrada no JSON.");
                return;
            }

            foreach (var classe in classes)
            {
                if (await RegistroExisteAsync(connection, transaction, "Classe", classe.Id))
                    continue;

                var parametros = GerarParametrosEntidadeBase(classe);
                parametros["Id"] = classe.Id;
                parametros["DadoVida"] = classe.DadoVida ?? "";
                parametros["PapelTatico"] = classe.PapelTatico ?? "";
                parametros["IdHabilidadeConjuracao"] = classe.IdHabilidadeConjuracao ?? "";
                parametros["UsaMagiaPreparada"] = classe.UsaMagiaPreparada ? 1 : 0;

                await InserirEntidadeFilhaAsync(connection, transaction, "Classe", parametros);
                await InserirTagsAsync(connection, transaction, "ClasseTag", "ClasseId", classe.Id, classe.Tags);
            }

            Console.WriteLine("✅ Classes populadas com sucesso.");
        }
    }
}
