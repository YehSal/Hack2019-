using System;
using System.Collections.Generic;
using System.Linq;
using TransitMatch.Models;

namespace TransitMatch.Common
{
    public static class EnumUtils
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static NavigationMode parseFromString(string mode)
        {
            if (!Enum.TryParse(typeof(NavigationMode), mode, true, out object resultMode))
            {
                switch (mode)
                {
                    case "car":
                    case "Car":
                        resultMode = NavigationMode.Rideshare;
                        break;
                    case "Bus":
                    case "bus":
                        resultMode = NavigationMode.Transit;
                        break;
                    default:
                        resultMode = NavigationMode.Walk;
                        break;
                }
            }

            return (NavigationMode) resultMode;
        }
    }

}
