using System;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class LogInventario
    {
        public int Id { get; set; } // Para banco de dados, se usar

        public DateTime Data { get; set; }

        /// <summary>
        /// Descrição da ação, ex: "Adicionado", "Removido", "Equipado", "Usado"
        /// </summary>
        public string Acao { get; set; }

        /// <summary>
        /// Nome do item envolvido na ação
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Quantidade envolvida na ação
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Opcional: usuário ou personagem que realizou a ação (se quiser rastrear)
        /// </summary>
        public string Usuario { get; set; }
    }
}
