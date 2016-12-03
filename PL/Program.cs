using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] mat = {
                         {0,3,0,6,0,0,7,0,0},
                         {0,0,0,9,0,0,6,0,2},
                         {2,0,0,1,8,7,0,0,0},
                         {3,8,6,0,0,1,9,4,0},
                         {0,0,9,0,0,0,2,0,0},
                         {0,7,2,5,0,0,1,6,8},
                         {0,0,0,4,2,9,0,0,6},
                         {1,0,4,0,0,6,0,0,0},
                         {0,0,3,0,0,8,0,9,0}
                         };
            var a = new Thiner.SudukuThiner(mat);
            a.Print();
            a.Run();
            a.Print();
        }
    }
}
