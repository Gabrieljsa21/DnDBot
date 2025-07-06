using System.Collections.Generic;

namespace DnDBot.Application.Models.ItensInventario
{
    /// <summary>
    /// Representa um material com suas propriedades específicas,
    /// podendo ser usado para itens como armas, armaduras, etc.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Identificador único do material.
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// Nome do material (ex: Aço, Mithral, Couro, Vidro).
        /// </summary>
        public string Nome { get; init; }

        /// <summary>
        /// Descrição ou detalhes do material.
        /// </summary>
        public string Descricao { get; init; }

        /// <summary>
        /// Propriedades especiais ou vantagens do material.
        /// Exemplo: "Leve", "Resistente a fogo", "Silencioso".
        /// </summary>
        public List<string> PropriedadesEspeciais { get; init; } = new();

        /// <summary>
        /// Peso relativo do material, usado para cálculos de peso de itens.
        /// </summary>
        public double PesoRelativo { get; init; } = 1.0;

        /// <summary>
        /// Multiplicador de custo do material, usado para cálculo de valor de itens.
        /// </summary>
        public decimal CustoMultiplicador { get; init; } = 1m;

        public Material(string id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
