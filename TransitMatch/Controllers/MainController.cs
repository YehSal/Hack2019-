using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INavigationRoutingService _navigationRoutingService;
        public MainController(INavigationRoutingService navigationRoutingService)
        {
            _navigationRoutingService = navigationRoutingService;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public Task<ActionResult<List<RoutingSegment>>> Post(NavigationRequestParam requestParam)
        {
            return _navigationRoutingService.GetOptimalRoute(requestParam);
        }
    }
}
