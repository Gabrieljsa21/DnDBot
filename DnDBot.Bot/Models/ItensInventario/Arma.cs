﻿using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.ItensInventario
{
    public abstract class Arma : Item
    {
        public TipoArma Tipo { get; set; }
        public CategoriaArma CategoriaArma { get; set; }

        public string DadoDano { get; set; } = string.Empty;
        public TipoDano TipoDano { get; set; }
        public TipoDano? TipoDanoSecundario { get; set; }

        public bool EhDuasMaos { get; set; }
        public bool EhLeve { get; set; }
        public bool EhVersatil { get; set; }
        public bool EhAgil { get; set; }
        public bool EhPesada { get; set; }

        public string DadoDanoVersatil { get; set; } = string.Empty;

        public List<string> Requisitos { get; set; } = new();
        public List<string> PropriedadesEspeciais { get; set; } = new();

        public List<string> BonusContraTipos { get; set; } = new();
        public List<string> MagiasAssociadas { get; set; } = new();

        public int DurabilidadeAtual { get; set; }
        public int DurabilidadeMaxima { get; set; }

        public FormaAreaEfeito AreaAtaque { get; set; }
        public AcaoRequerida TipoAcao { get; set; }
        public string RegraCritico { get; set; } = string.Empty;

        public List<string> AtaquesEspeciais { get; set; } = new();

        public List<ArmaRequisitoAtributo> RequisitosAtributos { get; set; } = new();
        public List<ArmaTag> ArmaTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => ArmaTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaTags = value?.Select(tag => new ArmaTag { Tag = tag, ArmaId = Id }).ToList() ?? new();
        }
    }
}
