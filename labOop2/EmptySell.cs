using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labOop2
{
    public class EmptyCell : Element
    {
        public EmptyCell(int x, int y) : base(x, y, " ", ConsoleColor.White)
        {

        }
    }
}
