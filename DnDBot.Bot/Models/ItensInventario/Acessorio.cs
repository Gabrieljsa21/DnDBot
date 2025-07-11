using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;

public class Acessorio : Item
{
    public List<string> PropriedadesEspeciais { get; set; } = new();
}
