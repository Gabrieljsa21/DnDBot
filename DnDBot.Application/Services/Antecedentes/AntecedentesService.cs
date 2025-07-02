using DnDBot.Application.Models.Antecedente;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBot.Application.Services.Antecedentes
{
    public class AntecedentesService
    {
        private const string CaminhoArquivo = "Data/antecedentes.json";
        private readonly Dictionary<string, Antecedente> _cache = new();

        public AntecedentesService()
        {
            CarregarAntecedentes();
        }

        private void CarregarAntecedentes()
        {
            Console.WriteLine($"📂 Verificando arquivo antecedentes.json em: {Path.GetFullPath(CaminhoArquivo)}");

            if (!File.Exists(CaminhoArquivo))
            {
                Console.WriteLine("❌ Arquivo antecedentes.json NÃO encontrado.");
                return;
            }

            Console.WriteLine("✅ Arquivo antecedentes.json encontrado, lendo conteúdo...");

            var json = File.ReadAllText(CaminhoArquivo, Encoding.UTF8);

            var lista = JsonSerializer.Deserialize<List<Antecedente>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (lista == null)
                return;

            foreach (var antecedente in lista)
            {
                if (!string.IsNullOrWhiteSpace(antecedente.Id))
                {
                    _cache[antecedente.Id.ToLower()] = antecedente;
                }
            }
        }

        /// <summary>
        /// Retorna todos os antecedentes disponíveis.
        /// </summary>
        public IReadOnlyList<Antecedente> ObterAntecedentes()
        {
            return _cache.Values.ToList();
        }

        /// <summary>
        /// Retorna um antecedente pelo ID.
        /// </summary>
        public Antecedente ObterAntecedentePorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            _cache.TryGetValue(id.ToLower(), out var antecedente);
            return antecedente;
        }

        /// <summary>
        /// Retorna apenas os nomes dos antecedentes.
        /// </summary>
        public IReadOnlyList<string> ObterNomes()
        {
            return _cache.Values.Select(a => a.Nome).ToList();
        }

        /// <summary>
        /// Retorna os IDs dos antecedentes.
        /// </summary>
        public IReadOnlyList<string> ObterIds()
        {
            return _cache.Keys.ToList();
        }

        /// <summary>
        /// Retorna um antecedente pelo nome (caso sensível a maiúsculas/minúsculas).
        /// </summary>
        public Antecedente ObterAntecedentePorNome(string nome)
        {
            return _cache.Values.FirstOrDefault(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}
