using System.Collections.Generic;

namespace DnDBot.Application.Data
{
    /// <summary>
    /// Contém a lista padrão de alinhamentos disponíveis no D&D 5e.
    /// </summary>
    public static class AlinhamentosData
    {
        /// <summary>
        /// Lista estática com os nove alinhamentos clássicos, incluindo siglas.
        /// </summary>
        public static readonly List<string> Alinhamentos = new()
        {
            "Leal e Bom (LG)",
            "Neutro e Bom (NG)",
            "Caótico e Bom (CG)",
            "Leal e Neutro (LN)",
            "Neutro Puro (N)",
            "Caótico e Neutro (CN)",
            "Leal e Mal (LM)",
            "Neutro e Mal (NM)",
            "Caótico e Mal (CM)"
        };
    }
}
