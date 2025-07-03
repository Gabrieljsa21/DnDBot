using DnDBot.Application.Models.Antecedente;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DnDBot.Application.Repositories
{
    /// <summary>
    /// Repositório estático responsável por carregar e fornecer os dados de antecedentes.
    /// Os dados são carregados uma única vez a partir do arquivo JSON localizado em "Data/antecedentes.json".
    /// </summary>
    public static class AntecedenteRepository
    {
        // Cache local da lista de antecedentes para evitar leituras repetidas do arquivo.
        private static List<Antecedente> _antecedentes;

        /// <summary>
        /// Obtém a lista completa de antecedentes carregados do arquivo JSON.
        /// Caso os antecedentes ainda não tenham sido carregados, realiza a leitura do arquivo e desserializa os dados.
        /// </summary>
        /// <returns>Lista de objetos Antecedente.</returns>
        public static List<Antecedente> GetAntecedentes()
        {
            if (_antecedentes == null)
            {
                var json = File.ReadAllText("Data/antecedentes.json");

                _antecedentes = JsonSerializer.Deserialize<List<Antecedente>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return _antecedentes;
        }
    }
}
