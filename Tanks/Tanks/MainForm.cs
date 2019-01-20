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


        private Image MyImage;

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
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            //model.NewGame();
            while (true)
            {
                //pictureBox1.;
                ShowMyImage(model.kolobok.TankImage, model.kolobok.X, model.kolobok.Y);
                DrawAllWalls();
                packmanController.NewGame(e);
                DrawAllApples();
                DrawAllTanks();


                System.Threading.Thread.Sleep(100/model._objectsSpeed);
                pictureBox1.Invalidate();
            }


            //Graphics g = pictureBox1.CreateGraphics();
            //g.DrawImage(model.tank.TankImage, new Point(model.tank.X, model.tank.Y));

            //вывод изображения из примера
            //pictureBox1.Size = new Size(210, 110);
            //this.Controls.Add(pictureBox1);

            //Bitmap flag = new Bitmap(200, 100);
            //Graphics flagGraphics = Graphics.FromImage(flag);
            //int red = 0;
            //int white = 11;
            //while (white <= 100)
            //{
            //    flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
            //    flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
            //    red += 20;
            //    white += 20;
            //}
            //pictureBox1.Image = flag;
            // конец вывода из примера
            //



            //packmanController.DrawImagePointF(e, )


        }

        private void ShowMyImage(Image image, int xSize, int ySize)
        {
            // Sets up an image object to be displayed.
            //if (MyImage != null)
            //{
            //    MyImage.Dispose();
            //}

            // Stretches the image to fit the pictureBox.
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //MyImage = image;
            //pictureBox1.ClientSize = new Size(xSize, ySize);
            //pictureBox1.Image = MyImage;

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
    }
}
