//using System;
//using System.Threading.Tasks;
//using TransitMatch.Models;
//using TransitMatch.Services;
//using System.Collections.Generic;
//using AzureMapsToolkit.Common;
//using System.Linq;

//namespace TransitMatch.Impl.CostFunctions
//{
//    public class HelperCostFunction : IGetAdjPointsFunction
//    {
//        public List<List<RoutingSegmentWithCost>> GetAdjPoints(RouteDirectionsResult routeDirectionsResult, String mode)
//        {
//            // Big array of legs
//            // Small array of one leg
//            // Small array has an object that has two members: pair of points and cost

//            // Array of arrays (per leg) that has tuples of adjacent points
//            var allLegsAdjPoints = new List<List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>>();
//            for (int i = 1; i < routeDirectionsResult.Legs.Length; i++)
//            {
//                List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>> legAdjPoints
//                    = new List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>();
//                for (int j = 1; j < routeDirectionsResult.Legs[i].Points.Length; i += 2)
//                {
//                    if (j > routeDirectionsResult.Legs[i].Points.Length) break;

//                    legAdjPoints.Add(
//                        Tuple.Create(
//                            routeDirectionsResult.Legs[i].Points[j],
//                            routeDirectionsResult.Legs[i].Points[j + 2]
//                        )
//                    );
//                }
//                allLegsAdjPoints.Add(legAdjPoints);
//                legAdjPoints = new List<Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate>>();
//            }


//            List<List<RoutingSegmentWithCost>> AllAdjPointsWithCostAndMode = new List<List<RoutingSegmentWithCost>>();
//            for (int i = 0; i < allLegsAdjPoints.Count; i++)
//            {
//                var legWithCostAndMode = new List<RoutingSegmentWithCost>();
//                for (int j = 0; j < allLegsAdjPoints[i].Count; j++)
//                {
//                    if (mode == "Transit")
//                    {
//                        legWithCostAndMode.Add(new RoutingSegmentWithCost(allLegsAdjPoints[i][j], 2, "Transit"));
//                    }
//                    else if (mode == "Rideshare")
//                    {
//                        legWithCostAndMode.Add(new RoutingSegmentWithCost(allLegsAdjPoints[i][j], 10, "Rideshare"));
//                    }
//                }
//                AllAdjPointsWithCostAndMode.Add(legWithCostAndMode);
//                legWithCostAndMode = new List<RoutingSegmentWithCost>();
//            }

//            return AllAdjPointsWithCostAndMode;
//        }
//    }
//}