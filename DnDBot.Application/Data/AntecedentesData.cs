using System.Collections.Generic;

namespace DnDBot.Application.Data
{
    /// <summary>
    /// Contém a lista fixa de antecedentes disponíveis para personagens.
    /// </summary>
    public static class AntecedentesData
    {
        /// <summary>
        /// Lista estática com os antecedentes disponíveis para seleção na criação de fichas.
        /// </summary>
        public static readonly List<string> Antecedentes = new()
        {
            "Acólito",
            "Agente de Facção",
            "Antropólogo",
            "Apostador",
            "Arqueólogo",
            "Artesão de Guilda",
            "Assombrado",
            "Caçador de Recompensas Urbano",
            "Cavaleiro Feudo Livre",
            "Charlatão",
            "Comerciante Fracassado",
            "Contrabandista",
            "Criminoso",
            "Eremita",
            "Estudioso Recluso",
            "Forasteiro",
            "Fuzileiro (Marinha)",
            "Herói do Povo",
            "Marinheiro",
            "Mercenário Veterano",
            "Nobre",
            "Órfão",
            "Sábio",
            "Soldado",
            "Viajante Distante"
        };
    }
}
