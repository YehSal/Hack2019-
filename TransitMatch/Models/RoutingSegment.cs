using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TransitMatch.Models
{

    public class RoutingSegment
    {
        public RoutingSegment(NavigationPoint startPoint, NavigationPoint endPoint, NavigationMode segmentNavigationMode)
        {
            SegmentStart = startPoint;
            SegmentEnd = endPoint;
            SegmentNavigationMode = segmentNavigationMode;
        }

        [Required]
        public NavigationPoint SegmentStart { get; }
        public NavigationPoint SegmentEnd { get; }

        [Required]
        public NavigationMode SegmentNavigationMode { get; set; }

    }
}
