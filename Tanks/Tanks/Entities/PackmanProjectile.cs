using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class PackmanProjectile
    {
        ProjectileView projectileView = new ProjectileView();
        public Image ProjectileImage { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction _direction;

        public PackmanProjectile(int x, int y, Direction direction)
        {
            ProjectileImage = projectileView.ProjectileImg;
            _direction = direction;
            switch (_direction)
            {
                case Direction.Left:
                    X = x;
                    Y = y + 10;
                    break;
                case Direction.Right:
                    X = x + 20;
                    Y = y + 10;
                    break;
                case Direction.Up:
                    X = x + 10;
                    Y = y;
                    break;
                case Direction.Down:
                    Y = y + 20;
                    X = x + 10;
                    break;
                default:
                    break;
            }
            X = x;
            Y = y;
        }

        public void Run()
        {
            switch (_direction)
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
    }
}
