using Discord;
using DnDBot.Application.Models;
using System;
using System.Collections.Generic;

namespace DnDBot.Application.Services
{
    public class DistribuicaoAtributosHandler
    {
        private readonly DistribuicaoAtributosService _service;

        public DistribuicaoAtributosHandler(DistribuicaoAtributosService service)
        {
            _service = service;
        }

        public DistribuicaoAtributosTemp ObterDistribuicao(ulong jogadorId, Guid fichaId)
        {
            return _service.CriarOuObterDistribuicao(jogadorId, fichaId);
        }

        public void InicializarDistribuicao(DistribuicaoAtributosTemp dist, FichaPersonagem ficha)
        {
            dist.PontosDisponiveis = 27;
            dist.Atributos = new Dictionary<string, int>
            {
                { "Forca", ficha.Forca > 0 ? ficha.Forca : 8 },
                { "Destreza", ficha.Destreza > 0 ? ficha.Destreza : 8 },
                { "Constituicao", ficha.Constituicao > 0 ? ficha.Constituicao : 8 },
                { "Inteligencia", ficha.Inteligencia > 0 ? ficha.Inteligencia : 8 },
                { "Sabedoria", ficha.Sabedoria > 0 ? ficha.Sabedoria : 8 },
                { "Carisma", ficha.Carisma > 0 ? ficha.Carisma : 8 },
            };


            dist.PontosUsados = _service.CalcularCusto(dist.Atributos);

            dist.BonusRacial = new Dictionary<string, int>
            {
                { "Forca", 0 },
                { "Destreza", 0 },
                { "Constituicao", 0 },
                { "Inteligencia", 0 },
                { "Sabedoria", 0 },
                { "Carisma", 0 }
            };
        }


        /// <summary>
        /// Tenta ajustar o valor do atributo, respeitando limites e pontos disponíveis.
        /// Retorna true se conseguiu alterar, false se inválido.
        /// </summary>
        public bool TentarAjustarAtributo(ulong jogadorId, Guid fichaId, string atributo, int delta)
        {
            var dist = _service.CriarOuObterDistribuicao(jogadorId, fichaId);

            if (!dist.Atributos.ContainsKey(atributo))
                return false;

            int valorAtual = dist.Atributos[atributo];
            int novoValor = valorAtual + delta;

            // Limites padrão point buy
            if (novoValor < 8 || novoValor > 15)
                return false;

            int custoNovo = _service.CalcularCustoComAlteracao(dist.Atributos, atributo, novoValor);

            if (custoNovo > dist.PontosDisponiveis)
                return false;

            dist.Atributos[atributo] = novoValor;
            dist.PontosUsados = custoNovo;

            return true;
        }
        public Embed ConstruirEmbedDistribuicao(DistribuicaoAtributosTemp dist)
        {
            var eb = new EmbedBuilder()
                .WithTitle("Distribuição de Atributos – Point Buy")
                .WithDescription($"Total usado: {dist.PontosUsados}/{dist.PontosDisponiveis} pontos")
                .WithColor(Color.DarkBlue);

            foreach (var atributo in dist.Atributos.Keys)
            {
                string nome = atributo;
                int valor = dist.Atributos[atributo];
                int bonus = dist.BonusRacial.ContainsKey(atributo) ? dist.BonusRacial[atributo] : 0;
                string bonusTexto = bonus != 0 ? $" (Bônus Racial: +{bonus})" : "";

                eb.AddField(nome, $"{valor}{bonusTexto}", true);
            }

            return eb.Build();
        }

        public MessageComponent ConstruirComponentesDistribuicao(DistribuicaoAtributosTemp dist)
        {
            var builder = new ComponentBuilder();
            int row = 0;

            foreach (var attr in dist.Atributos.Keys)
            {
                builder.WithButton($"+ {attr}", customId: $"atributo_mais_{attr}", style: ButtonStyle.Success, row: row);
                builder.WithButton($"- {attr}", customId: $"atributo_menos_{attr}", style: ButtonStyle.Danger, row: row);
                row++;
                if (row > 3) break;
            }

            builder.WithButton("✅ Concluir", customId: "concluir_distribuicao", style: ButtonStyle.Primary, row: 4);

            return builder.Build();
        }


        public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
        {
            _service.RemoverDistribuicao(jogadorId, fichaId);
        }

    }
}
