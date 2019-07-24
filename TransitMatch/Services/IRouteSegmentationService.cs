using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuickGraph;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IRouteSegmentationService
    {
        Task<AdjacencyGraph<NavigationPoint, TransportEdge<NavigationPoint>>> GetGraph(NavigationPoint startPoint,
            NavigationPoint endPoint);
    }
}