using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms.Search;
using QuickGraph.Serialization.DirectedGraphML;
using TransitMatch.Controllers;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class RouteSegmentationServiceImpl : IRouteSegmentationService
    {
        private readonly IMapsService _mapsService;

        public RouteSegmentationServiceImpl(IMapsService mapsService)
        {
            _mapsService = mapsService;
        }

        public async Task<AdjacencyGraph<NavigationPoint, TransportEdge<NavigationPoint>>> GetGraph(
            NavigationPoint startPoint,
            NavigationPoint endPoint)
        {
            var navGraph = new AdjacencyGraph<NavigationPoint, TransportEdge<NavigationPoint>>(false);
            var startBusStops = await _mapsService.GetNearbyTransit(startPoint);
            var endBusStops = await _mapsService.GetNearbyTransit(endPoint);
            List<NavigationPoint> interestingPoints = new List<NavigationPoint>();
            interestingPoints.Add(startPoint);
            interestingPoints.Add(endPoint);
            interestingPoints.AddRange(startBusStops);
            interestingPoints.AddRange(endBusStops);
            navGraph.AddVertexRange(interestingPoints);

            // Add edges
            // TODO: Add based on radius and user preferences instead.
            // TODO: Maybe use a list of nav modes?
            foreach (var busStop in startBusStops)
            {
                navGraph.AddEdge(new TransportEdge<NavigationPoint>(startPoint, busStop, NavigationMode.Rideshare));
            }

            foreach (var busStop in endBusStops)
            {
                navGraph.AddEdge(new TransportEdge<NavigationPoint>(busStop, endPoint, NavigationMode.Rideshare));
            }

            foreach (var startBusStop in startBusStops)
            {
                foreach (var endBusStop in endBusStops)
                {
                    navGraph.AddEdge(new TransportEdge<NavigationPoint>(startBusStop, endBusStop, NavigationMode.Transit));
                }
            }
            return navGraph;
        }
    }
}