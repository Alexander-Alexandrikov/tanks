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
        WallView wallView = new WallView();
        public Image Image { get; }
        public int XLeft { get; }
        public int XRight { get; }
        public int YUp { get; }
        public int YDown { get; }

        public Wall(int xLeft, int xRight, int yUp, int yDown)
        {
            Image = wallView.Img;
            XLeft = xLeft;
            XRight = xRight;
            YUp = yUp;
            YDown = yDown;
        }

    }
}
