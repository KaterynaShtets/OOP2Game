using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labOop2
{
    public class Enemy : Element
    {
        public Enemy(int x, int y) : base(x, y, "O",ConsoleColor.Magenta)
        {
        }

    }
}
