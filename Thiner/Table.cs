using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    class Table
    {
        private Cell[,] table = new Cell[9,9];
        // 0 x x x x
        // y
        // y  Table[x,y]
        // y

        private static void InitTable(Cell[,] table)
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    table[x, y] = new Cell(x, y);
        }

        public List<Cell> GetTableNumbersAsList()
        {
            List<Cell> array = new List<Cell>();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if(table[i,j].Num != 0)
                        array.Add(table[i, j]);
            return array;
        }

        public void ClearCell(Position pos)
        {
            table[pos.X, pos.Y].Clear();
        }

        public void ClearEditable()
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    if (!table[x, y].IsReadonly)
                        table[x, y].ClearNumber();
        }

        public void ClearAll()
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    table[x, y].Clear();
        }

        public void Read(Cell[] cell_array)
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

        public void Read(Cell[,] cell_matrix)
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    table[x, y].Num = cell_matrix[x, y].Num;
                    table[x, y].IsReadonly = cell_matrix[x, y].IsReadonly;
                }
        }

        public Table()
        {
            InitTable(table);
        }

        public Cell GetPrev(Cell cell)
        {
            if (cell.Pos.X > 0)
                return table[cell.Pos.X - 1, cell.Pos.Y];
            if (cell.Pos.Y > 0)
                return table[8, cell.Pos.Y - 1];
            throw new Exception("Underflow");
        }

        public Cell GetNext(Cell cell)
        {
            if (cell.Pos.X < 8)
                return table[cell.Pos.X + 1, cell.Pos.Y];
            if (cell.Pos.Y < 8)
                return table[0, cell.Pos.Y + 1];
            throw new Exception("Overflow");
        }

        private Cell GetLeftCorner(Cell cell)
        {
            int x = (cell.Pos.X / 3 ) * 3;
            int y = (cell.Pos.Y / 3) * 3;
            return table[x, y];
        }

        public bool IsValid(Cell cell)
        {
            for (int i = 0; i < 9; i++)
            {
                if (cell.Pos.X != i)
                    if (cell.Num == table[i, cell.Pos.Y].Num)
                        return false;
                if (cell.Pos.Y != i)
                    if (cell.Num == table[cell.Pos.X, i].Num)
                        return false;
            }
            Cell leftCorner = GetLeftCorner(cell);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != cell.Pos.X || j!= cell.Pos.Y)
                        if (table[leftCorner.Pos.X + i, leftCorner.Pos.Y + j].Num == cell.Num)
                            return false;
                }
            }
            return true;
        }

        public void Print(Action<Cell> PrintOneCell)
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    PrintOneCell(table[x,y]);
        }

        public void CopyTo(ref Table another)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    another.table[i, j].Num = table[i, j].Num;
                    another.table[i, j].IsReadonly = table[i, j].IsReadonly;
                }
        }

        public Cell GetCellInPos(Position pos)
        {
            return table[pos.X, pos.Y];
        }

        public bool CompareTo(Table another)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (table[i, j].Num != another.table[i, j].Num)
                        return false;
            return true;
        }

        public bool CompareTo(Cell[,] another)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (table[i, j].Num != another[i, j].Num)
                        return false;
            return true;
        }
    }
}
