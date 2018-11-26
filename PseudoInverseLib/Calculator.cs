using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoInverseLib
{
    internal static class Calculator
    {
        internal static IEnumerable<double[,]> EnumeratePseudoInverse(double[,] matrix)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return matrix;
            }
        }

        internal static double[,] CalculatePseudoInverse(double[,] matrix)
        {
            return null;
        }

        internal static double[,] MatrixTranspose(double[,] matrix)
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

        internal static double[,] MatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            //GetLength(0) = row, GetLength(1) = column.
            if (matrix1.GetLength(1) != matrix2.GetLength(0) || matrix1 == null || matrix2 == null) // For every, M1(a,b), M2(m,n) => b = m must be true.
            {
                return null;
            }

            double[,] matrixResult = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix2.GetLength(0); k++)
                    {
                        matrixResult[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return matrixResult;
        }

        internal static double[,] MatrixScalarMultiplication(double[,] matrix, double scalar)
        {
            if (matrix == null) return null;
            int m = matrix.GetLength(0), n = matrix.GetLength(1);

            double[,] resultMatrix = new double[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultMatrix[i, j] = scalar * matrix[i, j];
                }
            }
            return resultMatrix;
        }

        internal static double[,] MatrixMinor(double[,] matrix, int i, int j)
        {
            if (matrix.GetLength(0) <= 1 || matrix.GetLength(1) <= 1) return null;
            double[,] tempMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            int _a = 0;
            for (int a = 0; a < m; a++)
            {
                if (a == i) continue;
                int _b = 0;
                for (int b = 0; b < n; b++)
                {
                    if (b == j) continue;
                    tempMatrix[_a, _b] = matrix[a, b];
                    _b++;
                }
                _a++;
            }
            return tempMatrix;
        }

        internal static double? MatrixDeterminant(double[,] matrix)
        {
            double determinant = 0.0;
            if (matrix == null) return null;
            if (matrix.GetLength(0) != matrix.GetLength(1)) return null;
            int k = matrix.GetLength(0);
            if (k == 1) return matrix[0, 0];
            for (int a = 0; a < k; a++)
            {
                if (matrix[0, a] == 0) continue; // if M[a,b] == 0, skip to reduce calculation time.
                double[,] matrixTemp = MatrixMinor(matrix, 0, a); // M[i,j]
                double cofactor = CofactorMultiplier(0, a) * MatrixDeterminant(matrixTemp).Value; // C[i,j] = CM[i,j] . M[i,j]
                determinant += matrix[0, a] * cofactor; // det(A) += a[i,j] . C[i,j]
            }
            return determinant;
        }

        static int CofactorMultiplier(int row, int column)
        {
            return ((row + column) % 2 == 0) ? 1 : -1;
        }

        internal static double[,] GenerateRandomMatrix(int row, int column)
        {
            Random random = new Random();
            double[,] matrix = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = Math.Round(((random.NextDouble() + 0.1) * 10) % 10, 1);
                }
            }
            return matrix;
        }

        internal static double[,] GenerateRandomMatrix(int row)
        {
            Random random = new Random();
            int column = row;
            double[,] matrix = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = Math.Round(((random.NextDouble() + 0.1) * 10) % 10, 1);
                }
            }
            return matrix;
        }

        internal static double[,] InverseSquareMatrix(double[,] matrix)
        {
            if (matrix == null) return null;
            if (matrix.GetLength(0) != matrix.GetLength(1)) return null;
            int k = matrix.GetLength(0);
            double determinant = MatrixDeterminant(matrix) ?? 0;
            if (determinant == 0.0) return null;
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            double[,] minorMatrix = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    minorMatrix[i, j] = CofactorMultiplier(i,j)*MatrixDeterminant(MatrixMinor(matrix, i, j)) ?? 0;
                }
            }
            double[,] inverse = MatrixScalarMultiplication(MatrixTranspose(minorMatrix), 1 / determinant);
            return inverse;
        }
    }
}
