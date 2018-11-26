using System;
using System.Collections.Generic;
using System.Text;

namespace PseudoInverseLib
{
    public class Result<T>
    {
        public T Element { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }

        public Result()
        {
            Error = "";
            Success = false;
        }

        public Result(T element, string error, bool success)
        {
            Element = element;
            Error = error;
            Success = success;
        }
    }
}
