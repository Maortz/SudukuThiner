using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    class SudukuThiner
    {
        private Table suduku = new Table();
        private List<Cell> to_removed_cells;
        private int[,] matrix;

        public void Read(Cell[,] matrix)
        {
            suduku.Read(matrix);
        }

        public void Run()
        {
            if (!Suduku.IsSolvable(suduku))
                return;
            to_removed_cells = suduku.GetTableNumbersAsList();
            int DEGREES = to_removed_cells.Count;
            matrix = new int[DEGREES, DEGREES];
            InitMatrix(DEGREES);
            CalcMatrix(DEGREES);
            // Do something with this matrix...
            
        }

        private void CalcMatrix(int DEGREES)
        {
            List<List<int>> comb;
            for (int d = 1; d <= DEGREES; d++)
            {
                comb = GetCombinationbyIndex(to_removed_cells, d);
                foreach (var c in comb)
                {
                    if (CanErase(c))
                    {
                        foreach (var index in c)
                        {
                            matrix[d - 1, index]++;
                        }
                    }
                }
                if (!IsAnyDiffBetweenDegrees(d))
                    break;
            }
        }

        private void InitMatrix(int len)
        {
            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    matrix[i, j] = 0;
        }

        private List<List<int>> GetCombinationbyIndex(List<Cell> to_removed_cells, int d)
        {
            throw new NotImplementedException();
        }

        private bool CanErase(List<int> c)
        {
            if (!IsAllDiffBetweenDegrees(c))
                return false;
            // Checking actualy if it would be solved.
            throw new NotImplementedException();
        }

        private bool IsAllDiffBetweenDegrees(List<int> c)
        {
            foreach (var index in c)
            {
                if (GetDiff(index) == 0)
                    return false;
            }
            return true;
        }

        private bool IsAnyDiffBetweenDegrees(int current_degree)
        {
            // In the first degree, checks that not every one is 0.
            // Is there any difference between the *previous* degrees.

            throw new NotImplementedException();
        }

        private int GetDiff(int index)
        {
            // For the first degree, dont check.
            // For the second degree, checking if the first is 0.
            // For the others degrees, checking if the difference between 2 degrees before this level is 0.

            throw new NotImplementedException();
        }


    }
}
