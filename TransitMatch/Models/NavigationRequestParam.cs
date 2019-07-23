using System.ComponentModel.DataAnnotations;

namespace TransitMatch.Models
{
    public class NavigationRequestParam
    {
        public NavigationRequestParam(NavigationPoint startPoint, NavigationPoint endPoint, long optimizer)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Optimizer = new OptimizationParam(optimizer);
        }

        [Required]
        public NavigationPoint StartPoint { get; set; }
        [Required]
        public NavigationPoint EndPoint { get; set; }
        [Required]
        public OptimizationParam Optimizer { get; set; }
    }
}
