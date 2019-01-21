using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tanks
{
    public class Model : IDisposable
    {
        private int _fieldWidth;
        private int _fieldHeight;
        private int _tanksAmount;
        private int _applesAmount;
        private int _objectsSpeed;
        private List<Apple> apples;
        private List<Wall> walls;
        private List<Tank> tanks;
        private List<Projectile> projectiles;
        private Random random;
        public List<Wall> Walls { get; private set; }
        public List<Apple> Apples { get; private set; }
        public List<Tank> Tanks { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public Kolobok kolobok { get; private set; }
        public int GameCount { get; private set; }
        public WallView wallView { get; private set; }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
        public Model(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount, int objectsSpeed)
        {
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

            _tanksAmount = tanksAmount;
            _applesAmount = applesAmount;
            _objectsSpeed = objectsSpeed;
            kolobok = new Kolobok();
            wallView = new WallView();
            apples = new List<Apple>();
            tanks = new List<Tank>();
            walls = new List<Wall>();
            projectiles = new List<Projectile>();
            random = new Random();
            Walls = walls;
            Apples = apples;
            Tanks = tanks;
            Projectiles = projectiles;

            CreateWalls();
            CreateApples();
            CreateTanks();

            timer.Interval = _objectsSpeed;
            timer.Tick += new EventHandler(timer_Tick); //подписываемся на события Tick
            

        }

        public void NewGame()
        {
            timer.Start();
            kolobok.Run();

            Array values = Enum.GetValues(typeof(Direction));
            Direction tankDir = (Direction)values.GetValue(random.Next(values.Length));

            //выстрелы танков
            foreach (var t in tanks)
            {
                if (random.Next(100) < 1)
                {
                    switch (kolobok.TankDirection)
                    {
                        case Direction.Left:
                            projectiles.Add(new Projectile(t.X - 22, t.Y - 5, t.TankDirection));
                            break;
                        case Direction.Right:
                            projectiles.Add(new Projectile(t.X + 12, t.Y - 5, t.TankDirection));
                            break;
                        case Direction.Up:
                            projectiles.Add(new Projectile(t.X - 5, t.Y - 22, t.TankDirection));
                            break;
                        case Direction.Down:
                            projectiles.Add(new Projectile(t.X - 5, t.Y + 12, t.TankDirection));
                            break;
                    }
                }
            }

            //полёт снарядов
            foreach (var p in projectiles)
            {
                p.Run();
            }
                
            //расчёт нового направления танка
            foreach (var a in walls)
            {
                for (int i = 0; i < tanks.Count; i++)
                {
                    bool wallFlag = false;
                    switch (tankDir)
                    {
                        case Direction.Left:
                            if (Collide.BoxCollides(tanks[i].X - 1, tanks[i].Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Right:
                            if (Collide.BoxCollides(tanks[i].X + 1, tanks[i].Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Up:
                            if (Collide.BoxCollides(tanks[i].X, tanks[i].Y - 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Down:
                            if (Collide.BoxCollides(tanks[i].X, tanks[i].Y + 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                    }
                    if (random.Next(100) < 1)
                    {
                        tanks[i].Turn(tankDir);
                    }
                    if (wallFlag)
                    {
                        tanks[i].TurnAround();
                    }
                }
            }
                
            //проверка на разворот перед границей танка
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
                            if (Collide.BoxCollides(tanks[i].X - 1, tanks[i].Y, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Right:
                            if (Collide.BoxCollides(tanks[i].X + 1, tanks[i].Y, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Up:
                            if (Collide.BoxCollides(tanks[i].X, tanks[i].Y - 1, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Down:
                            if (Collide.BoxCollides(tanks[i].X, tanks[i].Y + 1, 20, 20, tanks[j].X, tanks[j].Y, 20, 20))
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                            break;
                    }
                }

            //столкновение танков с пулей
            foreach (var a in Projectiles)
            {
                for (int i = 0; i < tanks.Count; i++)
                {
                    if (Collide.BoxCollides(tanks[i].X, tanks[i].Y, 20, 20, a.X, a.Y, 10, 10))
                    {
                        tanks.Remove(tanks[i]);
                        CreateTanks();
                        break;
                    }
                    //Projectiles.Remove(Projectiles[i]);
                    //break;
                }
                
            }

            //подбор яблок
            foreach (var a in apples)
            {
                if (Collide.BoxCollides(kolobok.X, kolobok.Y, 20, 20, a.X, a.Y, 15, 15))
                {
                    apples.Remove(a);
                    GameCount++;
                    CreateApples();
                    break;
                }
            }

            //столкновение пакмена с границей
            switch (kolobok.TankDirection)
            {
                case Direction.Left:
                    if (kolobok.X - 1 < 0)
                        kolobok.TurnAround();
                    break;
                case Direction.Right:
                    if (kolobok.X + 21 > _fieldWidth)
                        kolobok.TurnAround();
                    break;
                case Direction.Up:
                    if (kolobok.Y - 1 < 0)
                        kolobok.TurnAround();
                    break;
                case Direction.Down:
                    if (kolobok.Y + 21 > _fieldHeight)
                        kolobok.TurnAround();
                    break;
            }

            //столкновение пакмена со стеной
            foreach (var a in walls)
            {
                bool wallFlag = false;
                switch (kolobok.TankDirection)
                {
                    case Direction.Left:
                        if (Collide.BoxCollides(kolobok.X - 1, kolobok.Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Right:
                        if (Collide.BoxCollides(kolobok.X + 1, kolobok.Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Up:
                        if (Collide.BoxCollides(kolobok.X, kolobok.Y - 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Down:
                        if (Collide.BoxCollides(kolobok.X, kolobok.Y + 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                }
                if (wallFlag)
                {
                    kolobok.TurnAround();
                }
            }

            //столкновение снаряда с границей
            foreach (var t in Projectiles)
            {
                switch (t._direction)
                {
                    case Direction.Left:
                        if (t.X - 1 < 0)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Right:
                        if (t.X + 16 > _fieldWidth)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Up:
                        if (t.Y - 1 < 0)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Down:
                        if (t.Y + 16 > _fieldHeight)
                            Projectiles.Remove(t);
                        break;
                }
                break;
            }

            //движение
            foreach (var t in tanks)
                t.Run();   
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
                    if (Collide.BoxCollides(_x, _y, 15, 15, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in apples)
                {
                    if (Collide.BoxCollides(_x, _y, 15, 15, a.X, a.Y, 20, 20))
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
                    if (Collide.BoxCollides(_x, _y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in tanks)
                {
                    if (Collide.BoxCollides(_x, _y, 20, 20, a.X, a.Y, 20, 20))
                    {
                        flag = true;
                        break;
                    }
                }

                if (Collide.BoxCollides(_x, _y, 20, 20, kolobok.X, kolobok.Y, 20, 20))
                {
                    flag = true;
                    break;
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

        private void timer_Tick(object sender, EventArgs e)
        {
            NewGame();
        }

        public void Shoot()
        {
            switch(kolobok.TankDirection)
            {
                case Direction.Left:
                    projectiles.Add(new Projectile(kolobok.X - 22, kolobok.Y - 5, kolobok.TankDirection));
                    break;
                case Direction.Right:
                    projectiles.Add(new Projectile(kolobok.X + 12, kolobok.Y - 5, kolobok.TankDirection));
                    break;
                case Direction.Up:
                    projectiles.Add(new Projectile(kolobok.X - 5, kolobok.Y - 22, kolobok.TankDirection));
                    break;
                case Direction.Down:
                    projectiles.Add(new Projectile(kolobok.X - 5, kolobok.Y + 12, kolobok.TankDirection));
                    break;

            }
        }


        public void Dispose()
        {
            kolobok = null;
            wallView = null;
            apples = null;
            tanks = null;
            walls = null;
            projectiles = null;
            random = null;
            Walls = null;
            Apples = null;
            Tanks = null;
            Projectiles = null;
        }
    }
}
