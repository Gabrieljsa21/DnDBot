using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class DistribuicaoAtributosService
{
    private readonly ConcurrentDictionary<(ulong jogadorId, Guid fichaId), DistribuicaoAtributosTemp> _distribuicoes
        = new();

    public DistribuicaoAtributosTemp CriarOuObterDistribuicao(ulong jogadorId, Guid fichaId)
    {
        return _distribuicoes.GetOrAdd((jogadorId, fichaId), key => new DistribuicaoAtributosTemp
        {
            JogadorId = jogadorId,
            FichaId = fichaId
        });
    }

    public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
    {
        _distribuicoes.TryRemove((jogadorId, fichaId), out _);
    }

    public int CalcularCusto(Dictionary<string, int> atributos)
    {
        int total = 0;
        foreach (var valor in atributos.Values)
            total += CalcularCustoUnitario(valor);

        return total;
    }

    public int CalcularCustoComAlteracao(Dictionary<string, int> atributos, string atributo, int novoValor)
    {
        var copia = new Dictionary<string, int>(atributos);
        copia[atributo] = novoValor;
        return CalcularCusto(copia);
    }

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
            _ => 999 // fora dos limites válidos
        };
    }

}
