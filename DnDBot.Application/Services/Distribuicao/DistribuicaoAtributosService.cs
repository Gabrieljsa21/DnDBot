using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

/// <summary>
/// Serviço responsável por gerenciar as distribuições temporárias de atributos de personagens,
/// incluindo criação, remoção e cálculo de custo segundo as regras de Point Buy do D&D 5e.
/// </summary>
public class DistribuicaoAtributosService
{
    private readonly ConcurrentDictionary<(ulong jogadorId, Guid fichaId), DistribuicaoAtributosTemp> _distribuicoes
        = new();

    /// <summary>
    /// Cria uma nova distribuição para o jogador e ficha, ou obtém a existente caso já tenha sido criada.
    /// </summary>
    /// <param name="jogadorId">ID do jogador (Discord).</param>
    /// <param name="fichaId">ID da ficha do personagem.</param>
    /// <returns>Objeto temporário com os dados da distribuição de atributos.</returns>
    public DistribuicaoAtributosTemp CriarOuObterDistribuicao(ulong jogadorId, Guid fichaId)
    {
        return _distribuicoes.GetOrAdd((jogadorId, fichaId), key => new DistribuicaoAtributosTemp
        {
            JogadorId = jogadorId,
            FichaId = fichaId
        });
    }

    /// <summary>
    /// Remove a distribuição temporária associada a um jogador e ficha.
    /// </summary>
    /// <param name="jogadorId">ID do jogador (Discord).</param>
    /// <param name="fichaId">ID da ficha do personagem.</param>
    public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
    {
        _distribuicoes.TryRemove((jogadorId, fichaId), out _);
    }

    /// <summary>
    /// Calcula o custo total da distribuição dos atributos segundo a tabela Point Buy.
    /// </summary>
    /// <param name="atributos">Dicionário com os valores dos atributos.</param>
    /// <returns>Custo total em pontos usados.</returns>
    public int CalcularCusto(Dictionary<string, int> atributos)
    {
        int total = 0;
        foreach (var valor in atributos.Values)
            total += CalcularCustoUnitario(valor);

        return total;
    }

    /// <summary>
    /// Calcula o custo total da distribuição considerando uma alteração no valor de um atributo específico.
    /// </summary>
    /// <param name="atributos">Dicionário atual de atributos.</param>
    /// <param name="atributo">Nome do atributo que será alterado.</param>
    /// <param name="novoValor">Novo valor para o atributo.</param>
    /// <returns>Custo total em pontos usados após a alteração.</returns>
    public int CalcularCustoComAlteracao(Dictionary<string, int> atributos, string atributo, int novoValor)
    {
        var copia = new Dictionary<string, int>(atributos);
        copia[atributo] = novoValor;
        return CalcularCusto(copia);
    }

    /// <summary>
    /// Calcula o custo unitário de um valor de atributo segundo as regras de Point Buy.
    /// </summary>
    /// <param name="valor">Valor do atributo.</param>
    /// <returns>Custo em pontos para aquele valor.</returns>
    private int CalcularCustoUnitario(int valor)
    {
        return valor switch
        {
            8 => 0,
            9 => 1,
            10 => 2,
            11 => 3,
            12 => 4,
            13 => 5,
            14 => 7,
            15 => 9,
            _ => 999 // valor fora dos limites válidos
        };
    }
}
