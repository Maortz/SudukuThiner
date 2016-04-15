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
        private Cell[,] solved = new Cell[9,9];
        private List<Cell> removed_cells = new List<Cell>();

        public void Read(Cell[,] matrix)
        {
            suduku.Read(matrix);
        }

        public void Run()
        {
            GetSolvedSuduku();

        }

        private void GetSolvedSuduku()
        {
            if (!suduku.Solve())
                throw new Exception("Error");
            suduku.CopyTo(ref solved);
        }

    }
}
