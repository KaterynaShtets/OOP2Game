using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labOop2
{
    public class Teleport : Element
    {
        public string Symbol { get; }
        public Teleport(int x, int y) : base (x, y, "T",ConsoleColor.Gray)
        {
        }
    }
}
