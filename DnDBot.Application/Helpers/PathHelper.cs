using System;
using System.IO;

namespace DnDBot.Application.Helpers
{
    public static class PathHelper
    {
        /// <summary>
        /// Retorna o caminho completo para um arquivo dentro da pasta 'Data' do projeto DnDBot.Application.
        /// </summary>
        public static string GetDataPath(string fileName)
        {
            var dataDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "DnDBot.Application", "Data");
            var fullPath = Path.GetFullPath(dataDir);
            Directory.CreateDirectory(fullPath); // Garante que a pasta existe
            return Path.Combine(fullPath, fileName);
        }
    }
}
