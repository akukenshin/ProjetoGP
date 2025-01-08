using System;
using System.Collections.Generic;
using System.IO;

namespace TravelRoute.Infra
{
    public class CsvHandler
    {
        public List<Route> LoadRoutes(string filePath)
        {
            var routes = new List<Route>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    if (parts.Length != 3 || 
                        !int.TryParse(parts[2], out int cost))
                    {
                        throw new FormatException($"Invalid line format: {line}");
                    }

                    routes.Add(new Route(parts[0], parts[1], cost));
                }
            }

            return routes;
        }

        public void SaveRoutes(string filePath, List<Route> routes)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var route in routes)
                {
                    writer.WriteLine($"{route.Origin},{route.Destination},{route.Cost}");
                }
            }
        }

    }
}
