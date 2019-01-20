using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Model
    {
        int _fieldWidth;
        int _fieldHeight;
        int _tanksAmount;
        int _applesAmount;
        public int _objectsSpeed;
        public Tank tank;
        public Kolobok kolobok;
        public WallView wallView;

        private List<Apple> apples;
        private List<Wall> walls;
        private List<Tank> tanks;
        private Random random;

        public List<Wall> Walls { get; }
        public List<Apple> Apples { get; }
        public List<Tank> Tanks { get; }

        public Model(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount, int objectsSpeed)
        {
            if((fieldWidth >= 250) && (fieldWidth <= 770))
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
            _tanksAmount = tanksAmount;
            _applesAmount = applesAmount;
            _objectsSpeed = objectsSpeed;
            //tank = new Tank();
            kolobok = new Kolobok();
            wallView = new WallView();
            apples = new List<Apple>();
            tanks = new List<Tank>();
            walls = new List<Wall>();
            random = new Random();
            Walls = walls;
            Apples = apples;
            Tanks = tanks;

            CreateWalls();
            CreateApples();
            CreateTanks();
            
        }

        private void CreateApples()
        {
            int _x;
            int _y;
            
            while (apples.Count < _applesAmount)
            {
                _x = random.Next(_fieldWidth - 20);
                _y = random.Next(_fieldHeight - 20);               

                bool flag = false;
                
                foreach (var a in walls)
                {
                    if (BoxCollides(_x, _y, 15, 15, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in apples)
                {
                    if (BoxCollides(_x, _y, 15, 15, a.X, a.Y, 20, 20))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    apples.Add(new Apple(_x, _y));
                }
                
            }
        }

        private void CreateTanks()
        {
            int _x;
            int _y;

            while (tanks.Count < _tanksAmount)
            {
                _x = random.Next(_fieldWidth - 20);
                _y = random.Next(_fieldHeight - 20);

                bool flag = false;

                foreach (var a in walls)
                {
                    if (BoxCollides(_x, _y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in tanks)
                {
                    if (BoxCollides(_x, _y, 20, 20, a.X, a.Y, 20, 20))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    tanks.Add(new Tank(_x, _y));
                }
            }
        }

        private void CreateWalls()
        {
            walls.Add(new Wall(140, 185, 50, 65));
            walls.Add(new Wall(170, 185, 140, 230));
            walls.Add(new Wall(30, 45, 80, 125));
            walls.Add(new Wall(100, 115, 5, 65));
            walls.Add(new Wall(80, 125, 130, 145));
            walls.Add(new Wall(0, 60, 170, 185));
        }

        public void NewGame()
        {
            kolobok.Run();
            
            Array values = Enum.GetValues(typeof(Direction));
            Direction tankDir = (Direction)values.GetValue(random.Next(values.Length));

            //обход стен
            //foreach (var a in walls)
            //{
            //    for (int i = 0; i < tanks.Count; i++)
            //    {
            //        if (BoxCollides(tanks[i].X, tanks[i].Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
            //        {
            //            tanks[i].TurnAround();
            //        }
            //    }
            //}



            //расчёт нового направления
            foreach (var element in tanks)
                if (random.Next(100) < 5)
                    element.Turn(tankDir);

            //проверка на разворот перед границей
            foreach (var element in tanks)
            {
                switch (element.TankDirection)
                {
                    case Direction.Left:
                        if (element.X - 1 < 0)
                            element.TurnAround();
                        break;
                    case Direction.Right:
                        if (element.X + 21 > _fieldWidth)
                            element.TurnAround();
                        break;
                    case Direction.Up:
                        if (element.Y - 1 < 0)
                            element.TurnAround();
                        break;
                    case Direction.Down:
                        if (element.Y + 21 > _fieldHeight)
                            element.TurnAround();
                        break;
                }
                
            }

            //столкновение танков
            for (int i = 0; i < tanks.Count - 1; i++)
                for (int j = 1; j < tanks.Count; j++)
                {
                    switch (tanks[i].TankDirection)
                    {
                        case Direction.Left:
                            if (BoxCollides(tanks[i].X - 1, tanks[i].Y, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Right:
                            if (BoxCollides(tanks[i].X + 1, tanks[i].Y, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Up:
                            if (BoxCollides(tanks[i].X, tanks[i].Y - 1, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Down:
                            if (BoxCollides(tanks[i].X, tanks[i].Y + 1, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                    }
                }

            //движение
            foreach (var t in tanks)
                t.Run();





        }

        // Collisions

        private bool Collides(int x, int y, int r, int b, int x2, int y2, int r2, int b2)
        {
            return !(r <= x2 || x > r2 ||
                     b <= y2 || y > b2);
        }

        private bool BoxCollides(int posX, int posY, int sizeX, int sizeY, int pos2X, int pos2Y, int size2X, int size2Y)
        {
            return Collides(posX, posY,
                            posX + sizeX, posY + sizeY,
                            pos2X, pos2Y,
                            pos2X + size2X, pos2Y + size2Y);
        }
    }
}
