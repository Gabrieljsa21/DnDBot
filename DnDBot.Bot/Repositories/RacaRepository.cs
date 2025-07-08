using DnDBot.Bot.Models.Ficha;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DnDBot.Bot.Repositories
{
    /// <summary>
    /// Repositório responsável por carregar e fornecer os dados de raças a partir de um arquivo JSON.
    /// </summary>
    public static class RacaRepository
    {
        /// <summary>
        /// Lista em cache com as raças carregadas do arquivo JSON.
        /// </summary>
        private static List<Raca> _racas;

        /// <summary>
        /// Retorna a lista de raças carregadas do arquivo 'Data/racas.json'.
        /// Os dados são carregados apenas uma vez e armazenados em cache para uso futuro.
        /// </summary>
        /// <returns>Lista de raças do tipo <see cref="Raca"/>.</returns>
        public static List<Raca> GetRacas()
        {
            if (_racas == null)
            {
                var json = File.ReadAllText("Data/racas.json");

                _racas = JsonSerializer.Deserialize<List<Raca>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return _racas;
        }
    }
}
