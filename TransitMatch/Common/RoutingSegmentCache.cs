using System;
using System.Collections.Generic;
using TransitMatch.Models;

namespace TransitMatch.Common
{
    public class RoutingSegmentCache
    {
        private readonly Dictionary<String, RoutingSegmentResult> _internalDictionary;

        public RoutingSegmentCache()
        {
            _internalDictionary = new Dictionary<string, RoutingSegmentResult>();
        }

        public RoutingSegmentResult GetByKey(RoutingSegment segment)
        {
            return _internalDictionary[GetKey(segment)];
        }

        public void Add(RoutingSegment segment, RoutingSegmentResult result)
        {
            _internalDictionary[GetKey(segment)] = result;
        }

        private string GetKey(RoutingSegment segment)
        {
            return $"{segment.SegmentStart.Latitude};{segment.SegmentStart.Longitude};{segment.SegmentEnd.Latitude};{segment.SegmentEnd.Longitude}";
        }
    }
}
