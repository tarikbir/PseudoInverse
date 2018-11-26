using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoInverseLib
{
    public static class Interface
    {
        public static Result<double[,]> GetRandomMatrix(int dimension)
        {
            Result<double[,]> result = new Result<double[,]>();
            double[,] rmatrix = Calculator.GenerateRandomMatrix(dimension);
            if (rmatrix != null)
            {
                result.Success = true;
                result.Element = rmatrix;
                return result;
            }
            result.Error = "Unknown Error.";
            return result;
        }

        public static Result<double[,]> GetRandomMatrix(int row, int column)
        {
            Result<double[,]> result = new Result<double[,]>();
            double[,] rmatrix = Calculator.GenerateRandomMatrix(row, column);
            if (rmatrix != null)
            {
                result.Success = true;
                result.Element = rmatrix;
                return result;
            }
            result.Error = "Unknown Error.";
            return result;
        }

        public static Result<IEnumerable<double[,]>> GetPseudoInverse(double[,] matrix)
        {
            Result<IEnumerable<double[,]>> result = new Result<IEnumerable<double[,]>>();
            var rmatrix = Calculator.EnumeratePseudoInverse(matrix);
            if (rmatrix != null)
            {
                result.Success = true;
                result.Element = rmatrix;
                return result;
            }
            result.Error = "Unknown Error.";
            return result;
        }

        public static Result<double[,]> MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            Result<double[,]> result = new Result<double[,]>();
            double[,] rmatrix = Calculator.MatrixMultiplication(matrix1, matrix2);
            if (rmatrix != null)
            {
                result.Success = true;
                result.Element = rmatrix;
                return result;
            }
            result.Error = "Unknown Error.";
            return result;
        }

        public static Result<double> GetDeterminant(double[,] matrix)
        {
            Result<double> result = new Result<double>();
            double? r = Calculator.MatrixDeterminant(matrix);
            if (r != null)
            {
                result.Success = true;
                result.Element = r.Value;
                return result;
            }
            result.Error = "Unknown Error.";
            return null;
        }

        public static Result<double[,]> GetInverse(double[,] matrix)
        {
            Result<double[,]> result = new Result<double[,]>();
            double[,] rmatrix = Calculator.InverseSquareMatrix(matrix);
            if (rmatrix != null)
            {
                result.Success = true;
                result.Element = rmatrix;
                return result;
            }
            result.Error = "Unknown Error.";
            return result;
        }
    }
}
