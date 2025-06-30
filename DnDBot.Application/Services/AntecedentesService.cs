using System.Collections.Generic;
using DnDBot.Application.Data;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por fornecer os antecedentes disponíveis.
    /// </summary>
    public class AntecedentesService
    {
        /// <summary>
        /// Retorna uma lista somente leitura com os antecedentes disponíveis.
        /// </summary>
        /// <returns>Lista imutável de antecedentes do D&D.</returns>
        public IReadOnlyList<string> ObterAntecedentes()
        {
            return AntecedentesData.Antecedentes.AsReadOnly();
        }
    }
}
