using DnDBot.Application.Models.ItensInventario;
using System;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa um item do histórico financeiro de um personagem, como uma compra, venda, ganho ou transferência.
    /// </summary>
    public class HistoricoFinanceiroItem : EntidadeBase
    {
        /// <summary>
        /// Identificador da ficha do personagem associada a este registro.
        /// </summary>
        public Guid FichaPersonagemId { get; set; }

        /// <summary>
        /// Referência à ficha do personagem associada.
        /// </summary>
        public FichaPersonagem FichaPersonagem { get; set; }

        /// <summary>
        /// Data e hora em que o evento financeiro ocorreu.
        /// </summary>
        public DateTime Data { get; set; } = DateTime.Now;

        /// <summary>
        /// Tipo do evento financeiro, como "Gasto", "Ganho", "Troca", "Compra", "Venda", "Transferência".
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Valor movimentado no evento, representado pela bolsa de moedas (tipos e quantidades).
        /// </summary>
        public BolsaDeMoedas Valor { get; set; } = new();

        /// <summary>
        /// Saldo da bolsa de moedas do personagem após a ocorrência do evento.
        /// </summary>
        public BolsaDeMoedas SaldoApos { get; set; } = new();

        /// <summary>
        /// Origem do evento, indicando se foi manual, sistema, evento automático, comando, etc.
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Categoria do evento para organização e filtragem, por exemplo "missão", "mercado", "saque", "depósito".
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Indica se o evento foi gerado automaticamente pelo sistema (true) ou criado manualmente (false).
        /// </summary>
        public bool FoiAutomatico { get; set; } = false;

        /// <summary>
        /// Referência opcional a um item relacionado ao evento, como equipamento ou magia envolvida.
        /// </summary>
        public Guid? ItemRelacionadoId { get; set; }

        /// <summary>
        /// Referência opcional a outro personagem envolvido, por exemplo numa troca ou presente.
        /// </summary>
        public Guid? PersonagemDestinoId { get; set; }
    }
}
