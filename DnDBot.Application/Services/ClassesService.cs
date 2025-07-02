using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por carregar, armazenar em cache e fornecer acesso às classes de personagens.
    /// </summary>
    public class ClassesService
    {
        private const string CaminhoArquivo = "Data/classes.json";
        private readonly Dictionary<string, Classe> _cache = new();

        /// <summary>
        /// Inicializa uma nova instância do serviço e carrega as classes do arquivo JSON.
        /// </summary>
        public ClassesService()
        {
            CarregarClasses();
        }

        /// <summary>
        /// Lê o arquivo JSON contendo as classes e popula o cache interno.
        /// </summary>
        private void CarregarClasses()
        {
            Console.WriteLine($"📂 Verificando arquivo classes.json em: {Path.GetFullPath(CaminhoArquivo)}");

            if (!File.Exists(CaminhoArquivo))
            {
                Console.WriteLine("❌ Arquivo classes.json NÃO encontrado.");
                return;
            }

            Console.WriteLine("✅ Arquivo classes.json encontrado, lendo conteúdo...");

            var json = File.ReadAllText(CaminhoArquivo);

            var lista = JsonSerializer.Deserialize<List<Classe>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (lista == null)
                return;

            foreach (var classe in lista)
            {
                if (!string.IsNullOrWhiteSpace(classe.Id))
                {
                    _cache[classe.Id.ToLower()] = classe;
                }
            }
        }

        /// <summary>
        /// Obtém todas as classes disponíveis no sistema.
        /// </summary>
        /// <returns>Lista de todas as classes carregadas.</returns>
        public IReadOnlyList<Classe> ObterClasses()
        {
            return _cache.Values.ToList();
        }

        /// <summary>
        /// Obtém uma classe específica pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">ID da classe (case-insensitive).</param>
        /// <returns>A classe correspondente ao ID, ou null se não encontrada.</returns>
        public Classe ObterClassePorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            _cache.TryGetValue(id.ToLower(), out var classe);
            return classe;
        }

        /// <summary>
        /// Obtém somente os nomes das classes disponíveis.
        /// </summary>
        /// <returns>Lista contendo os nomes das classes.</returns>
        public IReadOnlyList<string> ObterNomes()
        {
            return _cache.Values.Select(c => c.Nome).ToList();
        }

        /// <summary>
        /// Obtém os IDs das classes carregadas no sistema.
        /// </summary>
        /// <returns>Lista contendo os IDs das classes.</returns>
        public IReadOnlyList<string> ObterIds()
        {
            return _cache.Keys.ToList();
        }
    }
}
