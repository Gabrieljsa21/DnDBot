using System;
using System.Collections.Generic;

public class DistribuicaoAtributosTemp
{
    public ulong JogadorId { get; set; }
    public Guid FichaId { get; set; }

    public Dictionary<string, int> Atributos { get; set; } = new()
{
    { "Forca", 8 },
    { "Destreza", 8 },
    { "Constituicao", 8 },
    { "Inteligencia", 8 },
    { "Sabedoria", 8 },
    { "Carisma", 8 }
};

    public Dictionary<string, int> BonusRacial { get; set; } = new()
{
    { "Forca", 0 },
    { "Destreza", 0 },
    { "Constituicao", 0 },
    { "Inteligencia", 0 },
    { "Sabedoria", 0 },
    { "Carisma", 0 }
};


    public int PontosDisponiveis { get; set; } = 27;
    public int PontosUsados { get; set; } = 0;
}
