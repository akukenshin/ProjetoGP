using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelRoute.Infra;
using TravelRoute.Services;
using TravelRoute.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços
builder.Services.AddSingleton<CsvHandler>();
builder.Services.AddSingleton<RouteStorageService>(sp =>
    new RouteStorageService(sp.GetRequiredService<CsvHandler>(), "rotas.csv"));
builder.Services.AddSingleton<RouteFinderService>(sp =>
    new RouteFinderService(sp.GetRequiredService<RouteStorageService>().GetAllRoutes()));
builder.Services.AddControllers();

// Verifica o modo de execução
if (args.Length == 0)
{
    Console.WriteLine("Modo REST: Servidor iniciado...");
    var app = builder.Build();
    app.UseRouting();
    app.MapControllers();
    app.Run();
}
else if (args.Length == 1 && args[0].Equals("console", StringComparison.OrdinalIgnoreCase))
{
    Console.WriteLine("Modo Console: Iniciando aplicação em console...");
    var routeFinderService = builder.Services.BuildServiceProvider().GetRequiredService<RouteFinderService>();
    var consoleApp = new ConsoleApp(routeFinderService);
    consoleApp.Run();
}
else
{
    Console.WriteLine("Uso: ");
    Console.WriteLine("  dotnet run         -> Inicia em modo REST");
    Console.WriteLine("  dotnet run console -> Inicia em modo Console");
}
