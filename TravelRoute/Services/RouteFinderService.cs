using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelRoute.Services
{
    public class RouteFinderService
    {
        private readonly List<Route> _routes;

        public RouteFinderService(List<Route> routes)
        {
            _routes = routes;
        }

        public (List<string> Path, int Cost) FindBestRoute(string origin, string destination)
        {
            var visited = new HashSet<string>();
            var bestPath = new List<string>();
            int bestCost = int.MaxValue;

            void Dfs(string current, List<string> path, int cost)
            {
                if (cost >= bestCost) return;

                if (current == destination)
                {
                    bestCost = cost;
                    bestPath = new List<string>(path);
                    return;
                }

                visited.Add(current);

                foreach (var route in _routes.Where(r => r.Origin == current && !visited.Contains(r.Destination)))
                {
                    path.Add(route.Destination);
                    Dfs(route.Destination, path, cost + route.Cost);
                    path.RemoveAt(path.Count - 1);
                }

                visited.Remove(current);
            }

            Dfs(origin, new List<string> { origin }, 0);
            return (bestPath, bestCost);
        }
    }
}