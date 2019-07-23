using System;
using System.Collections.Generic;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class PathFindingServiceImpl : IPathFindingService
    {
        /**
         * costMatrix is a 2D array double[routeTypes, routeSegments] of cost for each segment
         */
        public List<RoutingSegment> GetOptimalPath(List<Tuple<NavigationPoint, NavigationPoint>> routeSegments,
            double[,] costMatrix)
        {
            var route = new List<RoutingSegment>();
            NavigationMode lastMode = NavigationMode.Walk;
            for (var i = 0; i < routeSegments.Count; i++)
            {
                double cost = double.PositiveInfinity;
                var selectedMode = NavigationMode.Walk;
                for (var j = 0; j < costMatrix.GetLength(0); j++)
                {
                    var calculatedCost = costMatrix[j, i] + _switchCostMatrix[(int) lastMode, j];
                    if (calculatedCost < cost)
                    {
                        cost = calculatedCost;
                        selectedMode = (NavigationMode) j;
                    }
                }
                route.Add(new RoutingSegment(routeSegments[i].Item1, routeSegments[i].Item2, selectedMode));
                lastMode = selectedMode;
            }

            return route;
        }

        // TODO: Don't have magic number matrix
        private double[,] _switchCostMatrix = {
            {0, 1, 2, 3, 4 },
            {1, 0, 2, 3, 4 },
            {1, 2, 0, 3, 4 },
            { 3, 1, 2, 0, 4 },
            {4, 1, 2, 3, 0 },
        };
    }
}