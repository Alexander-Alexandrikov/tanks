using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public static class Collide
    {
        public static bool Collides(int x, int y, int r, int b, int x2, int y2, int r2, int b2)
        {
            return !(r <= x2 || x > r2 ||
                     b <= y2 || y > b2);
        }

        public static bool BoxCollides(int posX, int posY, int sizeX, int sizeY, int pos2X, int pos2Y, int size2X, int size2Y)
        {
            return Collides(posX, posY,
                            posX + sizeX, posY + sizeY,
                            pos2X, pos2Y,
                            pos2X + size2X, pos2Y + size2Y);
        }
    }
}
