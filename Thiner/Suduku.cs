using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    public class Suduku
    {
        public static bool IsSolvable(Table table)
        {
            Table t1 = new Table();
            Table t2 = new Table();
            table.CopyTo(ref t1);
            table.CopyTo(ref t2);
            if (!Solve(t1, Direction.FromBegining))
                return false;
            Solve(t2, Direction.FromLast);
            return t1.CompareTo(t2);
        }

        public static bool Solve(Table table, Direction dir)
        {
            table.ClearEditable();
            //
            StepState state;
            Cell traveler;
            // WARRNING: Possible Exception, but its unlikly to get whole solved suduku...
            if (dir == Direction.FromBegining)
                traveler = GetFirstEditable(table);
            else
                traveler = GetLastEditable(table);
            do
            {
                state = Step(ref traveler, table, dir);
            } while (state == StepState.Running);
            if (state == StepState.Finish)
                return true;
            return false;
        }

        private static StepState Step(ref Cell traveler, Table table, Direction dir)
        {
            bool ok = false;
            while (traveler.Advance())
            {
                if (table.IsValid(traveler))
                {
                    ok = true;
                    break;
                }
            }
            try
            {
                if ((ok && dir == Direction.FromBegining) || (!ok && dir == Direction.FromLast))
                {
                    do
                    {
                        traveler = table.GetNext(traveler);
                    } while (traveler.IsReadonly);
                }
                else
                    do
                    {
                        traveler = table.GetPrev(traveler);
                    } while (traveler.IsReadonly);
            }
            catch (Exception exc)
            {
                switch (exc.Message)
                {
                    case "Underflow":
                        if (dir == Direction.FromBegining)
                            return StepState.Unsolvable;
                        else
                            return StepState.Finish;
                    case "Oferflow":
                        if (dir == Direction.FromBegining)
                            return StepState.Finish;
                        else
                            return StepState.Unsolvable;
                    default:
                        throw new Exception("Unexpected exception");
                }
            }
            return StepState.Running;
        }

        private static Cell GetFirstEditable(Table table)
        {
            Cell traveler = table.GetCellInPos(new Position { X = 0, Y = 0 });
            while (traveler.IsReadonly)
                traveler = table.GetNext(traveler);
            return traveler;
        }

        private static Cell GetLastEditable(Table table)
        {
            Cell traveler = table.GetCellInPos(new Position { X = 8, Y = 8 });
            while (traveler.IsReadonly)
                traveler = table.GetPrev(traveler);
            return traveler;
        }

        enum StepState
        {
            Finish, Unsolvable, Running
        }

        public enum Direction
        {
            FromBegining, FromLast
        }
    }
}
