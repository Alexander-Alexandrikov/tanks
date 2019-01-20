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

        public PackmanController(Model viewModel)
        {
            model = viewModel;
        }

        public void NewGame(EventArgs e)
        {
            model.NewGame();
        }

        //private void ShowMyImage(PaintEventArgs e, Image image, int xSize, int ySize)
        //{
        //    // Sets up an image object to be displayed.
        //    if (MyImage != null)
        //    {
        //        MyImage.Dispose();
        //    }

        //    // Stretches the image to fit the pictureBox.
        //    //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        //    MyImage = image;
        //    //pictureBox1.ClientSize = new Size(xSize, ySize);
        //    //pictureBox1.Image = MyImage;
        //    Graphics g = pictureBox1.CreateGraphics();
        //    g.DrawImage(MyImage, new Point(0, 0));
        //}

        //public void DrawImagePointF(PaintEventArgs e, Image image, int xSize, int ySize)
        //{

        //    // Sets up an image object to be displayed.
        //    if (MyImage != null)
        //    {
        //        MyImage.Dispose();
        //    }

        //    MyImage = image;

        //    // Create point for upper-left corner of image.
        //    PointF ulCorner = new PointF(100.0F, 100.0F);

        //    // Draw image to screen.
        //    e.Graphics.DrawImage(MyImage, ulCorner);
        //}

        //// отрисовка одной стены
        //private void DrawWall(int xLeft, int xRight, int yUp, int yDown)
        //{
        //    for (int i = xLeft; i < xRight; i += 15)
        //        for (int j = yUp; j < yDown; j += 15)
        //        {
        //            Graphics g = pictureBox1.CreateGraphics();
        //            g.DrawImage(model.wallView.Img, new Point(i, j));
        //        }
        //}

        //// расставление всех препятствий на поле
        //private void DrawAllWalls()
        //{
        //    DrawWall(140, 185, 50, 65);
        //    DrawWall(170, 185, 140, 230);
        //    DrawWall(30, 45, 80, 125);
        //    DrawWall(100, 115, 5, 65);
        //    DrawWall(80, 125, 130, 145);
        //    DrawWall(0, 60, 170, 185);
        //}
    }
}
