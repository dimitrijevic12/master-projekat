using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.DTOs
{
    public class PerDiem
    {
        public string UniquePersonalRegistrationNumber { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
