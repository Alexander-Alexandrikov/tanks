﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class KolobokView 
    {
        public Image ImgUp { get; } = Properties.Resources.Kolobok;
        public Image ImgRight { get; } = Properties.Resources.KolobokRight;
        public Image ImgLeft { get; } = Properties.Resources.KolobokLeft;
        public Image ImgDown { get; } = Properties.Resources.KolobokDown;
    }
}