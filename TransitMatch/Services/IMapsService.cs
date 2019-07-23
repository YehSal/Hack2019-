using System.Net.Http;
using System.Threading.Tasks;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IMapsService
    {
        Task<HttpResponseMessage> GetRouteDirections(NavigationPoint start, NavigationPoint end);
    }
}