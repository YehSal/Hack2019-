using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransitMatch.Controllers;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class RouteSegmentationServiceImpl : IRouteSegmentationService
    {
       //  private readonly AzureRoutingService _azureRoutingService;
        public async Task<List<Tuple<NavigationPoint, NavigationPoint>>> GetSegments(NavigationPoint startPoint,
            NavigationPoint endPoint)
        {
            // TODO: Implement
            return new List<Tuple<NavigationPoint, NavigationPoint>>();
        }
    }
}