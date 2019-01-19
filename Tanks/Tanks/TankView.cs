using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanks
{
    public class TankView : IEntitiesView
    {
        public Image Img { get;} = Properties.Resources.EnemyTank;
    }
}
