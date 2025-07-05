using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma arma no sistema D&D 5e, incluindo atributos físicos, mágicos, requisitos e comportamentos.
    /// </summary>
    public class Arma : EntidadeBase
    {

        /// <summary>Tipo da arma (corpo-a-corpo ou à distância).</summary>
        public TipoArma Tipo { get; set; }

        /// <summary>Categoria da arma (simples ou marcial).</summary>
        public CategoriaArma Categoria { get; set; }

        /// <summary>Dado de dano principal da arma (ex: "1d8").</summary>
        public string DadoDano { get; set; }

        /// <summary>Tipo de dano causado (ex: cortante, perfurante).</summary>
        public TipoDano TipoDano { get; set; }

        /// <summary>Tipo de dano secundário (ex: fogo), se aplicável.</summary>
        public TipoDano? TipoDanoSecundario { get; set; }

        /// <summary>Peso da arma em quilos.</summary>
        public double Peso { get; set; }

        /// <summary>Valor da arma em peças de ouro.</summary>
        public decimal Custo { get; set; }

        /// <summary>Alcance normal da arma, se for à distância.</summary>
        public int? Alcance { get; set; }

        /// <summary>Indica se a arma requer duas mãos para uso.</summary>
        public bool EhDuasMaos { get; set; }

        /// <summary>Indica se a arma é leve (pode ser usada com duas armas).</summary>
        public bool EhLeve { get; set; }

        /// <summary>Indica se a arma possui versão versátil.</summary>
        public bool EhVersatil { get; set; }

        /// <summary>Dado de dano ao usar com duas mãos (ex: "1d10").</summary>
        public string DadoDanoVersatil { get; set; }

        /// <summary>Indica se a arma pode ser arremessada.</summary>
        public bool PodeSerArremessada { get; set; }

        /// <summary>Alcance da arma quando arremessada.</summary>
        public int? AlcanceArremesso { get; set; }

        /// <summary>Requisitos específicos para uso (ex: proficiência ou raça).</summary>
        public List<string> Requisitos { get; set; }

        /// <summary>Lista de propriedades especiais que a arma possui.</summary>
        public List<string> PropriedadesEspeciais { get; set; }

        /// <summary>Bônus mágico aplicado em jogadas de ataque e dano.</summary>
        public int BonusMagico { get; set; }

        /// <summary>Tipos de criatura contra os quais a arma possui bônus adicional.</summary>
        public List<string> BonusContraTipos { get; set; }

        /// <summary>Magias vinculadas à arma (ex: através de encantamento).</summary>
        public List<string> MagiasAssociadas { get; set; }

        /// <summary>Valor atual de durabilidade da arma.</summary>
        public int DurabilidadeAtual { get; set; }

        /// <summary>Valor máximo de durabilidade da arma.</summary>
        public int DurabilidadeMaxima { get; set; }

        /// <summary>Área de efeito da arma, se aplicável (ex: cone, linha).</summary>
        public string AreaAtaque { get; set; }

        /// <summary>Tipo de ação necessária para usar a arma (ex: ação, bônus).</summary>
        public string TipoAcao { get; set; }

        /// <summary>Regra de crítico personalizada, se houver.</summary>
        public string RegraCritico { get; set; }

        /// <summary>Tipo de munição usada pela arma, se aplicável.</summary>
        public string TipoMunicao { get; set; }

        /// <summary>Quantidade de munição gasta por ataque.</summary>
        public int MunicaoPorAtaque { get; set; } = 1;

        /// <summary>Indica se a arma exige recarga entre os disparos.</summary>
        public bool RequerRecarga { get; set; }

        /// <summary>Quantidade de turnos necessários para recarregar.</summary>
        public int TempoRecargaTurnos { get; set; }

        /// <summary>Lista de ataques especiais que a arma permite (ex: empurrar, cortar corda).</summary>
        public List<string> AtaquesEspeciais { get; set; }

        /// <summary>Custo de reparo da arma (em peças de ouro).</summary>
        public decimal CustoReparo { get; set; }

        /// <summary>Indica se a arma é considerada mágica.</summary>
        public bool EMagica { get; set; }

        /// <summary>Classificação de raridade da arma.</summary>
        public string Raridade { get; set; }

        /// <summary>Nome do fabricante ou origem da arma, se relevante.</summary>
        public string Fabricante { get; set; }

        /// <summary>Lista de requisitos de atributos para manuseio ideal.</summary>
        public List<ArmaRequisitoAtributo> RequisitosAtributos { get; set; } = new();

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Arma_Tag.
        /// </summary>
        public List<ArmaTag> ArmaTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de ArmaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => ArmaTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaTags = value?.Select(tag => new ArmaTag { Tag = tag, ArmaId = this.Id }).ToList() ?? new();
        }


        /// <summary>
        /// Retorna uma string com o dano da arma incluindo bônus mágico.
        /// </summary>
        [JsonIgnore]
        public string DanoTotal => BonusMagico != 0 ? $"{DadoDano} + {BonusMagico}" : DadoDano;


        /// <summary>
        /// Calcula o dano total da arma considerando bônus mágico e se o ataque é versátil.
        /// </summary>
        /// <param name="usarVersatil">Indica se o ataque usa o dado versátil.</param>
        /// <returns>String com o dano formatado, ex: "1d8 + 2" ou "1d10 + 2"</returns>
        public string CalcularDanoTotal(bool usarVersatil = false)
        {
            string dadoDanoAtual = usarVersatil && EhVersatil && !string.IsNullOrEmpty(DadoDanoVersatil)
                ? DadoDanoVersatil
                : DadoDano;

            if (BonusMagico != 0)
                return $"{dadoDanoAtual} + {BonusMagico}";

            return dadoDanoAtual;
        }

        /// <summary>
        /// Aplica dano na durabilidade da arma, retornando se a arma quebrou (durabilidade chegou a zero).
        /// </summary>
        /// <param name="dano">Quantidade de dano à durabilidade.</param>
        /// <returns>True se a arma quebrou, false caso contrário.</returns>
        public bool AplicarDanoDurabilidade(int dano)
        {
            if (DurabilidadeAtual <= 0)
                return true; // Já quebrada

            DurabilidadeAtual -= dano;

            if (DurabilidadeAtual <= 0)
            {
                DurabilidadeAtual = 0;
                return true; // Quebrou agora
            }

            return false; // Ainda intacta
        }

        /// <summary>
        /// Repara a arma, restaurando durabilidade, até o máximo.
        /// </summary>
        /// <param name="quantidade">Quantidade de durabilidade a restaurar.</param>
        public void Reparar(int quantidade)
        {
            DurabilidadeAtual += quantidade;
            if (DurabilidadeAtual > DurabilidadeMaxima)
                DurabilidadeAtual = DurabilidadeMaxima;
        }

        /// <summary>
        /// Verifica se o personagem cumpre os requisitos mínimos de atributo para usar a arma sem penalidades.
        /// </summary>
        /// <param name="atributosPersonagem">Dicionário com atributos e seus valores.</param>
        /// <returns>True se cumpre todos os requisitos, false caso contrário.</returns>
        public bool VerificarRequisitosAtributos(Dictionary<Atributo, int> atributosPersonagem)
        {
            foreach (var requisito in RequisitosAtributos)
{
    if (!atributosPersonagem.TryGetValue(requisito.Atributo, out int valor) || valor < requisito.Valor)
        return false;
}
return true;

        }

        /// <summary>
        /// Verifica se a arma tem munição suficiente para um ataque.
        /// </summary>
        /// <param name="quantidadeMuniçãoDisponivel">Quantidade de munição disponível no personagem.</param>
        /// <returns>True se há munição suficiente, false caso contrário.</returns>
        public bool TemMuniçãoSuficiente(int quantidadeMuniçãoDisponivel)
        {
            if (string.IsNullOrEmpty(TipoMunicao))
                return true; // Arma não usa munição

            return quantidadeMuniçãoDisponivel >= MunicaoPorAtaque;
        }

        /// <summary>
        /// Calcula o alcance efetivo da arma dependendo do tipo de ataque (normal ou arremesso).
        /// </summary>
        /// <param name="arremesso">Indica se o ataque é arremesso.</param>
        /// <returns>Alcance efetivo em metros, ou null se não aplicável.</returns>
        public int? CalcularAlcance(bool arremesso = false)
        {
            if (arremesso && PodeSerArremessada)
                return AlcanceArremesso;

            if (!arremesso)
                return Alcance;

            return null;
        }

        /// <summary>
        /// Aplica o tempo de recarga após um ataque que requer recarga.
        /// </summary>
        /// <param name="turnosDisponiveis">Quantidade de turnos restantes para recarregar.</param>
        /// <returns>Turnos restantes para completar recarga.</returns>
        public int AplicarRecarga(int turnosDisponiveis)
        {
            if (!RequerRecarga)
                return 0;

            if (turnosDisponiveis >= TempoRecargaTurnos)
                return 0; // Recarga completa

            return TempoRecargaTurnos - turnosDisponiveis;
        }

        /// <summary>
        /// Adiciona uma magia associada à arma.
        /// </summary>
        /// <param name="magia">Nome da magia para associar.</param>
        public void AdicionarMagiaAssociada(string magia)
        {
            if (MagiasAssociadas == null)
                MagiasAssociadas = new List<string>();

            if (!MagiasAssociadas.Contains(magia))
                MagiasAssociadas.Add(magia);
        }

        /// <summary>
        /// Remove uma magia associada da arma.
        /// </summary>
        /// <param name="magia">Nome da magia para remover.</param>
        public void RemoverMagiaAssociada(string magia)
        {
            MagiasAssociadas?.Remove(magia);
        }

        /// <summary>
        /// Lista as magias associadas formatadas em uma string.
        /// </summary>
        /// <returns>String separada por vírgulas das magias associadas, ou "Nenhuma".</returns>
        public string ListarMagiasAssociadas()
        {
            if (MagiasAssociadas == null || MagiasAssociadas.Count == 0)
                return "Nenhuma";

            return string.Join(", ", MagiasAssociadas);
        }

        /// <summary>
        /// Verifica se a arma possui uma propriedade especial específica.
        /// </summary>
        /// <param name="propriedade">Nome da propriedade para verificar.</param>
        /// <returns>True se possuir, false caso contrário.</returns>
        public bool PossuiPropriedadeEspecial(string propriedade)
        {
            return PropriedadesEspeciais?.Contains(propriedade) ?? false;
        }

        /// <summary>
        /// Calcula o dano máximo possível da arma (ex: para rolagem máxima do dado + bônus mágico).
        /// </summary>
        /// <param name="usarVersatil">Se deve usar o dano versátil.</param>
        /// <returns>Dano máximo como inteiro, ou null se não for possível calcular.</returns>
        public int? CalcularDanoMaximo(bool usarVersatil = false)
        {
            string dado = usarVersatil && EhVersatil && !string.IsNullOrEmpty(DadoDanoVersatil) ? DadoDanoVersatil : DadoDano;
            int maxDado = ObterMaximoDado(dado);
            if (maxDado < 0) return null;

            return maxDado + BonusMagico;
        }

        /// <summary>
        /// Método auxiliar para obter o valor máximo de um dado representado como string (ex: "1d8").
        /// </summary>
        private int ObterMaximoDado(string dado)
        {
            // Espera formato NdX, como 1d8, 2d6, etc.
            if (string.IsNullOrEmpty(dado)) return -1;

            var partes = dado.ToLower().Split('d');
            if (partes.Length != 2) return -1;

            if (!int.TryParse(partes[0], out int numDados)) return -1;
            if (!int.TryParse(partes[1], out int faces)) return -1;

            return numDados * faces;
        }

        /// <summary>
        /// Verifica se a arma causa dano mágico (considera bonus mágico ou atributo EMagica).
        /// </summary>
        /// <returns>True se a arma é mágica ou tem bônus mágico, false caso contrário.</returns>
        public bool CausaDanoMagico()
        {
            return EMagica || BonusMagico > 0;
        }

        /// <summary>
        /// Incrementa o bônus mágico da arma, por exemplo ao aplicar encantamentos.
        /// </summary>
        /// <param name="incremento">Valor a ser somado ao bônus mágico.</param>
        public void IncrementarBonusMagico(int incremento)
        {
            BonusMagico += incremento;
        }

        // ───────────── ENUMERAÇÕES ─────────────

        /// <summary>
        /// Tipos de arma disponíveis.
        /// </summary>
        public enum TipoArma
        {
            /// <summary>Arma de combate corpo a corpo.</summary>
            CorpoACorpo,

            /// <summary>Arma de combate à distância.</summary>
            ADistancia
        }

        /// <summary>
        /// Categoria da arma (ligada à proficiência).
        /// </summary>
        public enum CategoriaArma
        {
            /// <summary>Armas básicas, acessíveis a quase todas as classes.</summary>
            Simples,

            /// <summary>Armas complexas, exigem proficiência marcial.</summary>
            Marcial
        }

    }
}
