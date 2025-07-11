using DnDBot.Bot.Models.Enums;
using System.Collections.Generic;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa um material com suas propriedades específicas,
    /// podendo ser usado para itens como armas, armaduras, etc.
    /// </summary>
    public class Material : EntidadeBase
    {

        public bool IgnoraCritico { get; init; } = false;
        public bool IgnoraDesvantagemFurtividade { get; init; } = false; 
        public int BonusCA { get; init; } = 0; 
        public int BonusAtaque { get; init; } = 0;
        public int BonusDano { get; init; } = 0;

        public List<TipoDano> ResistenciasDano { get; init; } = new();
        public List<TipoDano> ImunidadesDano { get; init; } = new();

        public double PesoRelativo { get; init; } = 1.0;
        public decimal CustoMultiplicador { get; init; } = 1m;

        public Material(string id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }

}
