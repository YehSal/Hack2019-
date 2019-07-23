using System;

namespace TransitMatch.Models
{

    public class RoutingSegment
    {
        public RoutingSegment(NavigationPoint segmentStart, NavigationPoint segmentEnd, NavigationMode segmentNavigationMode)
        {
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
            SegmentNavigationMode = segmentNavigationMode;
        }

        public NavigationPoint SegmentStart { get; }
        public NavigationPoint SegmentEnd { get; }
        public NavigationMode SegmentNavigationMode { get; }

    }
}
