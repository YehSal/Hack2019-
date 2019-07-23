using System.ComponentModel.DataAnnotations;

namespace TransitMatch.Models
{
    public class NavigationRequestParam
    {
        public NavigationRequestParam(NavigationPoint startPoint, NavigationPoint endPoint, OptimizationParam optimizer)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Optimizer = optimizer;
        }

        [Required]
        public NavigationPoint StartPoint { get; set; }
        [Required]
        public NavigationPoint EndPoint { get; set; }
        [Range(0, 10)]
        public OptimizationParam Optimizer { get; set; }
    }
}
