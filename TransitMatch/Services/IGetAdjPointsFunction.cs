using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design.Internal;
using TransitMatch.Models;
using AzureMapsToolkit.Common;

namespace TransitMatch.Services
{
    public interface IGetAdjPointsFunction
    {
        List<List<RouteLegWithCost>> GetAdjPoints(RouteDirectionsResult routeDirectionsResult, String mode);
    }
}
