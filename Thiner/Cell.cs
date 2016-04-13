using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiner
{
    class Cell
    {
        public int Num { get; set; }
        public bool IsReadonly { get; set; }
        public readonly Position Pos;

        public Cell(int x, int y)
        {
            Num = 0;
            IsReadonly = false;
            Pos = new Position { X = x, Y = y };
        }

        public bool Advance()
        {
            if (IsReadonly)
                return false;
            Num++;
            if(Num > 9)
            {
                Num = 0;
                return false;
            }
            return true;
        }
    }
}
