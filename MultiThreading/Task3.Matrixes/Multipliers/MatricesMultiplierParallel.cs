﻿using System.Threading.Tasks;
using MultiThreading.Task3.MatrixMultiplier.Matrices;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            Parallel.For(0, m1.RowCount, (row, loop) =>
            {
                for (byte j = 0; j < m2.ColCount; j++)
                {
                    long sum = 0;
                    for (byte k = 0; k < m1.ColCount; k++)
                    {
                        sum += m1.GetElement(row, k) * m2.GetElement(k, j);
                    }

                    resultMatrix.SetElement(row, j, sum);
                }

            });
            return resultMatrix;
        }
    }
}
