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
        private Model model;
        private PackmanController packmanController;
       
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private Image MyImage;
        private int _fieldWidth;
        private int _fieldHeight;
        private int _tanksAmount;
        private int _applesAmount;
        private int _objectsSpeed;

        private AppleView appleView = new AppleView();
        private KolobokView kolobokView = new KolobokView();
        private ProjectileView projectileView = new ProjectileView();
        private TankView tankView = new TankView();
        private WallView wallView = new WallView();
        private Graphics g;

        public MainForm() : this(250) { }
        public MainForm(int fieldWidth) : this(fieldWidth, 250) { }
        public MainForm(int fieldWidth, int fieldHeight) : this(fieldWidth, fieldHeight, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount) : this(fieldWidth, fieldHeight, tanksAmount, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount) : this(fieldWidth, fieldHeight, tanksAmount, applesAmount, 10) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount, int objectsSpeed)
        {
            InitializeComponent();
            
            if ((fieldWidth >= 250) && (fieldWidth <= 770))
            {
                _fieldWidth = fieldWidth;
            }
            else
            {
                _fieldWidth = 250;
            }

            if ((fieldHeight >= 250) && (fieldHeight <= 300))
            {
                _fieldHeight = fieldHeight;
            }
            else
            {
                _fieldHeight = 250;
            }

            if ((tanksAmount >= 10) && (tanksAmount <= 1))
            {
                _tanksAmount = tanksAmount;
            }
            else
            {
                _tanksAmount = 5;
            }

            if ((applesAmount >= 10) && (applesAmount <= 1))
            {
                _applesAmount = applesAmount;
            }
            else
            {
                _applesAmount = 5;
            }

            if ((objectsSpeed >= 10) && (objectsSpeed <= 1))
            {
                _objectsSpeed = objectsSpeed;
            }
            else
            {
                _objectsSpeed = 10;
            }

            model = new Model(_fieldWidth, _fieldHeight, _tanksAmount, _applesAmount, _objectsSpeed);
            pictureBox1.Size = new Size(_fieldWidth, _fieldHeight);
            packmanController = new PackmanController(model);
            timer.Interval = _objectsSpeed;
            timer.Tick += new EventHandler(Timer_Tick);
            g = pictureBox1.CreateGraphics();
        }

        

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            timer.Stop();
            packmanController.GameOver();
            Play();
            timer.Start();
        }

        private void ShowMyImage(Image image, int xSize, int ySize)
        {
            g.DrawImage(image, new Point(xSize, ySize));
        }

        // отрисовка одной стены
        private void DrawWall(int xLeft, int xRight, int yUp, int yDown)
        {
            for(int i = xLeft; i < xRight; i += 15)
                for(int j = yUp; j < yDown; j += 15)
                {
                    g.DrawImage(wallView.Img, new Point(i, j));
                }
        }

        // расставление всех препятствий на поле
        private void DrawAllWalls()
        {
            foreach(var w in model.Walls)
            {
                DrawWall(w.XLeft, w.XRight, w.YUp, w.YDown);
            }
        }

        private void DrawAllApples()
        {
            foreach(var element in model.Apples)
            {
                g.DrawImage(appleView.Img, new Point(element.X, element.Y));
            }
        }

        private void DrawAllTanks()
        {
            foreach (var t in model.Tanks)
            {
                g.DrawImage(t.TankImage, new Point(t.X, t.Y));
            }
        }

        private void DrawAllProjectiles()
        {
            foreach (var element in model.Projectiles)
            {               
                g.DrawImage(projectileView.ProjectileImg, new Point(element.X + 10, element.Y + 10));
            }
            foreach (var element in model.PackmanProjectiles)
            {
                g.DrawImage(element.ProjectileImage, new Point(element.X + 10, element.Y + 10));
            }
        }

        private void Btn_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void PaintAll()
        {
            ShowMyImage(model.kolobok.TankImage, model.kolobok.X, model.kolobok.Y);
            DrawAllWalls();
            DrawAllApples();
            DrawAllTanks();
            DrawAllProjectiles();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (model.GameStatus == false)
                timer.Stop();
            PaintAll();
            pictureBox1.Refresh();
            textBox1.Text = model.GameCount.ToString();
        }

        private void ShowReport_Click(object sender, EventArgs e)
        {
            if(ReportFormStatus.status)
            {
                ReportForm reportForm = new ReportForm(model);
                reportForm.Hide();
                reportForm.Show();
                ReportFormStatus.status = false;
            }           
        }

        private void Play()
        {
            packmanController.NewGame();
            PaintAll();
            pictureBox1.Refresh();
            textBox1.Text = model.GameCount.ToString();
        }
    }
}
