using Discord;
using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Bot.Services.Distribuicao
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

            //Sera usado a Variante de Aumento de Pontuação de Habilidade +2 e +1 em vez do bonus racial
            //dist.BonusRacial = new Dictionary<string, int>
            //{
            //    { "Forca", 0 },
            //    { "Destreza", 0 },
            //    { "Constituicao", 0 },
            //    { "Inteligencia", 0 },
            //    { "Sabedoria", 0 },
            //    { "Carisma", 0 }
            //};

            Console.WriteLine($"[LOG] Atributos base inicializados:");
            foreach (var attr in dist.Atributos)
            {
                Console.WriteLine($" - {attr.Key}: {attr.Value} (+{dist.BonusRacial[attr.Key]})");
            }

            Console.WriteLine($"[LOG] Pontos usados: {dist.PontosUsados}/27");
        }

        public bool TentarAjustarAtributo(ulong jogadorId, Guid fichaId, string atributo, int delta)
        {
            var dist = _service.CriarOuObterDistribuicao(jogadorId, fichaId);

            if (!dist.Atributos.ContainsKey(atributo))
            {
                Console.WriteLine($"[ERRO] Atributo '{atributo}' inválido para ficha {fichaId}");
                return false;
            }

            int valorAtual = dist.Atributos[atributo];
            int novoValor = valorAtual + delta;

            if (novoValor < 8 || novoValor > 15)
            {
                Console.WriteLine($"[ERRO] Valor fora do limite para {atributo}: {novoValor} (limite: 8-15)");
                return false;
            }

            int custoNovo = _service.CalcularCustoComAlteracao(dist.Atributos, atributo, novoValor);

            if (custoNovo > dist.PontosDisponiveis)
            {
                Console.WriteLine($"[ERRO] Pontos insuficientes para ajustar {atributo} para {novoValor}. Custo: {custoNovo}, Disponível: {dist.PontosDisponiveis}");
                return false;
            }

            Console.WriteLine($"[LOG] Ajustando atributo {atributo} de {valorAtual} para {novoValor} (delta {delta})");
            Console.WriteLine($"[LOG] Custo novo: {custoNovo}, Pontos disponíveis: {dist.PontosDisponiveis}");

            dist.Atributos[atributo] = novoValor;
            dist.PontosUsados = custoNovo;

            Console.WriteLine($"[LOG] Atributo {atributo} atualizado para {novoValor}. Pontos usados: {dist.PontosUsados}");
            return true;
        }

        public Embed ConstruirEmbedDistribuicao(DistribuicaoAtributosTemp dist, FichaPersonagem ficha)
        {
            //Console.WriteLine("[LOG] Construindo embed de atributos:");
            foreach (var attr in dist.Atributos)
            {
                int bonus = dist.BonusRacial.ContainsKey(attr.Key) ? dist.BonusRacial[attr.Key] : 0;
                //Console.WriteLine($" - {attr.Key}: {attr.Value} (+{bonus})");
            }

            //Console.WriteLine($"[LOG] Pontos usados: {dist.PontosUsados}/{dist.PontosDisponiveis}");

            var eb = new EmbedBuilder()
                .WithTitle("Distribuição de Atributos – Point Buy")
                .WithDescription($"Total usado: {dist.PontosUsados}/{dist.PontosDisponiveis} pontos")
                .WithColor(Color.DarkBlue);

            foreach (var atributo in dist.Atributos.Keys)
            {
                string nome = atributo;
                int valor = dist.Atributos[atributo];
                int bonus = ficha.ObterBonusTotal(atributo);
                string bonusTexto = bonus != 0 ? $" (+{bonus})" : "";

                eb.AddField(nome, $"{valor}{bonusTexto}", true);
            }

            return eb.Build();
        }


        public MessageComponent ConstruirComponentesDistribuicao(DistribuicaoAtributosTemp dist, Guid fichaId)
        {
            var builder = new ComponentBuilder();
            var atributos = dist.Atributos.Keys.ToList();
            int colunasPorLinha = 3;
            int total = atributos.Count;

            for (int i = 0; i < total; i++)
            {
                int grupo = i / colunasPorLinha;
                int rowPositivo = grupo * 2;
                int rowNegativo = rowPositivo + 1;

                if (rowPositivo <= 3)
                {
                    builder.WithButton(
                        $"+ {atributos[i]}",
                        customId: $"atributo_mais_{atributos[i]}_{fichaId}",
                        style: ButtonStyle.Success,
                        row: rowPositivo);
                }

                if (rowNegativo <= 3)
                {
                    builder.WithButton(
                        $"- {atributos[i]}",
                        customId: $"atributo_menos_{atributos[i]}_{fichaId}",
                        style: ButtonStyle.Danger,
                        row: rowNegativo);
                }
            }

            builder.WithButton("✅ Concluir", customId: $"concluir_distribuicao_{fichaId}", style: ButtonStyle.Primary, row: 4);

            return builder.Build();
        }


        public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
        {
            Console.WriteLine($"[LOG] Removendo distribuição temporária para jogador {jogadorId}, ficha {fichaId}");
            _service.RemoverDistribuicao(jogadorId, fichaId);
        }
    }
}
