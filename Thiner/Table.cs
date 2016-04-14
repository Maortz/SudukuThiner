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
        // 0 x x x x
        // y
        // y  Table[x,y]
        // y

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
            if (pos.X > 0)
                return table[pos.X - 1, pos.Y];
            if (pos.Y > 0)
                return table[8, pos.Y - 1];
            throw new Exception("Cannot Get Prev");
        }

        public Cell GetNext(Position pos)
        {
            if (pos.X < 8)
                return table[pos.X + 1, pos.Y];
            if (pos.Y < 8)
                return table[0, pos.Y + 1];
            throw new Exception("Cannot Get Next");
        }

        public bool IsValid(Cell cell)
        {
            throw new Exception("Unimpl");
        }
    }
}
