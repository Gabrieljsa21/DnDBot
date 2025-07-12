using DnDBot.Bot.Models.Ficha.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class Escudo : Item
    {
        public int BonusCA { get; set; } = 2;
        public int DurabilidadeAtual { get; set; }
        public int DurabilidadeMaxima { get; set; }
        public List<EscudoPropriedadeEspecial> PropriedadesEspeciais { get; set; } = new();
    }
}
