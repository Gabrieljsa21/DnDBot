using DnDBot.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class CaracteristicaEscala
    {

        public string Id { get; set; }
        /// <summary>
        /// Nível mínimo para esta escala entrar em vigor.
        /// </summary>
        public int NivelMinimo { get; set; }

        /// <summary>
        /// Nível máximo (opcional) em que esta escala se aplica.
        /// </summary>
        public int? NivelMaximo { get; set; }

        public List<CaracteristicaEscalaDano> Danos { get; set; } = new();
        public int? UsosPorDescansoCurto { get; set; }
        public int? UsosPorDescansoLongo { get; set; }
        public int? DuracaoEmRodadas { get; set; }

        public AcaoRequerida AcaoRequerida { get; set; }
        public Alvo Alvo { get; set; }
        public CondicaoAtivacao CondicaoAtivacao { get; set; }

        // FK  
        public string CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }
    public class CaracteristicaEscalaDano
    {
        public string Id { get; set; }
        public string DadoDano { get; set; } = string.Empty;
        public TipoDano TipoDano { get; set; }

        // FK para a escala principal
        public string CaracteristicaEscalaId { get; set; }
        public CaracteristicaEscala CaracteristicaEscala { get; set; }

    }

}
