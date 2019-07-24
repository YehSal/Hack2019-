using System;
using System.Collections.Generic;

namespace TransitMatch.Models
{
    public class AzureResponseContainer<TResult>
    {
        public List<TResult> results { get; set; }
    }

}
