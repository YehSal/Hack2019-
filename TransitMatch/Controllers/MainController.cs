using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INavigationRoutingService _navigationRoutingService;
        public MainController(INavigationRoutingService navigationRoutingService)
        {
            _navigationRoutingService = navigationRoutingService;
        }

        [HttpPost]
        public Task<ActionResult<List<RoutingSegmentResult>>> Post(NavigationRequestParam requestParam)
        {
            return _navigationRoutingService.GetOptimalRoute(requestParam);
        }
    }
}
