using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoConnector
{
    public class Connector
    {
        public double[,] GetRandomMatrix(int dimension)
        {
            return GenerateRandomMatrix(dimension);
        }

        public double[,] GetRandomMatrix(int row, int column)
        {
            return GenerateRandomMatrix(row, column);
        }

        public double[,] GetPseudoInverse(double[,] matrix)
        {
            return CalculatePseudoInverse(matrix);
        }

        public double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            return MatrixMultiplication(matrix1, matrix2);
        }
    }
}
