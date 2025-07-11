using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class Pergaminho : Consumivel
    {
        public string MagiaId { get; set; }
        public Magia Magia { get; set; }
        public int NivelMagia { get; set; } = 0;

        public EscolaMagia? Escola { get; set; }
        public string TempoConjuracao { get; set; } = "Ação";

        public bool Usar()
        {
            if (!PodeSerUsado()) return false;
            return Consumir();
        }

        public void Restaurar()
        {
            Recarregar();
        }
    }
}
