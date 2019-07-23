namespace TransitMatch.Models
{
    public class Cost
    {
        public Cost() : this(0)
        {
        }

        public Cost(double timeCost, double monetaryCost = 0)
        {
            MonetaryCost = monetaryCost;
            this.TimeCost = timeCost;
        }

        private double TimeCost { get; }
        private double MonetaryCost { get; }

        public double ApplyCost(OptimizationParam optimizer)
        {
            return optimizer.OptimizerValue * TimeCost + (OptimizationParam.MaxValue - optimizer.OptimizerValue) * MonetaryCost;
        }
    }
}