using System;
using System.Threading.Tasks;
using TransitMatch.Models;
using TransitMatch.Services;
using System.Collections.Generic;
using AzureMapsToolkit.Common;
using System.Linq;

namespace TransitMatch.Impl.CostFunctions
{
    public class HelperCostFunction : IGetAdjPointsFunction
    {
        public List<List<RouteLegWithCost>> GetAdjPoints(RouteDirectionsResult routeDirectionsResult, String mode)
        {
            // Big array of legs
            // Small array of one leg
            // Small array has an object that has two members: pair of points and cost

            // Array of arrays (per leg) that has tuples of adjacent points
            var allLegsAdjPoints = new List<List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>>();
            for (int i = 0; i < routeDirectionsResult.Legs.Length; i++)
            {
                List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>> legAdjPoints
                    = new List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>();
                for (int j = 0; j < routeDirectionsResult.Legs[i].Points.Length; i += 2)
                {
                    if (j > routeDirectionsResult.Legs[i].Points.Length) break;

                    legAdjPoints.Add(
                        Tuple.Create(
                            routeDirectionsResult.Legs[i].Points[j],
                            routeDirectionsResult.Legs[i].Points[j + 1]
                        )
                    );
                }
                allLegsAdjPoints.Add(legAdjPoints);
                legAdjPoints = new List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>();
            }


            List<List<RouteLegWithCost>> AllAdjPointsWithCostAndMode = new List<List<RouteLegWithCost>>();
            for (int i = 0; i < allLegsAdjPoints.Count; i++)
            {
                var legWithCostAndMode = new List<RouteLegWithCost>();
                for (int j = 0; j < allLegsAdjPoints[i].Count; j++)
                {
                    if (mode == "Transit")
                    {
                        legWithCostAndMode.Add(new RouteLegWithCost(allLegsAdjPoints[i][j], 2, "Transit"));
                    }
                    else if (mode == "Rideshare")
                    {
                        legWithCostAndMode.Add(new RouteLegWithCost(allLegsAdjPoints[i][j], 10, "Rideshare"));
                    }
                }
                AllAdjPointsWithCostAndMode.Add(legWithCostAndMode);
                legWithCostAndMode = new List<RouteLegWithCost>();
            }

            return AllAdjPointsWithCostAndMode;
        }
    }
}