using DnDBot.Application.Helpers;
using DnDBot.Application.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por carregar e fornecer dados de raças a partir do arquivo JSON.
    /// </summary>
    public class RacasService
    {
        private readonly List<Raca> _racas;

        /// <summary>
        /// Construtor que carrega os dados do arquivo <c>Data/racas.json</c>.
        /// </summary>
        public RacasService()
        {
            var path = PathHelper.GetDataPath("racas.json");

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                _racas = JsonSerializer.Deserialize<List<Raca>>(json) ?? new List<Raca>();
            }
            else
            {
                _racas = new List<Raca>();
            }
        }

        /// <summary>
        /// Retorna a lista de raças disponíveis.
        /// </summary>
        /// <returns>Lista somente leitura de <see cref="Raca"/>.</returns>
        public IReadOnlyList<Raca> ObterRacas() => _racas.AsReadOnly();
    }
}
