using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public class PackmanController
    {
        private Model model;
        private int _fieldWidth;
        private int _fieldHeight;
        private int _tanksAmount;
        private int _applesAmount;
        private int _objectsSpeed;

        public PackmanController(Model viewModel)
        {
            model = viewModel;
        }

        public void NewGame()
        {
            //model = new Model();
            model.NewGame();
            //model.Dispose();
            //model = null;
        }

        public void ChangeDirection(KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case 'a':
                    model.kolobok.SetDirection(Direction.Left);
                    break;
                case 'd':
                    model.kolobok.SetDirection(Direction.Right);
                    break;
                case 'w':
                    model.kolobok.SetDirection(Direction.Up);
                    break;
                case 's':
                    model.kolobok.SetDirection(Direction.Down);
                    break;

                case 'A':
                    model.kolobok.SetDirection(Direction.Left);
                    break;
                case 'D':
                    model.kolobok.SetDirection(Direction.Right);
                    break;
                case 'W':
                    model.kolobok.SetDirection(Direction.Up);
                    break;
                case 'S':
                    model.kolobok.SetDirection(Direction.Down);
                    break;

                case 'ф':
                    model.kolobok.SetDirection(Direction.Left);
                    break;
                case 'в':
                    model.kolobok.SetDirection(Direction.Right);
                    break;
                case 'ц':
                    model.kolobok.SetDirection(Direction.Up);
                    break;
                case 'ы':
                    model.kolobok.SetDirection(Direction.Down);
                    break;

                case 'Ф':
                    model.kolobok.SetDirection(Direction.Left);
                    break;
                case 'В':
                    model.kolobok.SetDirection(Direction.Right);
                    break;
                case 'Ц':
                    model.kolobok.SetDirection(Direction.Up);
                    break;
                case 'Ы':
                    model.kolobok.SetDirection(Direction.Down);
                    break;
                case ' ':
                    model.Shoot();
                    break;
            }

        }
    }
}
