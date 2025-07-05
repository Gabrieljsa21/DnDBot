using DnDBot.Application.Models.AntecedenteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Ficha.Auxiliares
{
    public class AlinhamentoTag
    {
        public string AlinhamentoId { get; set; }
        public string Tag { get; set; }

        public Alinhamento Alinhamento { get; set; }
    }
}
