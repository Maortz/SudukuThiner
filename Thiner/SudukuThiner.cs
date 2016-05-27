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
        private int DEGREES;

        public SudukuThiner(Cell[,] matrix)
        {
            Read(matrix);
        }

        public void Read(Cell[,] matrix)
        {
            suduku.Read(matrix);
        }

        public void Print()
        {
            Action<Cell> p = (Cell c) =>
                {
                    if (c.Num == 0)
                        Console.WriteLine(" ");
                    else
                        Console.WriteLine(c.Num);
                };
            suduku.Print(p);
        }

        public void Run()
        {
            if (!Suduku.IsSolvable(suduku))
                return;
            to_removed_cells = suduku.GetTableNumbersAsList();
            DEGREES = to_removed_cells.Count;
            matrix = new int[DEGREES, DEGREES];
            InitMatrix();
            int last_row = CalcMatrix();
            RemoveCells(last_row);
        }

        private void RemoveCells(int last_row)
        {
            List<int> to_remove_indexes = GetCellsToRemove(last_row);
            foreach (var index in to_remove_indexes)
            {
                to_removed_cells.RemoveAt(index);
            }
            foreach (var cell in to_removed_cells)
            {
                suduku.ClearCell(cell.Pos);
            }
        }

        private List<int> GetCellsToRemove(int last_row)
        {
            // What remove from the matrix
            throw new NotImplementedException();
        }

        private int CalcMatrix()
        {
            List<List<int>> comb;
            int d;
            for (d = 0; d < DEGREES; d++)
            {
                comb = GetCombinationbyIndex(d + 1);
                foreach (var c in comb)
                    if (CanErase(c, d))
                        foreach (var index in c)
                            matrix[d, index]++;
                if (!IsAnyDiffBetweenDegrees(d))
                {
                    d++;
                    break;
                }
            }
            return d - 1;
        }

        private void InitMatrix()
        {
            for (int i = 0; i < DEGREES; i++)
                for (int j = 0; j < DEGREES; j++)
                    matrix[i, j] = 0;
        }

        private List<List<int>> GetCombinationbyIndex(int deg)
        {
            List<int> indexes = Enumerable.Range(0, DEGREES).ToList();
            var combs = new Combinatorics.Collections.Combinations<int>(indexes, deg);
            List<List<int>> result = new List<List<int>>();
            foreach (var item in combs)
                result.Add(item.ToList());
            return result;
        }

        private bool CanErase(List<int> comb, int deg)
        {
            if (!IsAllDiffBetweenDegrees(comb, deg))
                return false;
            // Checking actualy if it would be solved.
            Table copy = new Table();
            suduku.CopyTo(ref copy);
            foreach (var index in comb)
                copy.ClearCell(to_removed_cells[index].Pos);
            return Suduku.IsSolvable(copy);
        }

        private bool IsAllDiffBetweenDegrees(List<int> c, int deg)
        {
            foreach (var index in c)
                if (GetDiff(index, deg) == 0)
                    return false;
            return true;
        }

        private bool IsAnyDiffBetweenDegrees(int current_degree)
        {
            // In the first degree, checks that not every one is 0.
            // Is there any difference between the *previous* degrees.
            for (int i = 0; i < DEGREES; i++)
                if (matrix[current_degree, i] > 0)
                    return true;
            return false;
        }

        private int GetDiff(int index, int deg)
        {
            if (deg == 0)
                return 1;
            else
                return matrix[deg - 1, index];
        }


    }
}
