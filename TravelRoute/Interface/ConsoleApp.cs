using System;
using System.Collections.Generic;
using TravelRoute.Infra;
using TravelRoute.Services;

namespace TravelRoute.Interfaces
{
    public class ConsoleApp
    {
        private readonly RouteFinderService _routeFinderService;

        public ConsoleApp(RouteFinderService routeFinderService)
        {
            _routeFinderService = routeFinderService;
        }

        public void Run()
        {
            Console.WriteLine("Bem-vindo ao sistema de rotas!");
            Console.WriteLine("Digite uma rota no formato ORIGEM-DESTINO ou 'sair' para encerrar.");

            while (true)
            {
                Console.Write("Digite a rota: ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;
                if (input.ToLower() == "sair") break;

                var points = input.Split('-');
                if (points.Length != 2)
                {
                    Console.WriteLine("Formato inválido! Use ORIGEM-DESTINO.");
                    continue;
                }

                var origin = points[0].ToUpper();
                var destination = points[1].ToUpper();

                try
                {
                    var (path, cost) = _routeFinderService.FindBestRoute(origin, destination);

                    if (path.Count > 0)
                    {
                        Console.WriteLine($"Melhor Rota: {string.Join(" - ", path)} ao custo de ${cost}");
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma rota encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }

            Console.WriteLine("Programa encerrado.");
        }
    }
}
