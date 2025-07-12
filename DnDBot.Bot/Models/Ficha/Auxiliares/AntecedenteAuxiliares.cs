using DnDBot.Bot.Models.AntecedenteModels;
using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class AntecedenteTag
    {
        public string AntecedenteId { get; set; }
        public string Tag { get; set; }

        public Antecedente Antecedente { get; set; }
    }
    
    public class AntecedenteProficiencia
    {
        public string AntecedenteId { get; set; }
        public string ProficienciaId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Proficiencia Proficiencia { get; set; }
    }
    
    public class AntecedenteProficienciaOpcoes
    {
        public string AntecedenteId { get; set; }
        public Antecedente Antecedente { get; set; }
        public string ProficienciaId { get; set; }
        public Proficiencia Proficiencia { get; set; }
    }
    
    public class AntecedenteItemOpcoes
    {
        public string AntecedenteId { get; set; }
        public Antecedente Antecedente { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
    public class AntecedenteIdioma
    {
        public string AntecedenteId { get; set; }
        public string IdiomaId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Idioma Idioma { get; set; }
    }
    public class AntecedenteCaracteristica
    {
        public string AntecedenteId { get; set; }
        public string CaracteristicaId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }

    public class AntecedenteItem
    {
        public string AntecedenteId { get; set; }
        public string ItemId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Item Item { get; set; }
    }

    public class AntecedenteIdeal
    {
        public string AntecedenteId { get; set; }
        public string IdealId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Ideal Ideal { get; set; }
    }
    public class AntecedenteVinculo
    {
        public string AntecedenteId { get; set; }
        public string VinculoId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Vinculo Vinculo { get; set; }
    }
    public class AntecedenteDefeito
    {
        public string AntecedenteId { get; set; }
        public string DefeitoId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Defeito Defeito { get; set; }
    }
    public class AntecedenteMoeda
    {
        public string AntecedenteId { get; set; }
        public string MoedaId { get; set; }
        public int Quantidade { get; set; }
        public Antecedente Antecedente { get; set; }
        public Moeda Moeda { get; set; }
    }
}
