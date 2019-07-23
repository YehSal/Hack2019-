using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IRouteSegmentationService
    {
        Task<List<Tuple<NavigationPoint, NavigationPoint>>> GetSegments(NavigationPoint startPoint,
            NavigationPoint endPoint);
    }
}