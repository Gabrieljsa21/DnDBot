using DnDBot.Application.Models;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar fichas de personagem.
    /// </summary>
    public class FichaService
    {
        private readonly List<FichaPersonagem> _fichas = new();

        /// <summary>
        /// Cria uma nova ficha básica com nome e valores padrão.
        /// </summary>
        /// <param name="nome">Nome do personagem.</param>
        /// <returns>Instância de <see cref="FichaPersonagem"/> com dados padrão.</returns>
        public FichaPersonagem CriarFichaBasica(string nome)
        {
            var ficha = new FichaPersonagem
            {
                Nome = nome,
                Raca = "Não definida",
                Classe = "Não definida",
                Antecedente = "Não definido",
                Alinhamento = "Não definido"
            };

            _fichas.Add(ficha);
            return ficha;
        }

        /// <summary>
        /// Adiciona uma ficha totalmente definida à lista.
        /// </summary>
        /// <param name="ficha">Ficha a ser adicionada.</param>
        public void AdicionarFicha(FichaPersonagem ficha)
        {
            _fichas.Add(ficha);
        }

        /// <summary>
        /// Retorna todas as fichas associadas a um jogador específico.
        /// </summary>
        /// <param name="jogadorId">ID do jogador no Discord.</param>
        /// <returns>Lista de fichas do jogador.</returns>
        public List<FichaPersonagem> ObterFichasPorJogador(ulong jogadorId)
        {
            return _fichas.Where(f => f.JogadorId == jogadorId).ToList();
        }

        // Método para remover ou atualizar fichas pode ser adicionado futuramente.
    }
}
