using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Distribuicao;
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

        public ComandoSubraca(FichaService fichaService, RacasService racasService, DistribuicaoAtributosHandler atributosHandler)
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _atributosHandler = atributosHandler;
        }

        [ComponentInteraction("select_subraca")]
        public async Task SelectSubracaHandler(string valor)
        {
            Console.WriteLine($"[LOG] Usuário {Context.User.Id} selecionou a sub-raça: {valor}");

            var ficha = await _fichaService.ObterUltimaFichaDoJogadorAsync(Context.User.Id);
            if (ficha == null)
            {
                Console.WriteLine("[LOG] Nenhuma ficha encontrada.");
                await RespondAsync("❌ Não encontrei a ficha para atualizar a sub-raça.", ephemeral: true);
                return;
            }

            var subraca = (await _racasService.ObterTodasSubracasAsync()).FirstOrDefault(sr => sr.Id == valor);
            if (subraca == null)
            {
                Console.WriteLine("[LOG] Sub-raça não encontrada.");
                await RespondAsync("❌ Sub-raça selecionada não encontrada.", ephemeral: true);
                return;
            }

            Console.WriteLine($"[LOG] Sub-raça '{subraca.Id}' encontrada. Aplicando à ficha '{ficha.Nome}' ({ficha.Id})");

            // Atualiza os campos da ficha
            ficha.SubracaId = subraca.Id;
            ficha.BonusAtributos = subraca.BonusAtributos
                .Select(b => new BonusAtributo
                {
                    Atributo = b.Atributo,
                    Valor = b.Valor,
                    Origem = "Sub-Raça"
                }).ToList();
            ficha.Tamanho = subraca.Tamanho.ToString();
            ficha.Deslocamento = subraca.Deslocamento;
            ficha.Idiomas = subraca.Idiomas;
            ficha.Proficiencias = subraca.Proficiencias;
            ficha.VisaoNoEscuro = subraca.VisaoNoEscuro;
            ficha.Resistencias = subraca.Resistencias.ToList();
            ficha.Caracteristicas = subraca.Caracteristicas.ToList();
            ficha.MagiasRaciais = subraca.MagiasRaciais.ToList();

            await _fichaService.AtualizarFichaAsync(ficha);
            Console.WriteLine("[LOG] Ficha atualizada com dados da sub-raça.");

            // Cria e inicializa a distribuição temporária
            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            _atributosHandler.InicializarDistribuicao(dist, ficha);

            // Aplica os bônus raciais da sub-raça
            foreach (var bonus in ficha.BonusAtributos)
            {
                if (dist.BonusRacial.ContainsKey(bonus.Atributo.ToString()))
                    dist.BonusRacial[bonus.Atributo.ToString()] = bonus.Valor;
            }

            Console.WriteLine("[LOG] Distribuição de atributos inicializada:");
            foreach (var attr in dist.Atributos)
            {
                int bonus = dist.BonusRacial.ContainsKey(attr.Key) ? dist.BonusRacial[attr.Key] : 0;
                Console.WriteLine($" - {attr.Key}: {attr.Value} (+{bonus})");
            }
            Console.WriteLine($"[LOG] Pontos usados: {dist.PontosUsados}/27");

            // Envia mensagem com embed e botões
            await RespondAsync(
                $"✅ Sub-raça **{subraca.Id}** aplicada à ficha **{ficha.Nome}** com sucesso!\n\nAgora distribua os pontos nos atributos:",
                embed: _atributosHandler.ConstruirEmbedDistribuicao(dist),
                components: _atributosHandler.ConstruirComponentesDistribuicao(dist, ficha.Id),
                ephemeral: true);
        }
    }
}
