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

        private void InitArray()
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    table[x, y] = new Cell(x, y);
        }

        public void Clear()
        {
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    if (!table[x, y].IsReadonly)
                        table[x, y].Num = 0;
        }

        public void Read(Cell[] cell_array)
        {
            InitArray();
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

        public Table()
        {
            InitArray();
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

        public bool Solve()
        {
            Clear();
            //
            StepState state;
            // WARRNING: Possible Exception, but its unlikly to get whole solved suduku...
            Cell traveler = GetFirstEditable();
            do
            {
                state = Step(ref traveler);
            } while (state == StepState.Running);
            if (state == StepState.Finish)
                return true;
            return false;
        }

        private Cell GetFirstEditable()
        {
            Cell traveler = table[0, 0];
            while (traveler.IsReadonly)
                traveler = GetNext(traveler);
            return traveler;
        }

        private StepState Step(ref Cell traveler)
        {
            bool ok = false;
            while (traveler.Advance())
            {
                if (IsValid(traveler))
                {
                    ok = true;
                    break;
                }
            }
            try
            {
                if (ok)
                {
                    do
                    {
                        traveler = GetNext(traveler);
                    } while (traveler.IsReadonly);
                }
                else
                    do
                    {
                        traveler = GetPrev(traveler);
                    } while (traveler.IsReadonly);
            }
            catch(Exception exc)
            {
                switch (exc.Message)
                {
                    case "Underflow":
                        return StepState.Unsolvable;
                    case "Oferflow":
                        return StepState.Finish;
                    default:
                        throw new Exception("Unexpected exception");
                }
            }
            return StepState.Running;
        }

        enum StepState
        {
            Finish, Unsolvable, Running 
        }

    }
}
