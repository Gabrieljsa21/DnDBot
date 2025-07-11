using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class PropriedadesMagicas : EntidadeBase
    {
        public bool EMagico => !string.IsNullOrEmpty(Raridade);
        public string Raridade { get; set; }
        public int BonusMagico { get; set; }
        public List<string> Efeitos { get; set; } = new();
        public List<string> MagiasAssociadas { get; set; } = new();

        public int CargasMaximas { get; set; } = 0;
        public int CargasAtuais { get; set; } = 0;
        public string UsosEspeciais { get; set; } = string.Empty;
        public bool EhConsumivel { get; set; } = false;
        public bool RequerSintonizacao { get; set; } = false;
        public List<string> RequisitosSintonizacao { get; set; } = new();
        public List<string> BonusContraTipos { get; set; } = new();

        public bool GastarCarga(int quantidade = 1)
        {
            if (quantidade <= 0) return false;
            if (CargasAtuais >= quantidade)
            {
                CargasAtuais -= quantidade;
                return true;
            }
            return false;
        }

        public void RecarregarCargas(int quantidade)
        {
            CargasAtuais += quantidade;
            if (CargasAtuais > CargasMaximas)
                CargasAtuais = CargasMaximas;
        }
    }

}
