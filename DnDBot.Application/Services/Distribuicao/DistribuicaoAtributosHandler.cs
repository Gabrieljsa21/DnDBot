using Discord;
using DnDBot.Application.Models.Ficha;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Application.Services.Distribuicao
{
    /// <summary>
    /// Manipula a lógica de distribuição de atributos para fichas de personagem,
    /// incluindo a criação, atualização, construção de embeds e componentes para Discord.
    /// </summary>
    public class DistribuicaoAtributosHandler
    {
        private readonly DistribuicaoAtributosService _service;

        /// <summary>
        /// Inicializa uma nova instância do handler de distribuição de atributos.
        /// </summary>
        /// <param name="service">Serviço de distribuição de atributos utilizado para operações.</param>
        public DistribuicaoAtributosHandler(DistribuicaoAtributosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém a distribuição temporária dos atributos para um jogador e ficha específicos.
        /// </summary>
        /// <param name="jogadorId">ID do jogador (Discord).</param>
        /// <param name="fichaId">ID da ficha do personagem.</param>
        /// <returns>Objeto temporário contendo os dados da distribuição.</returns>
        public DistribuicaoAtributosTemp ObterDistribuicao(ulong jogadorId, Guid fichaId)
        {
            return _service.CriarOuObterDistribuicao(jogadorId, fichaId);
        }

        /// <summary>
        /// Inicializa a distribuição de atributos, configurando os pontos disponíveis e os valores base.
        /// </summary>
        /// <param name="dist">Objeto de distribuição temporária a ser inicializado.</param>
        /// <param name="ficha">Ficha do personagem usada para valores base dos atributos.</param>
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
        /// Tenta ajustar o valor de um atributo na distribuição, respeitando os limites do sistema Point Buy
        /// e os pontos disponíveis.
        /// </summary>
        /// <param name="jogadorId">ID do jogador (Discord).</param>
        /// <param name="fichaId">ID da ficha do personagem.</param>
        /// <param name="atributo">Nome do atributo a ser alterado (ex: "Forca").</param>
        /// <param name="delta">Valor de incremento ou decremento (+1 ou -1).</param>
        /// <returns>True se a alteração foi realizada com sucesso; False caso contrário.</returns>
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

        /// <summary>
        /// Constrói o Embed que mostra a distribuição atual dos atributos para exibição no Discord.
        /// </summary>
        /// <param name="dist">Distribuição temporária dos atributos.</param>
        /// <returns>Embed formatado com os atributos e seus valores.</returns>
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
                string bonusTexto = bonus != 0 ? $" (+{bonus})" : "";

                eb.AddField(nome, $"{valor}{bonusTexto}", true);
            }

            return eb.Build();
        }

        /// <summary>
        /// Constrói os componentes interativos (botões) para a interface de distribuição de atributos no Discord.
        /// Inclui botões para incrementar, decrementar e concluir a distribuição.
        /// </summary>
        /// <param name="dist">Distribuição temporária dos atributos.</param>
        /// <returns>MessageComponent contendo os botões para interação do usuário.</returns>
        public MessageComponent ConstruirComponentesDistribuicao(DistribuicaoAtributosTemp dist)
        {
            var builder = new ComponentBuilder();
            var atributos = dist.Atributos.Keys.ToList();

            int colunasPorLinha = 3;
            int total = atributos.Count;

            for (int i = 0; i < total; i++)
            {
                int grupo = i / colunasPorLinha; // grupo 0, 1, ...
                int pos = i % colunasPorLinha;

                int rowPositivo = grupo * 2;     // linhas 0, 2, 4...
                int rowNegativo = rowPositivo + 1; // linhas 1, 3, 5...

                // Adiciona botão + na linha rowPositivo
                if (rowPositivo <= 3) // limitar a linha 3
                {
                    builder.WithButton(
                        $"+ {atributos[i]}",
                        customId: $"atributo_mais_{atributos[i]}",
                        style: ButtonStyle.Success,
                        row: rowPositivo);
                }

                // Adiciona botão - na linha rowNegativo
                if (rowNegativo <= 3) // limitar a linha 3
                {
                    builder.WithButton(
                        $"- {atributos[i]}",
                        customId: $"atributo_menos_{atributos[i]}",
                        style: ButtonStyle.Danger,
                        row: rowNegativo);
                }
            }

            // Botão concluir na linha 4
            builder.WithButton("✅ Concluir", customId: "concluir_distribuicao", style: ButtonStyle.Primary, row: 4);

            return builder.Build();
        }

        /// <summary>
        /// Remove a distribuição temporária associada ao jogador e ficha especificados.
        /// </summary>
        /// <param name="jogadorId">ID do jogador (Discord).</param>
        /// <param name="fichaId">ID da ficha do personagem.</param>
        public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
        {
            _service.RemoverDistribuicao(jogadorId, fichaId);
        }
    }
}
