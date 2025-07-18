﻿using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa a ficha de personagem de um jogador, contendo dados básicos,
    /// atributos, proficiências, magias, visão, tesouro e histórico financeiro.
    /// </summary>
    public class FichaPersonagem
    {
        /// <summary>
        /// Identificador único da ficha (GUID).
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Identificador do jogador dono da ficha (ex: Discord User ID).
        /// </summary>
        public ulong JogadorId { get; set; }

        /// <summary>
        /// Nome do personagem.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Nível atual do personagem.
        /// </summary>
        public int Level { get; set; } = 1;

        /// <summary>
        /// Quantidade total de pontos de experiência acumulados.
        /// </summary>
        public int Experiencia { get; set; } = 0;

        /// <summary>
        /// ID da raça do personagem.
        /// </summary>
        public string RacaId { get; set; } = "Não definida";

        /// <summary>
        /// ID da sub-raça do personagem.
        /// </summary>
        public string SubracaId { get; set; } = "Não definida";

        /// <summary>
        /// ID da classe do personagem.
        /// </summary>
        public string ClasseId { get; set; } = "Não definida";

        /// <summary>
        /// ID do antecedente (background) do personagem.
        /// </summary>
        public string AntecedenteId { get; set; } = "Não definido";

        /// <summary>
        /// ID do alinhamento do personagem.
        /// </summary>
        public string AlinhamentoId { get; set; } = "Não definido";

        /// <summary>
        /// Valor base do atributo Força.
        /// </summary>
        public int Forca { get; set; }

        /// <summary>
        /// Valor base do atributo Destreza.
        /// </summary>
        public int Destreza { get; set; }

        /// <summary>
        /// Valor base do atributo Constituição.
        /// </summary>
        public int Constituicao { get; set; }

        /// <summary>
        /// Valor base do atributo Inteligência.
        /// </summary>
        public int Inteligencia { get; set; }

        /// <summary>
        /// Valor base do atributo Sabedoria.
        /// </summary>
        public int Sabedoria { get; set; }

        /// <summary>
        /// Valor base do atributo Carisma.
        /// </summary>
        public int Carisma { get; set; }

        /// <summary>
        /// Lista de bônus aplicados aos atributos do personagem, com origem e valor.
        /// </summary>
        public List<BonusAtributo> BonusAtributos { get; set; } = new();

        /// <summary>
        /// Lista de proficiências do personagem.
        /// </summary>
        public List<FichaPersonagemProficiencia> Proficiencias { get; set; } = new();

        /// <summary>
        /// Lista de idiomas conhecidos pelo personagem.
        /// </summary>
        public List<FichaPersonagemIdioma> Idiomas { get; set; } = new();

        /// <summary>
        /// Lista de resistências do personagem.
        /// </summary>
        public List<FichaPersonagemResistencia> Resistencias { get; set; } = new();

        /// <summary>
        /// Lista de características especiais do personagem.
        /// </summary>
        public List<FichaPersonagemCaracteristica> Caracteristicas { get; set; } = new();

        /// <summary>
        /// Lista de magias raciais que o personagem possui.
        /// </summary>
        public List<FichaPersonagemMagia> MagiasRaciais { get; set; } = new();

        /// <summary>
        /// Tamanho do personagem (ex: Pequeno, Médio).
        /// </summary>
        public string Tamanho { get; set; }

        /// <summary>
        /// Deslocamento em pés do personagem.
        /// </summary>
        public int Deslocamento { get; set; }

        /// <summary>
        /// Valor do alcance de visão no escuro em pés, se aplicável.
        /// </summary>
        public int? VisaoNoEscuro { get; set; }

        /// <summary>
        /// Nome ou ID do usuário que criou o registro da entidade.
        /// </summary>
        public string CriadoPor { get; set; } = string.Empty;

        /// <summary>
        /// Nome ou ID do usuário que realizou a última modificação no registro.
        /// </summary>
        public string ModificadoPor { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação da ficha, ajustada para fuso horário "E. South America Standard Time".
        /// </summary>
        public DateTime CriadoEm { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");

        /// <summary>
        /// Data da última alteração da ficha, ajustada para fuso horário "E. South America Standard Time".
        /// </summary>
        public DateTime ModificadoEm { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");


        /// <summary>
        /// URL de uma imagem ilustrativa relacionada à entidade.
        /// Pode ser usada em embeds, cards visuais ou sistemas de pré-visualização.
        /// </summary>
        public string ImagemUrl { get; set; } = string.Empty;

        /// <summary>
        /// URL de um ícone representativo da entidade.
        /// Pode ser usado em listas ou menus compactos.
        /// </summary>
        public string IconeUrl { get; set; } = string.Empty;


        /// <summary>
        /// Indica se a ficha está ativa.
        /// </summary>
        public bool EstaAtivo { get; set; } = true;

        public Inventario Inventario { get; set; } = new Inventario();


        /// <summary>
        /// Bolsa de moedas do personagem, contendo quantidades de cada tipo de moeda.
        /// </summary>
        public BolsaDeMoedas BolsaDeMoedas { get; set; } = new();

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Raca_Tag.
        /// </summary>
        public List<FichaPersonagemTag> FichaPersonagemTags { get; set; } = new();

        [NotMapped]
        public EtapaCriacaoFicha EtapaAtual { get; set; } = EtapaCriacaoFicha.Inicio;

        /// <summary>
        /// Tags derivadas da lista de RacaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => FichaPersonagemTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => FichaPersonagemTags = value?.Select(tag => new FichaPersonagemTag { Tag = tag, FichaPersonagemId = Id }).ToList() ?? new();
        }

        /// <summary>
        /// Obtém o valor total dos bônus aplicados a um determinado atributo (por nome).
        /// </summary>
        /// <param name="atributo">Nome do atributo (ex: "Forca").</param>
        /// <returns>Soma dos valores dos bônus.</returns>
        public int ObterBonusTotal(string atributo)
        {
            if (!Enum.TryParse<Atributo>(atributo, true, out var atributoEnum))
                return 0;

            return BonusAtributos
                .Where(b => b.Atributo == atributoEnum)
                .Sum(b => b.Valor);
        }


        /// <summary>
        /// Obtém o valor total do atributo, somando o valor base com os bônus.
        /// </summary>
        /// <param name="atributo">Nome do atributo.</param>
        /// <returns>Valor total do atributo.</returns>
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
        /// Calcula o modificador do atributo conforme as regras de D&D ((valor - 10) / 2).
        /// </summary>
        /// <param name="atributo">Nome do atributo.</param>
        /// <returns>Modificador do atributo.</returns>
        public int ObterModificador(string atributo)
        {
            int total = ObterTotalComBonus(atributo);
            return (int)Math.Floor((total - 10) / 2.0);
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
        /// Nome do atributo que recebe o bônus (ex: "Forca").
        /// </summary>
        public Atributo Atributo { get; set; }

        /// <summary>
        /// Valor do bônus aplicado.
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// Origem do bônus (ex: item, efeito, magia).
        /// </summary>
        public string Origem { get; set; } = string.Empty;

        /// <summary>
        /// Tipo do dono do bônus (ex: "FichaPersonagem", "Raca", "Equipamento").
        /// </summary>
        public string OwnerType { get; set; } = string.Empty;

        /// <summary>
        /// Identificador do dono do bônus.
        /// </summary>
        public Guid OwnerId { get; set; }
    }
}
