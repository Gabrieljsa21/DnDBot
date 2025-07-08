using DnDBot.Bot.Models.Ficha;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DnDBot.Bot.Repositories
{
    /// <summary>
    /// Repositório estático responsável por carregar e fornecer os dados de classes.
    /// Os dados são carregados uma única vez a partir do arquivo JSON localizado em "Data/classes.json".
    /// </summary>
    public static class ClasseRepository
    {
        // Cache local da lista de classes para evitar leituras repetidas do arquivo.
        private static List<Classe> _classes;

        /// <summary>
        /// Obtém a lista completa de classes carregadas do arquivo JSON.
        /// Caso as classes ainda não tenham sido carregadas, realiza a leitura do arquivo e desserializa os dados.
        /// </summary>
        /// <returns>Lista de objetos Classe.</returns>
        public static List<Classe> GetClasses()
        {
            if (_classes == null)
            {
                var json = File.ReadAllText("Data/classes.json");

                _classes = JsonSerializer.Deserialize<List<Classe>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return _classes;
        }
    }
}
