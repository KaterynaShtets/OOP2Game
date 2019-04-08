using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace labOop2
{
    public class Player : Element
    {
       
        public int Lives { get; set; }
        public Player(int x, int y, int lives) : base(x, y, "I", ConsoleColor.Blue)
        {
            Lives = lives;
        }
        public void MinusLives()
        {
            --Lives;
        }
        public void NullLives()
        {
            Lives = 0;
        }
        public void PlusLives()
        {
            ++Lives;
        }
        public void Move(int x, int y)
        {
            X += x;
            Y += y;          
        }
        public void Teleport(int x, int y)
        {
            X = x;
            Y = y;           
        }
    }
}
