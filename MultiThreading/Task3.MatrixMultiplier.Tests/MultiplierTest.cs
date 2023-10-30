using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier.Tests
{
    /*
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
     *
     * Result: There are currently two tests that use two equal matrix, and before I used equal size random padding,
     * in both scenarios the parallel loop started working better with ~50, but sometimes the result may differ depending on usage system resource,
     * as I see, the parallel loop starts to execute better at 99% after capturing the 70-80 matrix size.
 */

    [TestClass]
    public class MultiplierTest
    {
        private MatricesMultiplierParallel _parallelMultiplier;
        private MatricesMultiplier _regularMultiplier;

        [TestInitialize]
        public void TestInitialize()
        {
            _parallelMultiplier = new MatricesMultiplierParallel();
            _regularMultiplier = new MatricesMultiplier();
        }

        [TestMethod]
        public void MultiplyMatrix3On3Test()
        {
            TestMatrix3On3(new MatricesMultiplier());
            TestMatrix3On3(new MatricesMultiplierParallel());
        }

        [TestMethod]
        public void CompareMatricesMultiplierConstructorsPerformance()
        {
            var regularMultiplierTime = MeasureMatrixMultiplier(TestMatrix3On3, _regularMultiplier);
            var parallelMultiplierTime = MeasureMatrixMultiplier(TestMatrix3On3, _parallelMultiplier);

            Console.WriteLine("Regular MatricesMultiplier Constructor Time: " + regularMultiplierTime + " ticks");
            Console.WriteLine("Parallel MatricesMultiplier Constructor Time: " + parallelMultiplierTime + " ticks");

            // Compare the execution times and check if the regular loop is faster
            Assert.IsTrue(regularMultiplierTime < parallelMultiplierTime);
        }

        [TestMethod]
        public void ParallelLoopFor_EfficiencyTest()
        {
            int matrixSize = 10;
            int matrixValue = 15;

            while (true)
            {
                IMatrix matrixA = FillMatrixWithStaticValues(matrixSize, matrixSize, matrixValue);
                IMatrix matrixB = FillMatrixWithStaticValues(matrixSize, matrixSize, matrixValue);

                var regularTicks = MeasureMatrixMultiplicationTime(_regularMultiplier, matrixA, matrixB);
                var parallelTicks = MeasureMatrixMultiplicationTime(_parallelMultiplier, matrixA, matrixB);

                Console.WriteLine($"Matrix Size: {matrixSize}x{matrixSize}");
                Console.WriteLine($"Regular Multiplication Time: {regularTicks} ticks");
                Console.WriteLine($"Parallel Multiplication Time: {parallelTicks} ticks");

                if (regularTicks >= parallelTicks)
                    break;

                matrixSize += 10;
            }
        }

        #region private methods

        private long MeasureMatrixMultiplier(Action<IMatricesMultiplier> action, IMatricesMultiplier multiplier)
        {
            var stopwatch = Stopwatch.StartNew();
            action(multiplier);
            stopwatch.Stop();

            return stopwatch.ElapsedTicks;
        }

        public static IMatrix FillMatrixWithStaticValues(int numRows, int numCols, long staticValue)
        {
            IMatrix matrix = new Matrix(numRows, numCols);
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    matrix.SetElement(row, col, staticValue);
                }
            }
            return matrix;
        }

        private long MeasureMatrixMultiplicationTime(IMatricesMultiplier multiplier, IMatrix matrixA, IMatrix matrixB)
        {
            var stopwatch = Stopwatch.StartNew();
            multiplier.Multiply(matrixA, matrixB);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }

        void TestMatrix3On3(IMatricesMultiplier matrixMultiplier)
        {
            if (matrixMultiplier == null)
            {
                throw new ArgumentNullException(nameof(matrixMultiplier));
            }

            var m1 = new Matrix(3, 3);
            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);

            var m2 = new Matrix(3, 3);
            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);

            var multiplied = matrixMultiplier.Multiply(m1, m2);
            Assert.AreEqual(448, multiplied.GetElement(0, 0));
            Assert.AreEqual(1826, multiplied.GetElement(0, 1));
            Assert.AreEqual(3052, multiplied.GetElement(0, 2));

            Assert.AreEqual(350, multiplied.GetElement(1, 0));
            Assert.AreEqual(712, multiplied.GetElement(1, 1));
            Assert.AreEqual(1127, multiplied.GetElement(1, 2));

            Assert.AreEqual(109, multiplied.GetElement(2, 0));
            Assert.AreEqual(213, multiplied.GetElement(2, 1));
            Assert.AreEqual(728, multiplied.GetElement(2, 2));
        }

        #endregion
    }
}
