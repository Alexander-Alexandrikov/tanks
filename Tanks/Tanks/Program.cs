using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            MainForm pc;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            switch (args.Length)
            {
                case 0: 
                    pc = new MainForm(); break;
                case 1:
                    pc = new MainForm(Convert.ToInt32(args[0])); break;
                case 2:
                    pc = new MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1])); break;
                case 3:
                    pc = new MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2])); break;
                case 4:
                    pc = new MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2]), Convert.ToInt32(args[3])); break;
                case 5:
                    pc = new MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), Convert.ToInt32(args[4])); break;
                default:
                    pc = new MainForm(); break;

            }

            

            Application.Run(pc);
        }
    }
}
