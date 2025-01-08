using System.Collections.Generic;
using TravelRoute;
using TravelRoute.Services;
using Xunit;

namespace TravelRoute.Tests.Services
{
    public class RouteFinderServiceTests
    {
        [Fact]
        public void FindBestRoute_ShouldReturnBestRoute_ForValidInput()
        {
            // Arrange
            var routes = new List<Route>
            {
                new Route("GRU", "BRC", 10),
                new Route("BRC", "SCL", 5),
                new Route("GRU", "CDG", 75),
                new Route("GRU", "SCL", 20),
                new Route("SCL", "ORL", 20),
                new Route("ORL", "CDG", 5)
            };
            var service = new RouteFinderService(routes);

            // Act
            var (path, cost) = service.FindBestRoute("GRU", "CDG");

            // Assert
            Assert.Equal(new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" }, path);
            Assert.Equal(40, cost);
        }

        [Fact]
        public void FindBestRoute_ShouldReturnEmptyPath_ForInvalidInput()
        {
            // Arrange
            var routes = new List<Route>
            {
                new Route("GRU", "BRC", 10),
                new Route("BRC", "SCL", 5)
            };
            var service = new RouteFinderService(routes);

            // Act
            var (path, cost) = service.FindBestRoute("GRU", "XYZ");

            // Assert
            Assert.Empty(path);
            Assert.Equal(int.MaxValue, cost);
        }
    }
}
