using System.Collections.Generic;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IRoutesService
    {
        Task<List<RouteDirectionsResult>> GetRidesAsync(double lat1, double lon1, double lat2, double lon2, NavigationMode mode);
    }
}