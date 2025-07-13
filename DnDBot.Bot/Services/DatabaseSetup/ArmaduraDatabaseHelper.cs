using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public static class ArmaduraDatabaseHelper
    {
        const string CaminhoJson = "Data/armaduras.json";

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {

            var armaduras = await JsonLoaderHelper.CarregarAsync<List<Armadura>>(CaminhoJson, "armaduras");

            if (armaduras == null || armaduras.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma armadura encontrada no JSON.");
                return;
            }

            foreach (var armadura in armaduras)
            {
                await ItemDatabaseHelper.InserirItem(connection, transaction, armadura);

                // Insere os campos específicos da Armadura
                var parametros = new Dictionary<string, object>
                {
                    ["Id"] = armadura.Id,
                    ["ClasseArmadura"] = armadura.ClasseArmadura,
                    ["ImpedeFurtividade"] = armadura.ImpedeFurtividade ? 1 : 0,
                    ["BonusDestrezaMaximo"] = armadura.BonusDestrezaMaximo,
                    ["RequisitoForca"] = armadura.RequisitoForca,
                    ["DurabilidadeAtual"] = armadura.DurabilidadeAtual,
                    ["DurabilidadeMaxima"] = armadura.DurabilidadeMaxima
                };
                await InserirEntidadeFilhaAsync(connection, transaction, "Armadura", parametros);

                // Inserir tags
                if (armadura.Tags?.Any() == true)
                    await InserirTagsAsync(connection, transaction, "ArmaduraTag", "ArmaduraId", armadura.Id, armadura.Tags);

                // Inserir propriedades especiais
                if (armadura.PropriedadesEspeciais?.Any() == true)
                    await InserirTagsAsync(connection, transaction, "ArmaduraPropriedadeEspecial", "ArmaduraId", armadura.Id, armadura.PropriedadesEspeciais.Select(p => p.PropriedadeEspecialId).ToList());

                // Inserir resistências
                if (armadura.Resistencias?.Any() == true)
                    await InserirRelacionamentoSimplesAsync(connection, transaction,"ArmaduraResistencia",new[] { "ArmaduraId", "ResistenciaId" },armadura.Resistencias,r => new object[] { armadura.Id, r.ResistenciaId });

                // Inserir imunidades
                if (armadura.Imunidades?.Any() == true)
                    await InserirRelacionamentoSimplesAsync(connection, transaction,"ArmaduraImunidade",new[] { "ArmaduraId", "ImunidadeId" },armadura.Imunidades,i => new object[] { armadura.Id, i.ImunidadeId });
            }

            Console.WriteLine("✅ Armaduras populadas.");
        }

    }
}

