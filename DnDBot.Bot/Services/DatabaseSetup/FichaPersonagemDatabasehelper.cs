using DnDBot.Bot.Helpers;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class FichaDatabaseHelper
    {
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["FichaPersonagem"] = string.Join(",\n", new[]
                {
                    "Id TEXT PRIMARY KEY",
                    "Nome TEXT NOT NULL",
                    "JogadorId INTEGER NOT NULL",
                    "RacaId TEXT",
                    "ClasseId TEXT",
                    "SubracaId TEXT",
                    "AntecedenteId TEXT",
                    "AlinhamentoId TEXT",
                    "Tamanho TEXT",
                    "Deslocamento INTEGER",
                    "VisaoNoEscuro INTEGER",
                    "CriadoEm TEXT",
                    "ModificadoEm TEXT"
                }),

                ["FichaPersonagem_Idiomas"] = string.Join(",\n", new[]
                {
                    "FichaId TEXT NOT NULL",
                    "IdiomaId TEXT NOT NULL",
                    "PRIMARY KEY (FichaId, IdiomaId)",
                    "FOREIGN KEY (FichaId) REFERENCES FichaPersonagem(Id) ON DELETE CASCADE",
                    "FOREIGN KEY (IdiomaId) REFERENCES Idioma(Id) ON DELETE CASCADE"
                })
            };

            foreach (var tabela in definicoes)
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }
    }
}
