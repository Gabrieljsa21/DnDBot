using Discord;
using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Distribuicao;
using DnDBot.Bot.Helpers;
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

        [ComponentInteraction("select_subraca")]
        public async Task SelectSubracaHandler(string subracaId)
        {
            var ficha = await _fichaService.ObterUltimaFichaDoJogadorAsync(Context.User.Id);
            if (ficha == null)
            {
                await RespondAsync("❌ Não encontrei a ficha para atualizar a sub-raça.", ephemeral: true);
                return;
            }

            var subraca = (await _racasService.ObterTodasSubracasAsync())
                            .FirstOrDefault(sr => sr.Id == subracaId);

            if (subraca == null)
            {
                await RespondAsync("❌ Sub-raça selecionada não encontrada.", ephemeral: true);
                return;
            }

            await _idiomaService.ObterFichaIdiomasAsync(ficha); // ✅ Correção importante

            AtualizarFichaComSubraca(ficha, subraca);

            await _fichaService.AtualizarFichaAsync(ficha);

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            _atributosHandler.InicializarDistribuicao(dist, ficha);

            await RespondAsync(
                $"✅ Sub-raça **{subraca.Nome}** aplicada! Agora distribua os pontos nos atributos:",
                embed: _atributosHandler.ConstruirEmbedDistribuicao(dist),
                components: _atributosHandler.ConstruirComponentesDistribuicao(dist, ficha.Id),
                ephemeral: true);
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
