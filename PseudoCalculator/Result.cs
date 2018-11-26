using System;
using System.Collections.Generic;
using System.Text;

namespace PseudoCalculator
{
    public class Result
    {
        public double[,] Matrix { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
    }
}
