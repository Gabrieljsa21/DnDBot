using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class DistribuicaoAtributosService
{
    private readonly ConcurrentDictionary<(ulong jogadorId, Guid fichaId), DistribuicaoAtributosTemp> _distribuicoes
        = new();

    public DistribuicaoAtributosTemp CriarOuObterDistribuicao(ulong jogadorId, Guid fichaId)
    {

        Console.WriteLine($"[LOG] CriarOuObterDistribuicao - Key: ({jogadorId}, {fichaId}), Já contém chave? {_distribuicoes.ContainsKey((jogadorId, fichaId))}");
        var dist = _distribuicoes.GetOrAdd((jogadorId, fichaId), key =>
        {
            Console.WriteLine($"[LOG] Criando nova distribuição de atributos para jogador {jogadorId}, ficha {fichaId}");
            return new DistribuicaoAtributosTemp
            {
                JogadorId = jogadorId,
                FichaId = fichaId,
                Atributos = new Dictionary<string, int>
                {
                    { "Forca", 8 },
                    { "Destreza", 8 },
                    { "Constituicao", 8 },
                    { "Inteligencia", 8 },
                    { "Sabedoria", 8 },
                    { "Carisma", 8 }
                },
                BonusRacial = new Dictionary<string, int>
                {
                    { "Forca", 0 },
                    { "Destreza", 0 },
                    { "Constituicao", 0 },
                    { "Inteligencia", 0 },
                    { "Sabedoria", 0 },
                    { "Carisma", 0 }
                },
                PontosDisponiveis = 27,
                PontosUsados = 0
            };
        });

        Console.WriteLine($"[LOG] Distribuição obtida para jogador {jogadorId}, ficha {fichaId}");
        return dist;
    }

    public void RemoverDistribuicao(ulong jogadorId, Guid fichaId)
    {
        if (_distribuicoes.TryRemove((jogadorId, fichaId), out _))
            Console.WriteLine($"[LOG] Distribuição removida da memória para jogador {jogadorId}, ficha {fichaId}");
        else
            Console.WriteLine($"[LOG] Tentativa de remover distribuição falhou: nenhuma encontrada para jogador {jogadorId}, ficha {fichaId}");
    }

    public int CalcularCusto(Dictionary<string, int> atributos)
    {
        int total = 0;
        foreach (var (nome, valor) in atributos)
        {
            var custo = CalcularCustoUnitario(valor);
            total += custo;
            Console.WriteLine($"[LOG] Custo para {nome} ({valor}) = {custo}");
        }

        Console.WriteLine($"[LOG] Custo total da distribuição: {total}");
        return total;
    }

    public int CalcularCustoComAlteracao(Dictionary<string, int> atributos, string atributo, int novoValor)
    {
        var copia = new Dictionary<string, int>(atributos);
        copia[atributo] = novoValor;
        var total = CalcularCusto(copia);
        Console.WriteLine($"[LOG] Novo custo após alterar {atributo} para {novoValor}: {total}");
        return total;
    }

    private int CalcularCustoUnitario(int valor)
    {
        int custo = valor switch
        {
            8 => 0,
            9 => 1,
            10 => 2,
            11 => 3,
            12 => 4,
            13 => 5,
            14 => 7,
            15 => 9,
            _ => -1
        };

        if (custo == -1)
            Console.WriteLine($"[ERRO] Valor de atributo inválido: {valor}");

        return custo == -1 ? 999 : custo;
    }
}
