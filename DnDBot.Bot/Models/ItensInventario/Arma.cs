using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa uma arma no sistema D&D 5e.
    /// </summary>
    public class Arma : Item
    {
        public TipoArma Tipo { get; set; }
        public new CategoriaArma Categoria { get; set; }

        public string DadoDano { get; set; }
        public TipoDano TipoDano { get; set; }
        public TipoDano? TipoDanoSecundario { get; set; }

        public int? Alcance { get; set; }
        public bool EhDuasMaos { get; set; }
        public bool EhLeve { get; set; }
        public bool EhVersatil { get; set; }
        public string DadoDanoVersatil { get; set; }

        public bool PodeSerArremessada { get; set; }
        public int? AlcanceArremesso { get; set; }

        public List<string> Requisitos { get; set; } = new();
        public List<string> PropriedadesEspeciais { get; set; } = new();
        public int BonusMagico { get; set; }
        public List<string> BonusContraTipos { get; set; } = new();
        public List<string> MagiasAssociadas { get; set; } = new();

        public int DurabilidadeAtual { get; set; }
        public int DurabilidadeMaxima { get; set; }

        public string AreaAtaque { get; set; }
        public string TipoAcao { get; set; }
        public string RegraCritico { get; set; }

        public string TipoMunicao { get; set; }
        public int MunicaoPorAtaque { get; set; } = 1;
        public bool RequerRecarga { get; set; }
        public int TempoRecargaTurnos { get; set; }

        public List<string> AtaquesEspeciais { get; set; } = new();
        public decimal CustoReparo { get; set; }
        public bool EMagica { get; set; }

        public string Raridade { get; set; }
        public string Fabricante { get; set; }

        public List<ArmaRequisitoAtributo> RequisitosAtributos { get; set; } = new();

        public List<ArmaTag> ArmaTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => ArmaTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaTags = value?.Select(tag => new ArmaTag { Tag = tag, ArmaId = Id }).ToList() ?? new();
        }

        [JsonIgnore]
        public string DanoTotal => BonusMagico != 0 ? $"{DadoDano} + {BonusMagico}" : DadoDano;

        public string CalcularDanoTotal(bool usarVersatil = false)
        {
            string dado = usarVersatil && EhVersatil && !string.IsNullOrEmpty(DadoDanoVersatil)
                ? DadoDanoVersatil
                : DadoDano;

            return BonusMagico != 0 ? $"{dado} + {BonusMagico}" : dado;
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

        public bool VerificarRequisitosAtributos(Dictionary<Atributo, int> atributosPersonagem)
        {
            foreach (var requisito in RequisitosAtributos)
            {
                if (!atributosPersonagem.TryGetValue(requisito.Atributo, out int valor) || valor < requisito.Valor)
                    return false;
            }
            return true;
        }

        public bool TemMunicaoSuficiente(int quantidadeDisponivel)
        {
            if (string.IsNullOrEmpty(TipoMunicao))
                return true;

            return quantidadeDisponivel >= MunicaoPorAtaque;
        }

        public int? CalcularAlcance(bool arremesso = false)
        {
            if (arremesso && PodeSerArremessada)
                return AlcanceArremesso;

            if (!arremesso)
                return Alcance;

            return null;
        }

        public int AplicarRecarga(int turnosDisponiveis)
        {
            if (!RequerRecarga) return 0;

            return turnosDisponiveis >= TempoRecargaTurnos
                ? 0
                : TempoRecargaTurnos - turnosDisponiveis;
        }

        public void AdicionarMagiaAssociada(string magia)
        {
            if (!MagiasAssociadas.Contains(magia))
                MagiasAssociadas.Add(magia);
        }

        public void RemoverMagiaAssociada(string magia)
        {
            MagiasAssociadas.Remove(magia);
        }

        public string ListarMagiasAssociadas()
        {
            return MagiasAssociadas.Count == 0
                ? "Nenhuma"
                : string.Join(", ", MagiasAssociadas);
        }

        public bool PossuiPropriedadeEspecial(string propriedade)
        {
            return PropriedadesEspeciais?.Contains(propriedade) ?? false;
        }

        public int? CalcularDanoMaximo(bool usarVersatil = false)
        {
            string dado = usarVersatil && EhVersatil && !string.IsNullOrEmpty(DadoDanoVersatil)
                ? DadoDanoVersatil
                : DadoDano;

            int max = ObterMaximoDado(dado);
            return max >= 0 ? max + BonusMagico : null;
        }

        private int ObterMaximoDado(string dado)
        {
            var partes = dado.ToLower().Split('d');
            if (partes.Length != 2) return -1;

            if (!int.TryParse(partes[0], out int n)) return -1;
            if (!int.TryParse(partes[1], out int faces)) return -1;

            return n * faces;
        }

        public bool CausaDanoMagico()
        {
            return EMagica || BonusMagico > 0;
        }

        public void IncrementarBonusMagico(int incremento)
        {
            BonusMagico += incremento;
        }
    }
}
