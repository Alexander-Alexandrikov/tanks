﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Apple : IGameObject
    {
        public int X { get; }
        public int Y { get; }
        public Point Point { get; }

        public Apple(int x, int y)
        {
            X = x;
            Y = y;
            Point = new Point(X, Y);
        }
    }
}
