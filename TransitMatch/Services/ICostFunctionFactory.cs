using System;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface ICostFunctionFactory
    {
        ICostFunction GetCostFunctionByType(NavigationMode mode);
    }
}
