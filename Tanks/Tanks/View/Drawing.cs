using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public class Drawing
    {
        private PictureBox _pictureBox;
        private Graphics flagGraphics;
        private AppleView appleView = new AppleView();
        private KolobokView kolobokView = new KolobokView();
        private ProjectileView projectileView = new ProjectileView();
        private TankView tankView = new TankView();
        private WallView wallView = new WallView();
        private Image tankImage;
        private Model model;
        private Bitmap flag;

        public Drawing(PictureBox pictureBox, Model model)
        {
            _pictureBox = pictureBox;
            this.model = model;
            flag = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            flagGraphics = Graphics.FromImage(flag);
        }

        private void ShowMyImage(Image image, int xSize, int ySize)
        {
            flagGraphics.DrawImage(image, new Point(xSize, ySize));
        }

        // отрисовка одной стены
        private void DrawWall(int xLeft, int xRight, int yUp, int yDown)
        {
            for (int i = xLeft; i < xRight; i += 15)
                for (int j = yUp; j < yDown; j += 15)
                {
                    flagGraphics.DrawImage(wallView.Img, new Point(i, j));
                }
        }

        // расставление всех препятствий на поле
        private void DrawAllWalls()
        {
            foreach (var w in model.Walls)
            {
                DrawWall(w.XLeft, w.XRight, w.YUp, w.YDown);
            }
        }

        private void DrawAllApples()
        {
            foreach (var a in model.Apples)
            {
                flagGraphics.DrawImage(appleView.Img, a.Point);
            }
        }

        private void DrawAllTanks()
        {
            foreach (var t in model.Tanks)
            {
                PutImage(t, tankView);
                flagGraphics.DrawImage(tankImage, new Point(t.X, t.Y));
            }
        }

        private void DrawAllProjectiles()
        {
            foreach (var p in model.Projectiles)
            {
                flagGraphics.DrawImage(projectileView.ProjectileImg, new Point(p.X + 10, p.Y + 10));
            }
            foreach (var p in model.PackmanProjectiles)
            {
                flagGraphics.DrawImage(projectileView.ProjectileImg, new Point(p.X + 10, p.Y + 10));
            }
        }

        public void PaintAll()
        {
            flag = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            flagGraphics = Graphics.FromImage(flag);
            PutImage(model.kolobok, kolobokView);
            ShowMyImage(tankImage, model.kolobok.X, model.kolobok.Y);
            DrawAllWalls();
            DrawAllApples();
            DrawAllTanks();
            DrawAllProjectiles();
            _pictureBox.Image = flag;
        }

        private void PutImage(ITank tank, ITankView tankView)
        {
            switch (tank.TankDirection)
            {
                case Direction.Left:
                    tankImage = tankView.ImgLeft;
                    break;
                case Direction.Right:
                    tankImage = tankView.ImgRight;
                    break;
                case Direction.Up:
                    tankImage = tankView.ImgUp;
                    break;
                case Direction.Down:
                    tankImage = tankView.ImgDown;
                    break;
                default:
                    break;
            }
        }
    }
}
