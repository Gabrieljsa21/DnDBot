using DnDBot.Application.Helpers;
using DnDBot.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar fichas de personagem.
    /// </summary>
    public class FichaService
    {
        private List<FichaPersonagem> _fichas = new();
        private readonly string CaminhoArquivo = PathHelper.GetDataPath("fichas.json");

        public FichaService()
        {
            if (File.Exists(CaminhoArquivo))
            {
                Console.WriteLine("Carregando fichas do arquivo...");
                var json = File.ReadAllText(CaminhoArquivo);
                _fichas = JsonSerializer.Deserialize<List<FichaPersonagem>>(json) ?? new List<FichaPersonagem>();
            }
            else
            {
                Console.WriteLine("Nenhum arquivo de fichas encontrado.");
                _fichas = new List<FichaPersonagem>();
            }
        }

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
            Console.WriteLine($"Adicionando ficha do jogador {ficha.JogadorId}");
            _fichas.Add(ficha);
            SalvarFichas();
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

        private void SalvarFichas()
        {
            try
            {
                Console.WriteLine($"📁 Salvando {_fichas.Count} fichas em: {Path.GetFullPath(CaminhoArquivo)}");
                var json = JsonSerializer.Serialize(_fichas, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(CaminhoArquivo));
                File.WriteAllText(CaminhoArquivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro ao salvar fichas: " + ex.Message);
            }

        }

        // Método para remover ou atualizar fichas pode ser adicionado futuramente.
    }
}
