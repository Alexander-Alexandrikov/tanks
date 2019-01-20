using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanks
{
    public class TankView 
    {
        public Image ImgUp { get; } = Properties.Resources.EnemyTank;
        public Image ImgRight { get; } = Properties.Resources.EnemyTankRight;
        public Image ImgLeft { get; } = Properties.Resources.EnemyTankLeft;
        public Image ImgDown { get; } = Properties.Resources.EnemyTankDown;
    }
}
