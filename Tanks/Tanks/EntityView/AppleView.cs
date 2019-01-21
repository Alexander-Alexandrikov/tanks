using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class AppleView : IEntitiesView
    {
        public Image Img { get; } = Properties.Resources.apple;
    }
}
