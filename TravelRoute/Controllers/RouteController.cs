using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TravelRoute.Services;

namespace TravelRoute.Controllers
{
    [ApiController]
    [Route("api/routes")]
    public class RouteController : ControllerBase
    {
        private readonly RouteFinderService _routeFinderService;
        private readonly RouteStorageService _routeStorageService;

        public RouteController(RouteFinderService routeFinderService, RouteStorageService routeStorageService)
        {
            _routeFinderService = routeFinderService;
            _routeStorageService = routeStorageService;
        }

        // Endpoint para buscar a melhor rota
        [HttpGet("best-route")]
        public IActionResult GetBestRoute([FromQuery] string origin, [FromQuery] string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
                return BadRequest("Origin and destination must be provided.");

            var (path, cost) = _routeFinderService.FindBestRoute(origin.ToUpper(), destination.ToUpper());

            if (path.Count == 0)
                return NotFound("No route found.");

            return Ok(new { Path = string.Join(" - ", path), Cost = cost });
        }

        // Endpoint para adicionar nova rota
        [HttpPost]
        public IActionResult AddRoute([FromBody] Route newRoute)
        {
            if (newRoute == null || string.IsNullOrEmpty(newRoute.Origin) || 
                string.IsNullOrEmpty(newRoute.Destination) || newRoute.Cost <= 0)
                return BadRequest("Invalid route data.");

            _routeStorageService.AddRoute(newRoute);
            return Ok("Route added successfully.");
        }
    }
}
