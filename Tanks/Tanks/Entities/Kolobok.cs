using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Kolobok : ITank, IGameObject, IRun
    {
        KolobokView kolobokView = new KolobokView();
        
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction TankDirection { get; private set; }
        public Image TankImage { get; private set; }

        public Kolobok()
        {
            TankDirection = Direction.Up;
            X = 110;
            Y = 200;
            TankImage = kolobokView.ImgUp;
        }

        public void SetDirection(Direction newDirection)
        {
            TankDirection = newDirection;
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
                    TankImage = kolobokView.ImgLeft;
                    break;
                case Direction.Right:
                    X++;
                    TankImage = kolobokView.ImgRight;
                    break;
                case Direction.Up:
                    Y--;
                    TankImage = kolobokView.ImgUp;
                    break;
                case Direction.Down:
                    Y++;
                    TankImage = kolobokView.ImgDown;
                    break;
            }
        }

        public void TurnAround()
        {
            switch (TankDirection)
            {
                case Direction.Left:
                    TankDirection = Direction.Right;
                    break;
                case Direction.Right:
                    TankDirection = Direction.Left;
                    break;
                case Direction.Up:
                    TankDirection = Direction.Down;
                    break;
                case Direction.Down:
                    TankDirection = Direction.Up;
                    break;
                default:
                    break;
            }
        }

    }
}
