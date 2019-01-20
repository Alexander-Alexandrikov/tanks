using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public interface ITank
    {
        Image TankImage { get; }
        int X { get; }
        int Y { get; }
        Direction TankDirection { get; }

        void Run();        
    }
}
