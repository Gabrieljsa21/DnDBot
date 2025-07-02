using DnDBot.Application.Helpers;
using DnDBot.Application.Models.Ficha;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Retorna uma raça com base no ID informado.
        /// </summary>
        /// <param name="id">ID da raça a ser buscada.</param>
        /// <returns>Objeto <see cref="Raca"/> correspondente ao ID, ou <c>null</c> se não encontrada.</returns>
        public Raca ObterRacaPorId(string id)
        {
            return _racas.FirstOrDefault(r => r.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retorna uma raça com base no nome informado.
        /// </summary>
        /// <param name="nome">Nome da raça a ser buscada.</param>
        /// <returns>Objeto <see cref="Raca"/> correspondente ao nome, ou <c>null</c> se não encontrada.</returns>
        public Raca ObterRacaPorNome(string nome)
        {
            return _racas.FirstOrDefault(r => r.Nome == nome);
        }
        public List<SubRaca> ObterTodasSubracas()
        {
            return _racas.SelectMany(r => r.SubRacas).ToList();
        }

        /// <summary>
        /// Retorna uma sub-raça com base no ID informado.
        /// </summary>
        /// <param name="id">ID da sub-raça a ser buscada.</param>
        /// <returns>Objeto <see cref="SubRaca"/> correspondente ao ID, ou <c>null</c> se não encontrada.</returns>
        public SubRaca ObterSubRacaPorId(string idSubRaca)
        {
            return _racas
                .SelectMany(r => r.SubRacas)
                .FirstOrDefault(sr => sr.Id.Equals(idSubRaca, System.StringComparison.OrdinalIgnoreCase));
        }


    }
}
