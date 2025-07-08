using DnDBot.Bot.Models.Ficha;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DnDBot.Bot.Repositories
{
    /// <summary>
    /// Repositório estático responsável por carregar e fornecer os dados de idiomas.
    /// Os dados são carregados uma única vez a partir do arquivo JSON localizado em "Data/idiomas.json".
    /// </summary>
    public static class IdiomaRepository
    {
        // Cache local da lista de idiomas para evitar leituras repetidas do arquivo.
        private static List<Idioma> _idiomas;

        /// <summary>
        /// Obtém a lista completa de idiomas carregados do arquivo JSON.
        /// Caso os idiomas ainda não tenham sido carregados, realiza a leitura do arquivo e desserializa os dados.
        /// </summary>
        /// <returns>Lista de objetos Idioma.</returns>
        public static List<Idioma> GetIdiomas()
        {
            if (_idiomas == null)
            {
                var json = File.ReadAllText("Data/idiomas.json");

                _idiomas = JsonSerializer.Deserialize<List<Idioma>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return _idiomas;
        }
    }
}
