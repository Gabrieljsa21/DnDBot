using Discord;
using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Distribuicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável por lidar com a seleção de sub-raças durante a criação da ficha.
    /// </summary>
    public class ComandoSubraca : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;
        private readonly IdiomaService _idiomaService;

        public ComandoSubraca(FichaService fichaService, RacasService racasService, DistribuicaoAtributosHandler atributosHandler, IdiomaService idiomaService)
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _atributosHandler = atributosHandler;
            _idiomaService = idiomaService;
        }



        // Método helper que aplica dados da sub-raça na ficha
        private void AtualizarFichaComSubraca(FichaPersonagem ficha, SubRaca subraca)
        {
            ficha.SubracaId = subraca.Id;
            ficha.BonusAtributos = subraca.BonusAtributos.Select(b => new BonusAtributo
            {
                Atributo = b.Atributo,
                Valor = b.Valor,
                Origem = "Sub-Raça"
            }).ToList();

            ficha.Tamanho = subraca.Tamanho.ToString();
            ficha.Deslocamento = subraca.Deslocamento;

            // Atualiza idiomas (adiciona os fixos da sub-raça)
            var idiomasNovos = subraca.Idiomas.Where(i => !string.IsNullOrWhiteSpace(i.Id))
                                .GroupBy(i => i.Id)
                                .Select(g =>
                                {
                                    var idioma = g.First();
                                    if (idioma.Categoria == 0)
                                        idioma.Categoria = CategoriaIdioma.Standard;
                                    return idioma;
                                });

            foreach (var idioma in idiomasNovos)
            {
                if (!ficha.Idiomas.Any(i => i.Id == idioma.Id))
                    ficha.Idiomas.Add(idioma);
            }

            ficha.Proficiencias = subraca.Proficiencias;
            ficha.VisaoNoEscuro = subraca.VisaoNoEscuro;
            ficha.Resistencias = subraca.Resistencias.ToList();
            ficha.Caracteristicas = subraca.Caracteristicas.ToList();
            ficha.MagiasRaciais = subraca.MagiasRaciais.ToList();
        }


    }
}
