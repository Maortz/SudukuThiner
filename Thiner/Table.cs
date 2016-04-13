using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    class Table
    {
        Cell[,] table = new Cell[9,9];
        //

        public Table(Cell[] cell_array)
        {
            int count = 0;
            int row = 0, col = 0;
            while (count < cell_array.Length)
            {
                table[row, col] = cell_array[count];
                count++;
                row++;
                col++;
                if (col == 9)
                {
                    col = 0;
                    row++;
                    if (row == 9)
                        break;
                }
            }
        }

        public Cell GetPrev(Position pos)
        {
            throw new Exception("Unimpl");
        }

        public Cell GetNext(Position pos)
        {
            throw new Exception("Unimpl");
        }

        public bool IsValid(Cell cell)
        {
            throw new Exception("Unimpl");
        }
    }
}
