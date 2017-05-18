using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEyesHomework.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public IReadOnlyList<ExchangeRate> ExchangeRates { get; set; }
    }
}
