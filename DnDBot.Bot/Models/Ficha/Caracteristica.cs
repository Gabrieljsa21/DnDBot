using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Bot.Models.Ficha
{
    public class Caracteristica : EntidadeBase
    {
        public TipoCaracteristica Tipo { get; set; }
        public OrigemCaracteristica Origem { get; set; }

        // Id da entidade de origem, por exemplo:
        // se Origem == Racial, esse campo pode conter a Id da raça
        // se Origem == Classe, pode conter a Id da classe, etc
        public string OrigemId { get; set; }

        /// <summary>
        /// Lista de escalas de efeito, cada uma válida para uma faixa de nível.
        /// </summary>
        public List<EfeitoEscalonado> EfeitoEscalonado { get; set; } = new();

    }
}
