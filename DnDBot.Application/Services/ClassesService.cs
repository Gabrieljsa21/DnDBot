using System.Collections.Generic;
using DnDBot.Application.Data;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por fornecer as classes disponíveis.
    /// </summary>
    public class ClassesService
    {
        /// <summary>
        /// Retorna uma lista somente leitura com as classes disponíveis.
        /// </summary>
        /// <returns>Lista imutável de classes do D&D.</returns>
        public IReadOnlyList<string> ObterClasses()
        {
            return ClassesData.Classes.AsReadOnly();
        }
    }
}
