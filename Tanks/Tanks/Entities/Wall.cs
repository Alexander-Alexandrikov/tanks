using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Wall
    {
        public int XLeft { get; }
        public int XRight { get; }
        public int YUp { get; }
        public int YDown { get; }

        public Wall(int xLeft, int xRight, int yUp, int yDown)
        {
            XLeft = xLeft;
            XRight = xRight;
            YUp = yUp;
            YDown = yDown;
        }

    }
}
