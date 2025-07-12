using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario.Auxiliares
{

    public class ArmaADistancia : Arma
    {
        public int AlcanceMinimo { get; set; }
        public int AlcanceMaximo { get; set; }

        public string TipoMunicao { get; set; } = string.Empty;
        public int MunicaoPorAtaque { get; set; } = 1;
        public bool RequerRecarga { get; set; }
        public int TempoRecargaTurnos { get; set; }

        [NotMapped]
        public int RecuperacaoMunicao => (int)System.Math.Floor(MunicaoPorAtaque * 0.5);
    }
}
