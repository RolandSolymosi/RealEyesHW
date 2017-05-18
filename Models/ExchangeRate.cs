using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEyesHomework.Models
{
    public class ExchangeRate
    {
        public DateTimeOffset Date { get; set; }
        public decimal Rate { get; set; }
    }
}
