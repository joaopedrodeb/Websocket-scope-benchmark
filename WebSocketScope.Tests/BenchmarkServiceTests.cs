using Xunit;
using WebSocketScope.Servicos;

namespace WebSocketScope.Tests
{
    public class BenchmarkServiceTests
    {
        [Fact]
        public void GerarLog_DeveRetornarStringFormatada()
        {
            // Arrange
            var agora = new DateTime(2025, 4, 6, 10, 30, 15);
            int conexoes = 123;
            double memoria = 45.67;

            // Act
            string resultado = BenchmarkService.GerarLog(agora, conexoes, memoria);

            // Assert
            Assert.Equal("10:30:15 |             123 |      45,67 MB", resultado);
        }
    }
}
