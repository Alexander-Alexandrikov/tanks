using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public interface ITankView
    {
        Image ImgUp { get; } 
        Image ImgRight { get; } 
        Image ImgLeft { get; } 
        Image ImgDown { get; } 
    }
}
