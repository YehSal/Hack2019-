using System;
using System.Collections.Generic;
using System.Linq;

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
    }

}
