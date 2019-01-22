using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tanks
{
    public class Tank : ITank, IGameObject, IRun
    {
        private TankView tankView = new TankView();
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction TankDirection { get; private set; }

        public Image TankImage { get; private set; }

        public Tank(int x, int y)
        {
            TankDirection = Direction.Up;
            X = x;
            Y = y;
            TankImage = tankView.ImgUp;
        }

        public void Run()
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
                default:
                    break;
            }
        }

        public void Turn(Direction direction)
        {
            TankDirection = direction;
            PutImage();
        }


        public void TurnAround()
        {
            switch (TankDirection)
            {
                case Direction.Left:
                    TankDirection = Direction.Right;
                    PutImage();
                    break;
                case Direction.Right:
                    TankDirection = Direction.Left;
                    PutImage();
                    break;
                case Direction.Up:
                    TankDirection = Direction.Down;
                    PutImage();
                    break;
                case Direction.Down:
                    TankDirection = Direction.Up;
                    PutImage();
                    break;
                default:
                    break;
            }

        }

        private void PutImage()
        {
            switch (TankDirection)
            {
                case Direction.Left:
                    TankImage = tankView.ImgLeft;
                    break;
                case Direction.Right:
                    TankImage = tankView.ImgRight;
                    break;
                case Direction.Up:
                    TankImage = tankView.ImgUp;
                    break;
                case Direction.Down:
                    TankImage = tankView.ImgDown;
                    break;
                default:
                    break;
            }
        }
    }
}
