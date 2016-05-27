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
                         {},
                         {},
                         {},
                         {},
                         {},
                         {},
                         {},
                         {},
                         {}
                         };
            var a = new Thiner.SudukuThiner(mat);
            a.Print();
            a.Run();
            a.Print();
        }
    }
}
