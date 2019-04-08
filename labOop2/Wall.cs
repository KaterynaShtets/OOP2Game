using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labOop2
{
    [JsonObject]
    public class Wall : Element
    {
        public Wall(int x, int y)
        {
            X = x;
            Y = y;
            Icon = "#";
        }
    }
}
