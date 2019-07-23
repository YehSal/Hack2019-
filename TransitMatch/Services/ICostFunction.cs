using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design.Internal;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface ICostFunction
    {
        Task<double> GetCost(NavigationPoint start, NavigationPoint end, OptimizationParam optimizer);
    }
}
