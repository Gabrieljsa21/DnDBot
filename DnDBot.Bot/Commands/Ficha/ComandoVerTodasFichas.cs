﻿using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável pelo comando /ficha_ver_todas que exibe fichas salvas do usuário.
    /// </summary>
    public class ComandoVerTodasFichas : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly ClassesService _classesService;
        private readonly AntecedentesService _antecedentesService;
        private readonly AlinhamentosService _alinhamentosService;

        /// <summary>
        /// Construtor com injeção do serviço de fichas.
        /// </summary>
        public ComandoVerTodasFichas(
            FichaService fichaService,
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService
        )
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
        }

        /// <summary>
        /// Comando que exibe todas as fichas criadas pelo usuário atual.
        /// </summary>
        [SlashCommand("ficha_ver_todas", "Mostra suas fichas de personagem")]
        public async Task VerFichasAsync()
        {
            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);

            if (fichas == null || fichas.Count == 0)
            {
                await RespondAsync("❌ Você não tem nenhuma ficha criada.", ephemeral: true);
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle($"📘 Fichas de {Context.User.Username}")
                .WithColor(Color.DarkPurple);

            foreach (var ficha in fichas)
            {
                var atributosTexto = new List<string>
        {
            $"Força: {_fichaService.FormatarAtributo(ficha, "Forca")}",
            $"Destreza: {_fichaService.FormatarAtributo(ficha, "Destreza")}",
            $"Constituição: {_fichaService.FormatarAtributo(ficha, "Constituicao")}",
            $"Inteligência: {_fichaService.FormatarAtributo(ficha, "Inteligencia")}",
            $"Sabedoria: {_fichaService.FormatarAtributo(ficha, "Sabedoria")}",
            $"Carisma: {_fichaService.FormatarAtributo(ficha, "Carisma")}"
        };

                string raca;
                if (string.IsNullOrWhiteSpace(ficha.RacaId) || ficha.RacaId.Equals("NãoDefinido", StringComparison.OrdinalIgnoreCase) || ficha.RacaId.Equals("Não definida", StringComparison.OrdinalIgnoreCase))
                    raca = ficha.RacaId;
                else
                    raca = (await _racasService.ObterRacaPorIdAsync(ficha.RacaId))?.Nome ?? ficha.RacaId;

                string subRaca;
                if (string.IsNullOrWhiteSpace(ficha.SubracaId) || ficha.SubracaId.Equals("NãoDefinido", StringComparison.OrdinalIgnoreCase) || ficha.SubracaId.Equals("Não definida", StringComparison.OrdinalIgnoreCase))
                    subRaca = ficha.SubracaId;
                else
                    subRaca = (await _racasService.ObterSubRacaPorIdAsync(ficha.SubracaId))?.Nome ?? ficha.SubracaId;

                string classe;
                if (string.IsNullOrWhiteSpace(ficha.ClasseId) || ficha.ClasseId.Equals("NãoDefinido", StringComparison.OrdinalIgnoreCase) || ficha.ClasseId.Equals("Não definida", StringComparison.OrdinalIgnoreCase))
                    classe = ficha.ClasseId;
                else
                    classe = (await _classesService.ObterClassePorIdAsync(ficha.ClasseId))?.Nome ?? ficha.ClasseId;

                string antecedente;
                if (string.IsNullOrWhiteSpace(ficha.AntecedenteId) || ficha.AntecedenteId.Equals("NãoDefinido", StringComparison.OrdinalIgnoreCase) || ficha.AntecedenteId.Equals("Não definida", StringComparison.OrdinalIgnoreCase))
                    antecedente = ficha.AntecedenteId;
                else
                    antecedente = (await _antecedentesService.ObterAntecedentePorIdAsync(ficha.AntecedenteId))?.Nome ?? ficha.AntecedenteId;

                string alinhamento;
                if (string.IsNullOrWhiteSpace(ficha.AlinhamentoId) || ficha.AlinhamentoId.Equals("NãoDefinido", StringComparison.OrdinalIgnoreCase) || ficha.AlinhamentoId.Equals("Não definida", StringComparison.OrdinalIgnoreCase))
                    alinhamento = ficha.AlinhamentoId;
                else
                    alinhamento = "";
                    //alinhamento = _alinhamentosService.ObterAlinhamentoPorId(ficha.AlinhamentoId)?.Nome ?? ficha.AlinhamentoId;

                embedBuilder.AddField(
                    ficha.Nome,
                    $"Raça: {raca}\n" +
                    $"Sub-Raça: {subRaca}\n" +
                    $"Classe: {classe}\n" +
                    $"Antecedente: {antecedente}\n" +
                    $"Alinhamento: {alinhamento}\n\n" +
                    $"**🧠 Atributos:**\n{string.Join("\n", atributosTexto)}",
                    inline: false);
            }

            await RespondAsync(embed: embedBuilder.Build(), ephemeral: true);
        }



    }
}
