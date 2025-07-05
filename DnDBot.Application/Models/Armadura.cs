using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma armadura no sistema D&D 5e, contendo atributos de defesa, raridade, durabilidade, propriedades mágicas e restrições.
    /// </summary>
    public class Armadura : EntidadeBase
    {

        /// <summary>Tipo da armadura: leve, média, pesada ou escudo.</summary>
        public TipoArmadura Tipo { get; set; }

        /// <summary>Classe de armadura base (CA) fornecida pela armadura.</summary>
        public int ClasseArmadura { get; set; }

        /// <summary>Indica se a armadura permite testes de furtividade sem penalidade.</summary>
        public bool PermiteFurtividade { get; set; }

        /// <summary>Penalidade aplicada a testes de furtividade ao usar essa armadura.</summary>
        public int PenalidadeFurtividade { get; set; } = 0;

        /// <summary>Peso da armadura em quilogramas.</summary>
        public double Peso { get; set; }

        /// <summary>Preço da armadura em peças de ouro (PO).</summary>
        public decimal Custo { get; set; }

        /// <summary>Valor mínimo de força necessário para utilizar a armadura sem penalidades.</summary>
        public int RequisitoForca { get; set; } = 0;

        /// <summary>Lista de propriedades ou efeitos especiais concedidos pela armadura.</summary>
        public List<string> PropriedadesEspeciais { get; set; }

        /// <summary>Valor atual de durabilidade da armadura.</summary>
        public int DurabilidadeAtual { get; set; }

        /// <summary>Valor máximo de durabilidade da armadura.</summary>
        public int DurabilidadeMaxima { get; set; }

        /// <summary>Indica se a armadura é considerada mágica.</summary>
        public bool EMagica { get; set; }

        /// <summary>Bônus mágico que aumenta a Classe de Armadura (CA).</summary>
        public int BonusMagico { get; set; }

        /// <summary>Classificação de raridade da armadura (ex: Comum, Raro, Lendário).</summary>
        public string Raridade { get; set; }

        /// <summary>Nome do fabricante, ferreiro ou origem da armadura.</summary>
        public string Fabricante { get; set; }

        /// <summary>Material predominante da armadura (ex: couro, aço, mithral).</summary>
        public string Material { get; set; }

        /// <summary>Lista de tipos de dano contra os quais a armadura concede resistência.</summary>
        public List<TipoDano> ResistenciasDano { get; set; }

        /// <summary>Lista de tipos de dano contra os quais a armadura concede imunidade total.</summary>
        public List<TipoDano> ImunidadesDano { get; set; }

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Armadura_Tag.
        /// </summary>
        public List<ArmaduraTag> ArmaduraTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de ArmaduraTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => ArmaduraTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaduraTags = value?.Select(tag => new ArmaduraTag { Tag = tag, ArmaduraId = this.Id }).ToList() ?? new();
        }


        /// <summary>
        /// Construtor padrão que inicializa listas para evitar valores nulos.
        /// </summary>
        public Armadura()
        {
            PropriedadesEspeciais = new List<string>();
            ResistenciasDano = new List<TipoDano>();
            ImunidadesDano = new List<TipoDano>();
        }

        /// <summary>
        /// Calcula a Classe de Armadura total considerando o bônus mágico.
        /// </summary>
        /// <returns>Valor total da CA.</returns>
        public int CalcularClasseArmaduraTotal()
        {
            return ClasseArmadura + BonusMagico;
        }

        /// <summary>
        /// Aplica dano à durabilidade da armadura.
        /// </summary>
        /// <param name="dano">Quantidade de dano à durabilidade.</param>
        /// <returns>True se a armadura quebrou (durabilidade chegou a zero), false caso contrário.</returns>
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

        /// <summary>
        /// Repara a armadura restaurando a durabilidade até o limite máximo.
        /// </summary>
        /// <param name="quantidade">Quantidade de durabilidade a restaurar.</param>
        public void Reparar(int quantidade)
        {
            DurabilidadeAtual += quantidade;
            if (DurabilidadeAtual > DurabilidadeMaxima)
                DurabilidadeAtual = DurabilidadeMaxima;
        }

        /// <summary>
        /// Verifica se a armadura possui determinada propriedade especial.
        /// </summary>
        /// <param name="propriedade">Nome da propriedade.</param>
        /// <returns>True se a propriedade estiver presente, false caso contrário.</returns>
        public bool PossuiPropriedade(string propriedade)
        {
            return PropriedadesEspeciais?.Contains(propriedade) ?? false;
        }

        /// <summary>
        /// Verifica se o personagem tem força suficiente para usar a armadura.
        /// </summary>
        /// <param name="forcaPersonagem">Valor de força do personagem.</param>
        /// <returns>True se o personagem puder usar sem penalidade, false caso contrário.</returns>
        public bool PodeUsar(int forcaPersonagem)
        {
            return forcaPersonagem >= RequisitoForca;
        }

        /// <summary>
        /// Gera uma descrição curta da armadura com CA total e propriedades.
        /// </summary>
        /// <returns>String com informações resumidas.</returns>
        public string DescricaoResumo()
        {
            var props = PropriedadesEspeciais != null && PropriedadesEspeciais.Count > 0
                ? string.Join(", ", PropriedadesEspeciais)
                : "Sem propriedades especiais";

            return $"{Nome} — CA: {CalcularClasseArmaduraTotal()} (Base: {ClasseArmadura}, Bônus Mágico: {BonusMagico}) - {props}";
        }

        /// <summary>
        /// Enumeração que define os tipos de armadura.
        /// </summary>
        public enum TipoArmadura
        {
            /// <summary>Armadura leve (ex: gibão de couro).</summary>
            Leve,

            /// <summary>Armadura média (ex: cota de escamas).</summary>
            Media,

            /// <summary>Armadura pesada (ex: armadura completa).</summary>
            Pesada,

            /// <summary>Escudo (complemento defensivo).</summary>
            Escudo
        }
    }
}
