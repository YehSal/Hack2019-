using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IMapsService
    {
        Task<RouteDirectionsResult> GetRouteDirections(NavigationPoint start, NavigationPoint end, NavigationMode navMode,
            int numRoutes = 3);

        Task<List<NavigationPoint>> GetNearbyTransit(NavigationPoint point);
    }
}