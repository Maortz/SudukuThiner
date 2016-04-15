using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    class SudukuThiner
    {
        private Table suduku;


        public void Read(Cell[,] matrix)
        {
            suduku.Read(matrix);
        }

        public void Run()
        {
            bool success = suduku.Solve();
            if (!success)
                throw new Exception("Error");

        }

    }
}
