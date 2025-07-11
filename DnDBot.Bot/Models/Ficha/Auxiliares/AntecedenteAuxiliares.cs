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
    public class AntecedenteProficienciaPericias
    {
        public string AntecedenteId { get; set; }
        public string PericiaId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Pericia Pericia { get; set; }
    }
    
    public class AntecedenteProficienciaFerramentas
    {
        public string AntecedenteId { get; set; }
        public string FerramentaId { get; set; }
        public Antecedente Antecedente { get; set; }
        public Ferramenta Ferramenta { get; set; }
    }
    
    public class AntecedenteOpcaoEscolhaProficienciaFerramentas
    {
        public string Id { get; set; }
        public List<Ferramenta> Opcoes { get; set; } = new();
        public int QuantidadeEscolhas { get; set; } = 1;
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

    public class AntecedenteOpcaoEscolhaItem
    {
        public string Id { get; set; }

        public int QuantidadeEscolhas { get; set; }

        public List<AntecedenteOpcaoEscolhaItemOpcoes> Opcoes { get; set; } = new();
    }

    public class AntecedenteOpcaoEscolhaItemOpcoes
    {
        public string Id { get; set; }

        public string AntecedenteOpcaoEscolhaItemId { get; set; }
        public AntecedenteOpcaoEscolhaItem AntecedenteOpcaoEscolhaItem { get; set; }

        public string ItemId { get; set; }
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
