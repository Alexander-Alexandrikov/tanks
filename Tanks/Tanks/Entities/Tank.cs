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
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction TankDirection { get; private set; }

        public Tank(int x, int y)
        {
            TankDirection = Direction.Up;
            X = x;
            Y = y;
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
