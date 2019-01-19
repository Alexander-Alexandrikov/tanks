using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Kolobok : ITank
    {
        KolobokView kolobokView = new KolobokView();
        
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction TankDirection { get; private set; }
        public Image TankImage { get; private set; }

        public Kolobok()
        {
            TankDirection = Direction.Up;
            X = 0;
            Y = 0;
            TankImage = kolobokView.Img;
        }

        public void Run()
        {
            MoveToDirection();
        }

        public void Run(Direction direction)
        {
            TankDirection = direction;
            MoveToDirection();
        }

        private void MoveToDirection()
        {
            switch (TankDirection)
            {
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
            }
        }
    }
}
