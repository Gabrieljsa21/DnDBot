﻿using DnDBot.Bot.Models.ItensInventario;
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

}
