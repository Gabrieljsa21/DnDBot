using System.Collections.Generic;

namespace DnDBot.Application.Models.ItensInventario
{
    /// <summary>
    /// Representa um item mágico, que pode ser arma, armadura, acessório, consumível ou outro tipo,
    /// com propriedades mágicas, raridade, usos especiais e restrições.
    /// </summary>
    public class ItemMagico : Item
    {
        /// <summary>
        /// Raridade do item mágico (Comum, Incomum, Raro, Muito Raro, Lendário, Artefato).
        /// </summary>
        public string Raridade { get; set; } = "Comum";

        /// <summary>
        /// Descrição das propriedades mágicas ou efeitos especiais do item.
        /// </summary>
        public List<string> PropriedadesMagicas { get; set; } = new();

        /// <summary>
        /// Quantidade máxima de cargas, se o item usar sistema de cargas.
        /// </summary>
        public int CargasMaximas { get; set; } = 0;

        /// <summary>
        /// Quantidade atual de cargas disponíveis.
        /// </summary>
        public int CargasAtuais { get; set; } = 0;

        /// <summary>
        /// Descrição de usos especiais, como feitiços armazenados ou efeitos ativáveis.
        /// </summary>
        public string UsosEspeciais { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o item é consumível (ex: pergaminho, poção mágica).
        /// </summary>
        public bool EhConsumivel { get; set; } = false;

        /// <summary>
        /// Indica se o item mágico requer sintonização (attunement).
        /// </summary>
        public bool RequerSintonizacao { get; set; } = false;

        /// <summary>
        /// Condições ou pré-requisitos para sintonização.
        /// </summary>
        public List<string> RequisitosSintonizacao { get; set; } = new();

        /// <summary>
        /// Lista de tipos de criatura para os quais o item concede bônus especial.
        /// </summary>
        public List<string> BonusContraTipos { get; set; } = new();

        /// <summary>
        /// Método para gastar cargas, retorna true se foi possível gastar.
        /// </summary>
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

        /// <summary>
        /// Método para recarregar cargas até o máximo.
        /// </summary>
        public void RecarregarCargas(int quantidade)
        {
            CargasAtuais += quantidade;
            if (CargasAtuais > CargasMaximas)
                CargasAtuais = CargasMaximas;
        }
    }
}
