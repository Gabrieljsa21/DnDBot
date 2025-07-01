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
                Alinhamento = "Não definido",
                DataCriacao = DateTime.UtcNow,
                DataAlteracao = DateTime.UtcNow
            };

            _fichas.Add(ficha);
            SalvarFichas();
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

        /// <summary>
        /// Salva a lista atual de fichas em disco no arquivo JSON padrão.
        /// </summary>
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

        /// <summary>
        /// Carrega todas as fichas salvas do arquivo JSON.
        /// </summary>
        /// <returns>Lista de fichas carregadas do arquivo. Retorna uma lista vazia se o arquivo não existir.</returns>
        private List<FichaPersonagem> CarregarFichas()
        {
            if (File.Exists(CaminhoArquivo))
            {
                var json = File.ReadAllText(CaminhoArquivo);
                return JsonSerializer.Deserialize<List<FichaPersonagem>>(json) ?? new List<FichaPersonagem>();
            }

            return new List<FichaPersonagem>();
        }

        /// <summary>
        /// Salva uma lista específica de fichas no arquivo JSON.
        /// </summary>
        /// <param name="fichas">Lista de fichas a serem salvas.</param>
        private void SalvarFichas(List<FichaPersonagem> fichas)
        {
            try
            {
                Console.WriteLine($"📁 Salvando {fichas.Count} fichas em: {Path.GetFullPath(CaminhoArquivo)}");
                var json = JsonSerializer.Serialize(fichas, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(CaminhoArquivo));
                File.WriteAllText(CaminhoArquivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro ao salvar fichas: " + ex.Message);
            }
        }

        /// <summary>
        /// Recupera uma ficha de personagem com base no ID do jogador e no nome do personagem.
        /// </summary>
        /// <param name="jogadorId">ID do jogador (usuário Discord).</param>
        /// <param name="nome">Nome do personagem.</param>
        /// <returns>A ficha correspondente, ou null se não encontrada.</returns>
        public FichaPersonagem ObterFichaPorJogadorENome(ulong jogadorId, string nome)
        {
            var fichas = CarregarFichas();
            return fichas.FirstOrDefault(f => f.JogadorId == jogadorId && f.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Atualiza uma ficha existente no armazenamento, sobrescrevendo seus dados.
        /// </summary>
        /// <param name="fichaAtualizada">Ficha atualizada a ser salva.</param>
        public void AtualizarFicha(FichaPersonagem fichaAtualizada)
        {
            var fichas = CarregarFichas();

            var index = fichas.FindIndex(f => f.Id == fichaAtualizada.Id);
            if (index >= 0)
            {
                fichaAtualizada.DataAlteracao = DateTime.UtcNow;
                fichas[index] = fichaAtualizada;
                SalvarFichas(fichas);
            }
        }

        /// <summary>
        /// Recupera a última ficha criada por um jogador específico.
        /// </summary>
        /// <param name="jogadorId">ID do jogador (usuário Discord).</param>
        /// <returns>Última ficha criada pelo jogador, ou null se nenhuma existir.</returns>
        public FichaPersonagem ObterUltimaFichaDoJogador(ulong jogadorId)
        {
            return _fichas
                .Where(f => f.JogadorId == jogadorId)
                .LastOrDefault();
        }



        // Método para remover ou atualizar fichas pode ser adicionado futuramente.
    }
}
