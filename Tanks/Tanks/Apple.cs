using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Apple
    {
        AppleView appleView = new AppleView();
        public int X { get; }
        public int Y { get; }
        public Image Image { get; }
        public Apple(int x, int y)
        {
            X = x;
            Y = y;
            Image = appleView.Img;
        }
    }
}
