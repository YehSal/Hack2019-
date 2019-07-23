using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransitMatch.Models;

namespace TransitMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpPost]
        public ActionResult<List<RoutingSegment>> Post(NavigationRequestParam requestParam)
        {
            Console.WriteLine(requestParam.ToString());
            var testResponse = new List<RoutingSegment>
            {
                new RoutingSegment(new NavigationPoint(0, 0), new NavigationPoint(10, 10),
                    NavigationMode.Walk)
            };
            return testResponse;
        }
    }
}
