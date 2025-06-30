using System.Collections.Generic;

namespace DnDBot.Application.Data
{
    /// <summary>
    /// Contém a lista fixa de classes de personagem disponíveis no D&D 5e.
    /// </summary>
    public static class ClassesData
    {
        /// <summary>
        /// Lista estática com as classes jogáveis usadas no sistema.
        /// </summary>
        public static readonly List<string> Classes = new()
        {
            "Artífice",
            "Bárbaro",
            "Bardo",
            "Bruxo",
            "Clérigo",
            "Druida",
            "Feiticeiro",
            "Guerreiro",
            "Ladino",
            "Mago",
            "Monge",
            "Paladino",
            "Patrulheiro"
        };
    }
}
