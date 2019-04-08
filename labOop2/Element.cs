using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace labOop2
{
    
    public abstract class Element
    {
        [JsonProperty]
        public int X { get; set; }
        [JsonProperty]
        public int Y { get; set; }
        [JsonProperty]
        public string Icon { get; set; }
        [JsonProperty]
        public ConsoleColor Color { get; set; }
        public Element() { }
        public Element(int x, int y, string i,ConsoleColor color)
        {
            X = x;
            Y = y;
            Icon = i;
            Color = color;
        }
    }
}
