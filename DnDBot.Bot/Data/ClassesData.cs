using System.Collections.Generic;

namespace DnDBot.Bot.Data
{
    public static class ClassesData
    {
        /// <summary>
        /// Lista dos IDs das classes jogáveis disponíveis no sistema.
        /// Esses IDs devem corresponder aos arquivos de dados JSON.
        /// </summary>
        public static readonly List<string> Classes = new()
        {
            "artifice",
            "barbaro",
            "bardo",
            "bruxo",
            "clerigo",
            "druida",
            "feiticeiro",
            "guerreiro",
            "ladino",
            "mago",
            "monge",
            "paladino",
            "patrulheiro",
            "cacador-de-sangue"
        };
    }
}
