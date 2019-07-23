using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TransitMatch.Models
{
    public class OptimizationParam
    {
        public double OptimizerValue => Math.Clamp(_internalValue, 0, MaxValue);
        public static double MaxValue => 10;

        [Required]
        private readonly long _internalValue;

        [JsonConstructor]
        public OptimizationParam(long internalValue)
        {
            this._internalValue = internalValue;
        }
    }
}