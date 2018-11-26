using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoInverseLib
{
    internal static class Calculator
    {
        internal static Result CalculatePseudoInverse(double[,] matrix)
        {
            return null;
        }

        static double[,] MatrixTranspose(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            double[,] matrixT = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrixT[j, i] = matrix[i, j];
                }
            }
            return matrixT;
        }

        static double[,] MatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(1)) // For every, M1(a,b), M2(m,n) => b = m must 
            {
                return null;
            }

            double[,] matrixResult = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrixResult.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(0); j++)
                {
                    for (int z = 0; z < matrix1.GetLength(1); z++)
                    {
                        matrixResult[i, j] += matrix1[i, z] * matrix2[z, j];
                    }
                }
            }
            return matrixResult;
        }

        static double[,] MatrixMinor(double[,] matrix, int i, int j)
        {
            if (matrix.GetLength(0) <= 1 || matrix.GetLength(1) <= 1) return null;
            double[,] tempMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            for (int a = 0; a < m; a++)
            {
                if (a == i) continue;
                for (int b = 0; b < n; b++)
                {
                    if (b == n) continue;
                    tempMatrix[i, j] = matrix[a, b];
                }
            }
            return tempMatrix;
        }

        static double MatrixDeterminant(double[,] matrix)
        {
            double determinant = 0.0;
            int k = matrix.GetLength(0);
            if (k == 1) return matrix[0, 0];
            for (int a = 0; a < k; a++)
            {
                if (matrix[0, a] == 0) continue; // if M[a,b] == 0, skip to reduce calculation time.
                double[,] matrixTemp = MatrixMinor(matrix, 0, a); // M[i,j]
                double cofactor = CofactorMultiplier(0, a) * MatrixDeterminant(matrixTemp); // C[i,j] = CM[i,j] . M[i,j]
                determinant += matrix[0, a] * cofactor; // det(A) += a[i,j] . C[i,j]
            }
            return determinant;
        }

        internal static double[,] GenerateRandomMatrix(int row, int column)
        {
            Random random = new Random();
            row = random.Next(0, 6);
            column = random.Next(0, 6);
            double[,] matrix = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = random.NextDouble() * 9;
                }
            }
            return matrix;
        }

        internal static double[,] GenerateRandomMatrix(int row)
        {
            Random random = new Random();
            row = random.Next(0, 6);
            int column = random.Next(0, 6);
            double[,] matrix = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = ((random.NextDouble() + 0.1) * 10) % 10;
                }
            }
            return matrix;
        }

        static int CofactorMultiplier(int row, int column)
        {
            return ((row + column) % 2 == 0) ? 1 : -1;
        }
    }
}
