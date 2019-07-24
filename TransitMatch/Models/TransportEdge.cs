using System;
using QuickGraph;

namespace TransitMatch.Models
{
    public class TransportEdge<TVertex> : Edge<TVertex>
    {
        public NavigationMode NavigationMode { get; }
        public TransportEdge(TVertex source, TVertex target, NavigationMode mode) : base(source, target)
        {
            NavigationMode = mode;
        }
    }

}
