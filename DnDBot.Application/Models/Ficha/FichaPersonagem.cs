using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa a ficha de personagem de um jogador, contendo dados básicos,
    /// atributos, proficiências, magias, visão, tesouro e histórico financeiro.
    /// </summary>
    public class FichaPersonagem
    {
        /// <summary>
        /// Identificador único da ficha.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID do jogador no Discord.
        /// </summary>
        public ulong IdJogador { get; set; }

        /// <summary>
        /// Nome do personagem.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Identificador da raça selecionada.
        /// </summary>
        public string IdRaca { get; set; } = "Não definida";

        /// <summary>
        /// Identificador da sub-raça selecionada.
        /// </summary>
        public string IdSubraca { get; set; } = "Não definida";

        /// <summary>
        /// Identificador da classe selecionada.
        /// </summary>
        public string IdClasse { get; set; } = "Não definida";

        /// <summary>
        /// Identificador do antecedente selecionado.
        /// </summary>
        public string IdAntecedente { get; set; } = "Não definido";

        /// <summary>
        /// Identificador do alinhamento selecionado.
        /// </summary>
        public string IdAlinhamento { get; set; } = "Não definido";

        // Atributos base do personagem
        public int Forca { get; set; }
        public int Destreza { get; set; }
        public int Constituicao { get; set; }
        public int Inteligencia { get; set; }
        public int Sabedoria { get; set; }
        public int Carisma { get; set; }

        /// <summary>
        /// Lista de bônus de atributos adicionais (ex: por raça, itens).
        /// </summary>
        public List<BonusAtributo> BonusAtributos { get; set; } = new();

        /// <summary>
        /// Lista de IDs de proficiências do personagem.
        /// </summary>
        public List<string> IdProficiencias { get; set; } = new();

        /// <summary>
        /// Lista de IDs de idiomas conhecidos pelo personagem.
        /// </summary>
        public List<string> IdIdiomas { get; set; } = new();

        /// <summary>
        /// Lista de IDs de resistências que o personagem possui.
        /// </summary>
        public List<string> IdResistencias { get; set; } = new();

        /// <summary>
        /// Lista de IDs das características especiais do personagem.
        /// </summary>
        public List<string> IdCaracteristicas { get; set; } = new();

        /// <summary>
        /// Lista de IDs de magias raciais disponíveis ao personagem.
        /// </summary>
        public List<string> IdMagiasRaciais { get; set; } = new();

        /// <summary>
        /// Tamanho do personagem (ex: Pequeno, Médio).
        /// </summary>
        public string Tamanho { get; set; }

        /// <summary>
        /// Valor do deslocamento do personagem em metros.
        /// </summary>
        public int Deslocamento { get; set; }

        /// <summary>
        /// Alcance da visão no escuro, em metros. Zero indica ausência dessa visão.
        /// </summary>
        public int? VisaoNoEscuro { get; set; }

        /// <summary>
        /// Data e hora da criação da ficha, ajustada para o fuso horário de Brasília (UTC-3).
        /// </summary>
        public DateTime DataCriacao { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");

        /// <summary>
        /// Data e hora da última alteração na ficha.
        /// </summary>
        public DateTime DataAlteracao { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");

        /// <summary>
        /// Indica se a ficha está ativa.
        /// </summary>
        public bool EstaAtivo { get; set; } = true;

        /// <summary>
        /// Tesouro atual do personagem, contendo moedas e outros valores.
        /// </summary>
        public Tesouro Tesouro { get; set; } = new();

        /// <summary>
        /// Histórico financeiro do personagem, para registro de transações ou eventos econômicos.
        /// </summary>
        public List<string> HistoricoFinanceiro { get; set; } = new();

        /// <summary>
        /// Calcula a soma total dos bônus para o atributo informado.
        /// </summary>
        /// <param name="atributo">Nome do atributo (ex: "Forca")</param>
        /// <returns>Soma dos valores de bônus para o atributo.</returns>
        public int ObterBonusTotal(string atributo)
        {
            return BonusAtributos
                .Where(b => b.Atributo.Equals(atributo, StringComparison.OrdinalIgnoreCase))
                .Sum(b => b.Valor);
        }

        /// <summary>
        /// Obtém o valor total do atributo incluindo o valor base e os bônus.
        /// </summary>
        /// <param name="atributo">Nome do atributo.</param>
        /// <returns>Valor total do atributo com bônus aplicados.</returns>
        public int ObterTotalComBonus(string atributo)
        {
            int baseValor = atributo switch
            {
                "Forca" => Forca,
                "Destreza" => Destreza,
                "Constituicao" => Constituicao,
                "Inteligencia" => Inteligencia,
                "Sabedoria" => Sabedoria,
                "Carisma" => Carisma,
                _ => 0
            };

            return baseValor + ObterBonusTotal(atributo);
        }

        /// <summary>
        /// Calcula o modificador do atributo baseado no valor total (base + bônus), conforme regras do D&D 5e.
        /// </summary>
        /// <param name="atributo">Nome do atributo.</param>
        /// <returns>Valor do modificador.</returns>
        public int ObterModificador(string atributo)
        {
            int total = ObterTotalComBonus(atributo);
            return (total - 10) / 2;
        }
    }

    /// <summary>
    /// Representa um bônus aplicado a um atributo, indicando o valor e a origem desse bônus.
    /// </summary>
    public class BonusAtributo
    {
        /// <summary>
        /// Identificador único do bônus.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do atributo ao qual o bônus se aplica (ex: "Forca").
        /// </summary>
        public string Atributo { get; set; } = string.Empty;

        /// <summary>
        /// Valor do bônus (ex: +2).
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// Origem do bônus (ex: "Raça", "Item", "Sub-Raça").
        /// </summary>
        public string Origem { get; set; } = string.Empty;
    }
}
