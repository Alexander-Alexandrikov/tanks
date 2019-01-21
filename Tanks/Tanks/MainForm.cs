using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class MainForm : Form
    {
        Model model;
        PackmanController packmanController;

        int timerCounter = 0; //счётчик для таймера
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private Image MyImage;
        private int _objectsSpeed;

        public MainForm() : this(250) { }
        public MainForm(int fieldWidth) : this(fieldWidth, 250) { }
        public MainForm(int fieldWidth, int fieldHeight) : this(fieldWidth, fieldHeight, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount) : this(fieldWidth, fieldHeight, tanksAmount, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount) : this(fieldWidth, fieldHeight, tanksAmount, applesAmount, 10) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount, int objectsSpeed)
        {
            InitializeComponent();
            model = new Model(fieldWidth, fieldHeight, tanksAmount, applesAmount, objectsSpeed);
            pictureBox1.Size = new Size(fieldWidth, fieldHeight);
            packmanController = new PackmanController(model);
            _objectsSpeed = objectsSpeed;
        }

        

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            timer.Interval = _objectsSpeed;
            timer.Tick += new EventHandler(Timer_Tick); 
            timer.Start();

            packmanController.NewGame();
        }

        private void ShowMyImage(Image image, int xSize, int ySize)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawImage(image, new Point(xSize, ySize));
        }

        // отрисовка одной стены
        private void DrawWall(int xLeft, int xRight, int yUp, int yDown)
        {
            for(int i = xLeft; i < xRight; i += 15)
                for(int j = yUp; j < yDown; j += 15)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawImage(model.wallView.Img, new Point(i, j));
                }
        }

        // расставление всех препятствий на поле
        private void DrawAllWalls()
        {
            foreach(var w in model.Walls)
            {
                DrawWall(w.XLeft, w.XRight, w.YUp, w.YDown);
            }
            DrawWall(140, 185, 50, 65);
            DrawWall(170, 185, 140, 230);
            DrawWall(30, 45, 80, 125);
            DrawWall(100, 115, 5, 65);
            DrawWall(80, 125, 130, 145);
            DrawWall(0, 60, 170, 185);
        }

        private void DrawAllApples()
        {
            foreach(var element in model.Apples)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawImage(element.Image, new Point(element.X, element.Y));
            }
        }

        private void DrawAllTanks()
        {
            foreach (var t in model.Tanks)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawImage(t.TankImage, new Point(t.X, t.Y));
            }
        }

        private void DrawAllProjectiles()
        {
            foreach (var element in model.Projectiles)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawImage(element.ProjectileImage, new Point(element.X + 10, element.Y + 10));
            }
        }

        private void NewGameBtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            packmanController.ChangeDirection(e); 
        }

        private void ShowReport_KeyPress(object sender, KeyPressEventArgs e)
        {
            packmanController.ChangeDirection(e);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ShowMyImage(model.kolobok.TankImage, model.kolobok.X, model.kolobok.Y);
            DrawAllWalls();
            DrawAllApples();
            DrawAllTanks();
            DrawAllProjectiles();
            //Invalidate();
        }

        private void PaintAll()
        {
            ShowMyImage(model.kolobok.TankImage, model.kolobok.X, model.kolobok.Y);
            DrawAllWalls();
            DrawAllApples();
            DrawAllTanks();
            DrawAllProjectiles();
            //Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PaintAll();
            pictureBox1.Refresh();
        }

        private void ShowReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm(model);
            reportForm.Hide();
            reportForm.Show();
        }

        
    }
}
