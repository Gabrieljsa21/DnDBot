using System.Collections.Generic;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Enums;

namespace DnDBot.Bot.Data
{
    /// <summary>
    /// Lista estática das resistências de dano do Dungeons & Dragons 5ª Edição.
    /// Cada resistência possui um Id, Nome, Tipo de Dano e Descrição.
    /// </summary>
    public static class ResistenciasData
    {
        public static readonly List<Resistencia> Resistencias = new()
        {
            new Resistencia
            {
                Id = "cortante",
                Nome = "Resistência a Dano Cortante",
                TipoDano = TipoDano.Cortante,
                Descricao = "Reduz o dano causado por armas cortantes como espadas, machados e lâminas."
            },
            new Resistencia
            {
                Id = "perfurante",
                Nome = "Resistência a Dano Perfurante",
                TipoDano = TipoDano.Perfurante,
                Descricao = "Reduz o dano causado por armas perfurantes como lanças, flechas e estacas."
            },
            new Resistencia
            {
                Id = "contundente",
                Nome = "Resistência a Dano Contundente",
                TipoDano = TipoDano.Contundente,
                Descricao = "Reduz o dano causado por impacto contundente como martelos, porretes e golpes brutos."
            },
            new Resistencia
            {
                Id = "fogo",
                Nome = "Resistência a Dano de Fogo",
                TipoDano = TipoDano.Fogo,
                Descricao = "Reduz o dano causado por fogo e calor intenso."
            },
            new Resistencia
            {
                Id = "gelo",
                Nome = "Resistência a Dano de Gelo",
                TipoDano = TipoDano.Gelo,
                Descricao = "Reduz o dano causado por frio intenso e gelo."
            },
            new Resistencia
            {
                Id = "eletrico",
                Nome = "Resistência a Dano Elétrico",
                TipoDano = TipoDano.Elétrico,
                Descricao = "Reduz o dano causado por eletricidade e choque elétrico."
            },
            new Resistencia
            {
                Id = "acido",
                Nome = "Resistência a Dano Ácido",
                TipoDano = TipoDano.Ácido,
                Descricao = "Reduz o dano causado por ácido e substâncias corrosivas."
            },
            new Resistencia
            {
                Id = "veneno",
                Nome = "Resistência a Dano de Veneno",
                TipoDano = TipoDano.Veneno,
                Descricao = "Reduz o dano causado por venenos e toxinas."
            },
            new Resistencia
            {
                Id = "forca",
                Nome = "Resistência a Dano de Força",
                TipoDano = TipoDano.Força,
                Descricao = "Reduz o dano causado por energia mágica ou física não convencional."
            },
            new Resistencia
            {
                Id = "radiante",
                Nome = "Resistência a Dano Radiante",
                TipoDano = TipoDano.Radiante,
                Descricao = "Reduz o dano causado por energia radiante, geralmente divina."
            },
            new Resistencia
            {
                Id = "necrotico",
                Nome = "Resistência a Dano Necrótico",
                TipoDano = TipoDano.Necrótico,
                Descricao = "Reduz o dano causado por energia necromântica ou força negativa."
            },
            new Resistencia
            {
                Id = "psiquico",
                Nome = "Resistência a Dano Psíquico",
                TipoDano = TipoDano.Psíquico,
                Descricao = "Reduz o dano causado por ataques mentais, medo ou confusão."
            }
        };
    }
}
