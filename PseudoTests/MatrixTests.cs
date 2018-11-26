using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PseudoTests
{
    [TestClass]
    public class MatrixTests
    {
        double[,] MatrixSquare = new double[,] { { 1, 2, 3 }, { 5, 5, 5 }, { 9, 8, 7 } };
        double[,] Matrix3x2 = new double[,] { { 1, 2 }, { 5, 5 }, { 9, 8 } };
        double[,] Matrix2x3 = new double[,] { { 1, 2, 3 }, { 9, 8, 7 } };

        public void WriteMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                    if (j < matrix.GetLength(1) - 1) Console.Write("\t");
                }
                Console.Write("\n");
            }
        }

        public bool Between(double num, double lower, double upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        [TestMethod]
        public void Multiplication1()
        {
            double[,] resultMatrix = PseudoInverseLib.Interface.MultiplyMatrices(Matrix2x3, Matrix3x2).Element;

            double[,] assertMatrix = new double[,] { { 38, 36 }, { 112, 114 } };

            CollectionAssert.AreEqual(assertMatrix, resultMatrix);
        }

        [TestMethod]
        public void Multiplication2()
        {
            double[,] resultMatrix = PseudoInverseLib.Interface.MultiplyMatrices(MatrixSquare, Matrix3x2).Element;

            double[,] assertMatrix = new double[,] { { 38, 36 }, { 75, 75 }, { 112, 114 } };

            CollectionAssert.AreEqual(assertMatrix, resultMatrix);
        }

        [TestMethod]
        public void GenerateRandom1()
        {
            double[,] resultMatrix = PseudoInverseLib.Interface.GetRandomMatrix(2).Element;

            WriteMatrix(resultMatrix);

            Assert.IsTrue(Between(resultMatrix[0, 0],0,10,true));
        }

        [TestMethod]
        public void GenerateRandom2()
        {
            double[,] resultMatrix = PseudoInverseLib.Interface.GetRandomMatrix(3,5).Element;

            WriteMatrix(resultMatrix);

            Assert.IsTrue(Between(resultMatrix[0, 0], 0, 10, true));
        }

        [TestMethod]
        public void Determinant3x3()
        {
            double result = Math.Round(PseudoInverseLib.Interface.GetDeterminant(MatrixSquare).Element, 2);

            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void Inverse()
        {
            double[,] matrix = new double[,] { { 1, 2, 3 }, { 0, 4, 5 }, { 1, 0, 6 } };

            WriteMatrix(PseudoInverseLib.Interface.GetInverse(matrix).Element);
        }

        //[TestMethod]
        //public void CalculateInverse()
        //{

        //}
    }
}
