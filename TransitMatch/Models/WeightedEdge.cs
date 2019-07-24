using System;
using QuickGraph;

namespace TransitMatch.Models
{
    public class WeightedEdge<TVertex> : TransportEdge<TVertex>
    {
        public double EdgeWeight { get; }
        public WeightedEdge(TVertex source, TVertex target, NavigationMode mode, double weight) : base(source, target, mode)
        {
            EdgeWeight = weight;
            EdgeWeight = weight;
            EdgeWeight = weight;
        }
    }

}
