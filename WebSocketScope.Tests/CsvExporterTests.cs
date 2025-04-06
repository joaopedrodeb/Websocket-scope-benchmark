using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketScope.Servicos;

namespace WebSocketScope.Tests
{
    public class CsvExporterTests
    {
        [Fact(DisplayName = "CriarCsv deve criar um novo arquivo com cabeçalho")]
        public void CriarCsv_DeveCriarArquivoComCabecalho()
        {
            // Arrange
            var dir = Path.Combine("TempTests", "CriarCsv");
            var path = Path.Combine(dir, "teste.csv");
            var cabecalho = "Coluna1 | Coluna2";

            if (File.Exists(path)) File.Delete(path);

            // Act
            CsvExporter.CriarCsv(path, cabecalho);

            // Assert
            Assert.True(File.Exists(path));
            var conteudo = File.ReadAllText(path);
            Assert.Contains(cabecalho, conteudo);
        }

        [Fact(DisplayName = "AdicionarLinha deve acrescentar uma linha ao CSV existente")]
        public void AdicionarLinha_DeveAcrescentarLinhaAoArquivo()
        {
            // Arrange
            var dir = Path.Combine("TempTests", "AdicionarLinha");
            var path = Path.Combine(dir, "teste.csv");
            CsvExporter.CriarCsv(path, "Header");

            string linha = "valor1 | valor2";

            // Act
            CsvExporter.AdicionarLinha(path, linha);

            // Assert
            var linhas = File.ReadAllLines(path);
            Assert.Equal(2, linhas.Length);
            Assert.Equal("valor1 | valor2", linhas[1]);
        }

        [Fact(DisplayName = "AdicionarLinhas deve acrescentar múltiplas linhas ao CSV")]
        public void AdicionarLinhas_DeveAcrescentarMultiplasLinhas()
        {
            // Arrange
            var dir = Path.Combine("TempTests", "AdicionarLinhas");
            var path = Path.Combine(dir, "teste.csv");
            CsvExporter.CriarCsv(path, "Header");

            var novasLinhas = new List<string> { "A | 1", "B | 2" };

            // Act
            CsvExporter.AdicionarLinhas(path, novasLinhas);

            // Assert
            var linhas = File.ReadAllLines(path);
            Assert.Equal(3, linhas.Length);
            Assert.Equal("A | 1", linhas[1]);
            Assert.Equal("B | 2", linhas[2]);
        }
    }
}
