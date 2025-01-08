using System.Collections.Generic;
using TravelRoute.Infra;
using TravelRoute;

namespace TravelRoute.Services
{
    public class RouteStorageService
    {
        private readonly CsvHandler _csvHandler;
        private readonly string _filePath;
        private List<Route> _routes;

        public RouteStorageService(CsvHandler csvHandler, string filePath)
        {
            _csvHandler = csvHandler;
            _filePath = filePath;
            _routes = _csvHandler.LoadRoutes(_filePath);
        }

        public List<Route> GetAllRoutes() => _routes;

        public void AddRoute(Route route)
        {
            _routes.Add(route);
            _csvHandler.SaveRoutes(_filePath, _routes);
        }
    }
}
