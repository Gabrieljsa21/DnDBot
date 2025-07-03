using System.Collections.Generic;
using DnDBot.Application.Models.Ficha;

namespace DnDBot.Application.Data
{
    /// <summary>
    /// Lista estática dos nove alinhamentos clássicos do Dungeons & Dragons 5ª Edição.
    /// Cada alinhamento possui um Id, Nome e Descrição oficial que definem seu comportamento e filosofia.
    /// </summary>
    public static class AlinhamentosData
    {
        /// <summary>
        /// Lista contendo todos os alinhamentos disponíveis no sistema.
        /// </summary>
        public static readonly List<Alinhamento> Alinhamentos = new()
        {
            new Alinhamento
            {
                Id = "lg",
                Nome = "Leal e Bom (LG)",
                Descricao = "Age com compaixão e honra, respeitando as leis e a ordem para promover o bem-estar de todos."
            },
            new Alinhamento
            {
                Id = "ng",
                Nome = "Neutro e Bom (NG)",
                Descricao = "Faz o que é certo, buscando o bem sem se prender a regras ou autoridade."
            },
            new Alinhamento
            {
                Id = "cg",
                Nome = "Caótico e Bom (CG)",
                Descricao = "Valoriza a liberdade e o bem, agindo conforme a própria consciência, mesmo que isso signifique desafiar regras."
            },
            new Alinhamento
            {
                Id = "ln",
                Nome = "Leal e Neutro (LN)",
                Descricao = "Prioriza a lei, a ordem e o dever, independente de ser para o bem ou mal."
            },
            new Alinhamento
            {
                Id = "n",
                Nome = "Neutro Puro (N)",
                Descricao = "Busca o equilíbrio, evitando extremos entre bem e mal, ordem e caos."
            },
            new Alinhamento
            {
                Id = "cn",
                Nome = "Caótico e Neutro (CN)",
                Descricao = "Age segundo seus próprios desejos, valorizando a liberdade e a independência sem se importar com o bem ou mal."
            },
            new Alinhamento
            {
                Id = "le",
                Nome = "Leal e Mal (LE)",
                Descricao = "Usa a lei e a ordem para fins egoístas e cruéis, impondo tirania e controle."
            },
            new Alinhamento
            {
                Id = "ne",
                Nome = "Neutro e Mal (NE)",
                Descricao = "Age com egoísmo e crueldade, sem se preocupar com regras ou consequências."
            },
            new Alinhamento
            {
                Id = "ce",
                Nome = "Caótico e Mal (CE)",
                Descricao = "Valoriza o caos e a destruição, agindo violentamente sem respeitar regras ou a vida alheia."
            }
        };
    }
}
