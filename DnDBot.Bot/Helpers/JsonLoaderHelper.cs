using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDBot.Bot.Helpers
{
    public static class JsonLoaderHelper
    {
        public static async Task<T?> CarregarAsync<T>(string caminhoJson, string nomeArquivo, JsonSerializerOptions? options = null)
        {
            if (!File.Exists(caminhoJson))
            {
                Console.WriteLine($"❌ Arquivo {nomeArquivo}.json não encontrado.");
                return default;
            }

            Console.WriteLine($"📥 Lendo dados de {nomeArquivo}.json...");

            var json = await File.ReadAllTextAsync(caminhoJson, Encoding.UTF8);

            options ??= new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var resultado = JsonSerializer.Deserialize<T>(json, options);

            if (resultado == null)
            {
                Console.WriteLine($"❌ Nenhum dado válido encontrado no arquivo {nomeArquivo}.json.");
            }

            return resultado;
        }
    }
}
