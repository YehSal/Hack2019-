using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface INavigationRoutingService
    {
        Task<ActionResult<List<RoutingSegmentResult>>> GetOptimalRoute(NavigationRequestParam navigationParams);
    }
}
