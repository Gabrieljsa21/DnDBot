using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;

public class Acessorio : Item
{
    // atributos específicos de Acessorio
    public List<string> PropriedadesEspeciais { get; set; } = new();
    public bool EMagico { get; set; }
    public string Raridade { get; set; }
    public List<string> Bonus { get; set; } = new();
}
