using System.IO;
using TravelRoute;
using TravelRoute.Infra;
using Xunit;

namespace TravelRoute.Tests.Infra
{
    public class CsvHandlerTests
    {
        [Fact]
        public void LoadRoutes_ShouldReturnRoutes_ForValidCsvFile()
        {
            // Arrange
            var csvContent = "GRU,BRC,10\nBRC,SCL,5\n";
            var filePath = "test_routes.csv";

            // Escreve o conteúdo no arquivo
            File.WriteAllText(filePath, csvContent);

            try
            {
                var handler = new CsvHandler();

                // Act
                var routes = handler.LoadRoutes(filePath);

                // Assert
                Assert.NotNull(routes);
                Assert.Equal(2, routes.Count);
                Assert.Equal("GRU", routes[0].Origin);
                Assert.Equal("BRC", routes[0].Destination);
                Assert.Equal(10, routes[0].Cost);
            }
            finally
            {
                // Cleanup: Sempre tenta deletar o arquivo
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [Fact]
        public void LoadRoutes_ShouldHandleEmptyFile()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, ""); // Arquivo vazio
            var handler = new CsvHandler();

            // Act
            var routes = handler.LoadRoutes(tempFilePath);

            // Assert
            Assert.NotNull(routes);
            Assert.Empty(routes);

            // Cleanup
            File.Delete(tempFilePath);
        }

    }
}
