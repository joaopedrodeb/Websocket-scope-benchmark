using System;
using System.Collections.Generic;
using System.IO;

namespace WebSocketScope.Servicos
{
    public static class CsvExporter
    {
        /// <summary>
        /// Cria um novo arquivo CSV com cabeçalho.
        /// </summary>
        public static void CriarCsv(string caminho, string cabecalho)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(caminho)!);

            using var writer = new StreamWriter(caminho, append: false);
            writer.WriteLine(cabecalho);
        }

        /// <summary>
        /// Adiciona uma linha ao CSV existente.
        /// </summary>
        public static void AdicionarLinha(string caminho, string linha)
        {
            using var writer = new StreamWriter(caminho, append: true);
            writer.WriteLine(linha);
        }

        /// <summary>
        /// Adiciona múltiplas linhas ao CSV existente.
        /// </summary>
        public static void AdicionarLinhas(string caminho, IEnumerable<string> linhas)
        {
            using var writer = new StreamWriter(caminho, append: true);
            foreach (var linha in linhas)
                writer.WriteLine(linha);
        }
    }
}
