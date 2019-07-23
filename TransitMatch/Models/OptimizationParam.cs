using System;
using System.ComponentModel.DataAnnotations;

namespace TransitMatch.Models
{
    public class OptimizationParam
    {
        public long OptimizerValue => Math.Clamp(_internalValue, 0, 10);

        [Required]
        private readonly long _internalValue;

        public OptimizationParam(long internalValue)
        {
            this._internalValue = internalValue;
        }
    }
}