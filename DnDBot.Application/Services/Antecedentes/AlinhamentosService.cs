using System.Collections.Generic;
using System.Linq;
using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;

namespace DnDBot.Application.Services.Antecedentes
{
    /// <summary>
    /// Serviço responsável por gerenciar operações relacionadas aos alinhamentos dos personagens.
    /// </summary>
    public class AlinhamentosService
    {
        /// <summary>
        /// Obtém a lista completa de alinhamentos disponíveis.
        /// </summary>
        /// <returns>Lista somente leitura contendo todos os alinhamentos.</returns>
        public IReadOnlyList<Alinhamento> ObterAlinhamentos()
        {
            return AlinhamentosData.Alinhamentos.AsReadOnly();
        }

        /// <summary>
        /// Busca um alinhamento pelo seu ID.
        /// </summary>
        /// <param name="id">ID do alinhamento a ser buscado.</param>
        /// <returns>O alinhamento correspondente ao ID, ou <c>null</c> se não encontrado.</returns>
        public Alinhamento ObterAlinhamentoPorId(string id)
        {
            return AlinhamentosData.Alinhamentos
                .FirstOrDefault(a => a.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Busca um alinhamento pelo seu nome exato.
        /// </summary>
        /// <param name="nome">Nome do alinhamento a ser buscado.</param>
        /// <returns>O alinhamento correspondente ao nome, ou <c>null</c> se não encontrado.</returns>
        public Alinhamento ObterAlinhamentoPorNome(string nome)
        {
            return AlinhamentosData.Alinhamentos.FirstOrDefault(a => a.Nome == nome);
        }
    }
}
