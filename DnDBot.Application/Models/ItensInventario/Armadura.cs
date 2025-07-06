using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Application.Models.ItensInventario
{
    /// <summary>
    /// Representa uma armadura no sistema D&D 5e.
    /// </summary>
    public class Armadura : Item
    {
        public TipoArmadura Tipo { get; set; }

        public int ClasseArmadura { get; set; }
        public bool PermiteFurtividade { get; set; }
        public int PenalidadeFurtividade { get; set; } = 0;

        public double Peso => PesoUnitario;
        public decimal Custo { get; set; }

        public int RequisitoForca { get; set; } = 0;
        public List<string> PropriedadesEspeciais { get; set; } = new();

        public int DurabilidadeAtual { get; set; }
        public int DurabilidadeMaxima { get; set; }

        public bool EMagica { get; set; }
        public int BonusMagico { get; set; }

        public string Raridade { get; set; }
        public string Fabricante { get; set; }
        public string Material { get; set; }

        public List<TipoDano> ResistenciasDano { get; set; } = new();
        public List<TipoDano> ImunidadesDano { get; set; } = new();

        public List<ArmaduraTag> ArmaduraTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => ArmaduraTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaduraTags = value?.Select(tag => new ArmaduraTag { Tag = tag, ArmaduraId = Id }).ToList() ?? new();
        }

        public int CalcularClasseArmaduraTotal()
        {
            return ClasseArmadura + BonusMagico;
        }

        public bool AplicarDanoDurabilidade(int dano)
        {
            if (DurabilidadeAtual <= 0)
                return true;

            DurabilidadeAtual -= dano;
            if (DurabilidadeAtual <= 0)
            {
                DurabilidadeAtual = 0;
                return true;
            }

            return false;
        }

        public void Reparar(int quantidade)
        {
            DurabilidadeAtual += quantidade;
            if (DurabilidadeAtual > DurabilidadeMaxima)
                DurabilidadeAtual = DurabilidadeMaxima;
        }

        public bool PossuiPropriedade(string propriedade)
        {
            return PropriedadesEspeciais?.Contains(propriedade) ?? false;
        }

        public bool PodeUsar(int forcaPersonagem)
        {
            return forcaPersonagem >= RequisitoForca;
        }

        public string DescricaoResumo()
        {
            var props = PropriedadesEspeciais.Count > 0
                ? string.Join(", ", PropriedadesEspeciais)
                : "Sem propriedades especiais";

            return $"{Nome} — CA: {CalcularClasseArmaduraTotal()} (Base: {ClasseArmadura}, Bônus Mágico: {BonusMagico}) - {props}";
        }
    }
}
