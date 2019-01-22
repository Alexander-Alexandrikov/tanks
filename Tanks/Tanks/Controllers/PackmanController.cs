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
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int keyChangeStatus;

        public PackmanController(Model viewModel)
        {
            model = viewModel;
            timer.Interval = model.ObjectsSpeed;
            timer.Tick += new EventHandler(Timer_Tick);
            keyChangeStatus = 0;
        }

        public void NewGame()
        {
            model.StartTimer();
            timer.Start();
            model.NewGame();
        }

        public void GameOver()
        {
            model.GameOver();
        }       

        public void ChangeDirection(KeyPressEventArgs e)
        {
            if(keyChangeStatus > 1)
            {
                switch (e.KeyChar)
                {
                    case 'a':
                    case 'A':
                    case 'ф':
                    case 'Ф':
                        model.kolobok.SetDirection(Direction.Left);
                        break;
                    case 'd':
                    case 'D':
                    case 'в':
                    case 'В':
                        model.kolobok.SetDirection(Direction.Right);
                        break;
                    case 'w':
                    case 'W':
                    case 'ц':
                    case 'Ц':
                        model.kolobok.SetDirection(Direction.Up);
                        break;
                    case 's':
                    case 'S':
                    case 'ы':
                    case 'Ы':
                        model.kolobok.SetDirection(Direction.Down);
                        break;
                    case 'l':
                    case 'L':
                    case 'д':
                    case 'Д':
                        model.Shoot();
                        break;
                }
                keyChangeStatus = 0;
            }           
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            keyChangeStatus++;
        }
    }
}
