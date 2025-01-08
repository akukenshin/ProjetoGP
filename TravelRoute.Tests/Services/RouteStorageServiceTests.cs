using System.IO;
using TravelRoute;
using TravelRoute.Infra;
using TravelRoute.Services;
using Xunit;

namespace TravelRoute.Tests.Services
{
    public class RouteStorageServiceTests
    {
        [Fact]
        public void AddRoute_ShouldAddRoute_ToTheList()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "GRU,BRC,10\n");

            var csvHandler = new CsvHandler();
            var service = new RouteStorageService(csvHandler, tempFilePath);

            var newRoute = new Route("BRC", "SCL", 5);

            try
            {
                // Act
                service.AddRoute(newRoute);

                // Assert
                var routes = service.GetAllRoutes();
                Assert.Equal(2, routes.Count);
                Assert.Contains(routes, r => r.Origin == "BRC" && r.Destination == "SCL" && r.Cost == 5);
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
