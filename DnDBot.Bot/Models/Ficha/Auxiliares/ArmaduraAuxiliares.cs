using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class ArmaduraTag
    {
        public string ArmaduraId { get; set; }
        public string Tag { get; set; }

        public Armadura Armadura { get; set; }
    }
    public class ArmaduraPropriedadeEspecial
    {
        public string ArmaduraId { get; set; }

        public Armadura Armadura { get; set; }
        public string PropriedadeEspecialId { get; set; }

        public PropriedadeEspecial PropriedadeEspecial { get; set; }
    }
    public class ArmaduraResistencia
    {
        public string ArmaduraId { get; set; }

        public Armadura Armadura { get; set; }
        public string ResistenciaId { get; set; }

        public Resistencia Resistencia { get; set; }
    }
    public class ArmaduraImunidade
    {
        public string ArmaduraId { get; set; }

        public Armadura Armadura { get; set; }
        public string ImunidadeId { get; set; }

        public Imunidade Imunidade { get; set; }
    }

}
