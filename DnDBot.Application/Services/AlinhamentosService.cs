using System.Collections.Generic;
using DnDBot.Application.Data;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por fornecer a lista de alinhamentos disponíveis no sistema.
    /// </summary>
    public class AlinhamentosService
    {
        /// <summary>
        /// Retorna uma lista somente leitura contendo os alinhamentos clássicos do D&D.
        /// </summary>
        /// <returns>
        /// Uma <see cref="IReadOnlyList{String}"/> contendo os nove alinhamentos tradicionais,
        /// como "Leal e Bom (LG)", "Caótico e Mal (CM)", etc.
        /// </returns>
        public IReadOnlyList<string> ObterAlinhamentos()
        {
            return AlinhamentosData.Alinhamentos.AsReadOnly();
        }
    }
}
