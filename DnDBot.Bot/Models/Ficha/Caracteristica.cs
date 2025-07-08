using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using System.Collections.Generic;

namespace DnDBot.Bot.Models.Ficha
{
    public class Caracteristica : EntidadeBase
    {
        public TipoCaracteristica Tipo { get; set; }
        public AcaoRequerida AcaoRequerida { get; set; }
        public Alvo Alvo { get; set; }
        public int? DuracaoEmRodadas { get; set; }
        public int? UsosPorDescansoCurto { get; set; }
        public int? UsosPorDescansoLongo { get; set; }
        public CondicaoAtivacao CondicaoAtivacao { get; set; }

        public OrigemCaracteristica Origem { get; set; }

        // Id da entidade de origem, por exemplo:
        // se Origem == Racial, esse campo pode conter a Id da raça
        // se Origem == Classe, pode conter a Id da classe, etc
        public string OrigemId { get; set; }

        /// <summary>
        /// Nível mínimo do personagem para desbloquear essa característica.
        /// </summary>
        public int NivelMinimo { get; set; } = 1;

        /// <summary>
        /// Nível máximo do personagem para manter essa característica (opcional).
        /// </summary>
        public int? NivelMaximo { get; set; } = null;
    }
}
