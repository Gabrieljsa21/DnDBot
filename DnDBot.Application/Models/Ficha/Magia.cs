using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma magia do universo D&D, contendo informações sobre sua conjuração,
    /// efeitos, componentes, restrições, e outras propriedades relevantes.
    /// </summary>
    public class Magia
    {
        // Identificação básica
        /// <summary>
        /// Identificador único da magia (ex: "bola-de-fogo").
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da magia.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Nível da magia (ex: "Truque", "1º nível").
        /// </summary>
        public string Nivel { get; set; }

        /// <summary>
        /// Escola de magia (ex: Evocação, Necromancia).
        /// </summary>
        public string Escola { get; set; }

        // Conjuração
        /// <summary>
        /// Tempo necessário para conjurar a magia.
        /// </summary>
        public string TempoConjuracao { get; set; }

        /// <summary>
        /// Alcance da magia.
        /// </summary>
        public string Alcance { get; set; }

        /// <summary>
        /// Alvo(s) da magia.
        /// </summary>
        public string Alvo { get; set; }

        /// <summary>
        /// Indica se a magia requer concentração para manter seus efeitos.
        /// </summary>
        public bool Concentração { get; set; }

        /// <summary>
        /// Duração total da magia.
        /// </summary>
        public string Duracao { get; set; }

        /// <summary>
        /// Indica se a magia pode ser conjurada como ritual.
        /// </summary>
        public bool PodeSerRitual { get; set; }

        // Componentes
        /// <summary>
        /// Indica se a magia possui componente verbal.
        /// </summary>
        public bool ComponenteVerbal { get; set; }

        /// <summary>
        /// Indica se a magia possui componente somático.
        /// </summary>
        public bool ComponenteSomatico { get; set; }

        /// <summary>
        /// Indica se a magia possui componente material.
        /// </summary>
        public bool ComponenteMaterial { get; set; }

        /// <summary>
        /// Detalhes do componente material (ex: "uma pena de corvo").
        /// </summary>
        public string DetalhesMaterial { get; set; }

        /// <summary>
        /// Indica se o componente material é consumido ao conjurar.
        /// </summary>
        public bool ComponenteMaterialConsumido { get; set; }

        /// <summary>
        /// Custo do componente material (ex: "50 po").
        /// </summary>
        public string CustoComponenteMaterial { get; set; }

        // Efeitos e dano
        /// <summary>
        /// Tipo do dano causado (ex: Fogo, Gelo).
        /// </summary>
        public string TipoDano { get; set; }

        /// <summary>
        /// Dados de dano da magia (ex: "8d6").
        /// </summary>
        public string DadoDano { get; set; }

        /// <summary>
        /// Descrição do escalonamento de dano (ex: "dano aumenta 1d6 por nível").
        /// </summary>
        public string Escalonamento { get; set; }

        // Teste de resistência
        /// <summary>
        /// Atributo usado no teste de resistência contra a magia (ex: "Constituição").
        /// </summary>
        public string AtributoTesteResistencia { get; set; }

        /// <summary>
        /// Indica se o alvo recebe metade do dano ao passar no teste de resistência.
        /// </summary>
        public bool MetadeNoTeste { get; set; }

        // Condições aplicadas ou removidas
        /// <summary>
        /// Lista de condições que a magia aplica (ex: "Envenenado", "Paralisado").
        /// </summary>
        public List<string> CondicoesAplicadas { get; set; }

        /// <summary>
        /// Lista de condições que a magia pode remover.
        /// </summary>
        public List<string> CondicoesRemovidas { get; set; }

        // Classes e fontes
        /// <summary>
        /// Lista de classes que podem usar essa magia.
        /// </summary>
        public List<string> ClassesPermitidas { get; set; }

        /// <summary>
        /// Lista das fontes (livros, suplementos) onde a magia aparece.
        /// </summary>
        public List<string> Fontes { get; set; }

        // Detalhes adicionais
        /// <summary>
        /// Descrição detalhada da magia.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Informações de recarga ou recarregamento da magia.
        /// </summary>
        public string Recarga { get; set; }

        /// <summary>
        /// Tags para categorização ou filtro.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Tipo de uso da magia (ex: "Ação", "Bônus").
        /// </summary>
        public string TipoUso { get; set; }

        /// <summary>
        /// Indica se a magia requer linha de visão para ser conjurada.
        /// </summary>
        public bool RequerLinhaDeVisao { get; set; }

        /// <summary>
        /// Indica se a magia requer linha reta para atingir o alvo.
        /// </summary>
        public bool RequerLinhaReta { get; set; }

        // Técnicos
        /// <summary>
        /// Número máximo de alvos que a magia pode atingir.
        /// </summary>
        public int? NumeroMaximoAlvos { get; set; }

        /// <summary>
        /// Área de efeito da magia (ex: "círculo de 20 pés").
        /// </summary>
        public string AreaEfeito { get; set; }

        /// <summary>
        /// Foco necessário para conjurar a magia (ex: "um cajado").
        /// </summary>
        public string FocoNecessario { get; set; }

        /// <summary>
        /// Limitações no uso da magia.
        /// </summary>
        public string LimiteUso { get; set; }

        /// <summary>
        /// Efeito que ocorre a cada turno enquanto a magia estiver ativa.
        /// </summary>
        public string EfeitoPorTurno { get; set; }

        // Fonte oficial
        /// <summary>
        /// Nome do livro ou suplemento onde a magia está publicada.
        /// </summary>
        public string FonteLivro { get; set; }

        /// <summary>
        /// Número da página no livro da fonte oficial.
        /// </summary>
        public int? PaginaLivro { get; set; }

        // Notas internas
        /// <summary>
        /// Notas internas para uso em desenvolvimento, marcações ou debug.
        /// </summary>
        public string NotasInternas { get; set; }

        // Métrica de uso
        /// <summary>
        /// Contador opcional de quantas vezes a magia foi usada.
        /// </summary>
        public int NumeroDeUsos { get; set; }
    }
}
