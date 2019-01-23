using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tanks
{
    public class Model
    {
        private int _fieldWidth;
        private int _fieldHeight;
        private int _tanksAmount;
        private int _applesAmount;
        private int _objectsSpeed;
        private Random random;
        public bool GameStatus { get; private set; } = true;
        public List<Wall> Walls { get; private set; }
        public List<Apple> Apples { get; private set; }
        public List<Tank> Tanks { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public List<PackmanProjectile> PackmanProjectiles { get; private set; }
        public Kolobok kolobok { get; private set; }
        public int GameCount { get; private set; }
        public int ObjectsSpeed { get; private set; }
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        
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
                ObjectsSpeed = objectsSpeed;
            }
            else
            {
                ObjectsSpeed = 10;
            }
            _objectsSpeed = objectsSpeed;
            kolobok = new Kolobok();
            random = new Random();
            Walls = new List<Wall>();
            Apples = new List<Apple>(); 
            Tanks = new List<Tank>();
            Projectiles = new List<Projectile>();

            PackmanProjectiles = new List<PackmanProjectile>();
            CreateWalls();
            CreateApples();
            CreateTanks();

            timer.Interval = _objectsSpeed;
            timer.Tick += new EventHandler(timer_Tick);
        }

        public void StartTimer()
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void NewGame()
        {
            CreateApples();
            CreateTanks();
            GameStatus = true;
            timer.Start();
            kolobok.Run();
            DoWithTanks();
            DoWithPackman();
            DoWithProjectiles();
        }

        private void DoWithTanks()
        {
            Array values = Enum.GetValues(typeof(Direction));
            Direction tankDir = (Direction)values.GetValue(random.Next(values.Length));

            //выстрелы танков
            foreach (var t in Tanks)
            {
                if (random.Next(100) < 1)
                {
                    switch (kolobok.TankDirection)
                    {
                        case Direction.Left:
                            Projectiles.Add(new Projectile(t.X - 22, t.Y - 5, t.TankDirection));
                            break;
                        case Direction.Right:
                            Projectiles.Add(new Projectile(t.X + 12, t.Y - 5, t.TankDirection));
                            break;
                        case Direction.Up:
                            Projectiles.Add(new Projectile(t.X - 5, t.Y - 22, t.TankDirection));
                            break;
                        case Direction.Down:
                            Projectiles.Add(new Projectile(t.X - 5, t.Y + 12, t.TankDirection));
                            break;
                    }
                }
            }

            //расчёт нового направления танка
            foreach (var a in Walls)
            {
                for (int i = 0; i < Tanks.Count; i++)
                {
                    bool wallFlag = false;
                    if (random.Next(100) < 1)
                    {
                        Tanks[i].Turn(tankDir);
                    }
                    switch (Tanks[i].TankDirection)
                    {
                        case Direction.Left:
                            if (AllCollision.BoxCollides(Tanks[i].X - 1, Tanks[i].Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Right:
                            if (AllCollision.BoxCollides(Tanks[i].X + 1, Tanks[i].Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Up:
                            if (AllCollision.BoxCollides(Tanks[i].X, Tanks[i].Y - 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                        case Direction.Down:
                            if (AllCollision.BoxCollides(Tanks[i].X, Tanks[i].Y + 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                            {
                                wallFlag = true;
                            }
                            break;
                    }
                    if (wallFlag)
                    {
                        Tanks[i].TurnAround();
                    }
                }
            }

            //проверка на разворот перед границей танка
            foreach (var element in Tanks)
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
            for (int i = 0; i < Tanks.Count; i++)
                for (int j = 0; j < Tanks.Count; j++)
                {
                    switch (Tanks[i].TankDirection)
                    {
                        case Direction.Left:
                            if (AllCollision.BoxCollides(Tanks[i].X - 2, Tanks[i].Y, 20, 20, Tanks[j].X, Tanks[j].Y, 20, 20) && (i != j))
                            {
                                Tanks[i].TurnAround();
                                Tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Right:
                            if (AllCollision.BoxCollides(Tanks[i].X + 2, Tanks[i].Y, 20, 20, Tanks[j].X, Tanks[j].Y, 20, 20) && (i != j))
                            {
                                Tanks[i].TurnAround();
                                Tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Up:
                            if (AllCollision.BoxCollides(Tanks[i].X, Tanks[i].Y - 2, 20, 20, Tanks[j].X, Tanks[j].Y, 20, 20) && (i != j))
                            {
                                Tanks[i].TurnAround();
                                Tanks[j].TurnAround();
                            }
                            break;
                        case Direction.Down:
                            if (AllCollision.BoxCollides(Tanks[i].X, Tanks[i].Y + 2, 20, 20, Tanks[j].X, Tanks[j].Y, 20, 20) && (i != j))
                            {
                                Tanks[i].TurnAround();
                                Tanks[j].TurnAround();
                            }
                            break;
                    }
                }

            //столкновение танков с пулей
            foreach (var a in PackmanProjectiles)
            {
                for (int i = 0; i < Tanks.Count; i++)
                {
                    if (AllCollision.BoxCollides(Tanks[i].X, Tanks[i].Y, 20, 20, a.X, a.Y, 10, 10))
                    {
                        Tanks.Remove(Tanks[i]);
                        CreateTanks();
                        break;
                    }
                }

            }

            //движение
            foreach (var t in Tanks)
                t.Run();
        }

        private void DoWithPackman()
        {
            //подбор яблок
            foreach (var a in Apples)
            {
                if (AllCollision.BoxCollides(kolobok.X, kolobok.Y, 20, 20, a.X, a.Y, 15, 15))
                {
                    Apples.Remove(a);
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
            foreach (var a in Walls)
            {
                bool wallFlag = false;
                switch (kolobok.TankDirection)
                {
                    case Direction.Left:
                        if (AllCollision.BoxCollides(kolobok.X - 1, kolobok.Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Right:
                        if (AllCollision.BoxCollides(kolobok.X + 1, kolobok.Y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Up:
                        if (AllCollision.BoxCollides(kolobok.X, kolobok.Y - 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                        {
                            wallFlag = true;
                        }
                        break;
                    case Direction.Down:
                        if (AllCollision.BoxCollides(kolobok.X, kolobok.Y + 1, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
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

            // столкновение пакмена с пулей или танком
            foreach (var a in Projectiles)
            {
                if (AllCollision.BoxCollides(kolobok.X, kolobok.Y, 20, 20, a.X, a.Y, 10, 10))
                {
                    GameOver();
                }
            }

            foreach (var a in Tanks)
            {
                if (AllCollision.BoxCollides(kolobok.X, kolobok.Y, 20, 20, a.X, a.Y, 20, 20))
                {
                    GameOver();
                }
            }
        }

        private void DoWithProjectiles()
        {
            //полёт снарядов
            foreach (var p in Projectiles)
            {
                p.Run();
            }
            foreach (var p in PackmanProjectiles)
            {
                p.Run();
            }

            //столкновение снаряда с границей
            foreach (var t in Projectiles)
            {
                switch (t._direction)
                {
                    case Direction.Left:
                        if (t.X + 10 < 0)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Right:
                        if (t.X + 20 > _fieldWidth)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Up:
                        if (t.Y + 10 < 0)
                            Projectiles.Remove(t);
                        break;
                    case Direction.Down:
                        if (t.Y + 20 > _fieldHeight)
                            Projectiles.Remove(t);
                        break;
                }
                break;
            }

            foreach (var t in PackmanProjectiles)
            {
                switch (t._direction)
                {
                    case Direction.Left:
                        if (t.X + 10 < 0)
                            PackmanProjectiles.Remove(t);
                        break;
                    case Direction.Right:
                        if (t.X + 20 > _fieldWidth)
                            PackmanProjectiles.Remove(t);
                        break;
                    case Direction.Up:
                        if (t.Y + 10 < 0)
                            PackmanProjectiles.Remove(t);
                        break;
                    case Direction.Down:
                        if (t.Y + 20 > _fieldHeight)
                            PackmanProjectiles.Remove(t);
                        break;
                }
                break;
            }

            //столкновение снаряда со стеной
            foreach (var a in Walls)
            {
                for (int i = 0; i < Projectiles.Count; i++)
                {
                    if (AllCollision.BoxCollides(Projectiles[i].X + 10, Projectiles[i].Y + 10, 10, 10, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        Projectiles.Remove(Projectiles[i]);
                    }
                }
            }

            foreach (var a in Walls)
            {
                for (int i = 0; i < PackmanProjectiles.Count; i++)
                {
                    if (AllCollision.BoxCollides(PackmanProjectiles[i].X + 10, PackmanProjectiles[i].Y + 10, 10, 10, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        PackmanProjectiles.Remove(PackmanProjectiles[i]);
                    }
                }
            }
        }

        private void CreateApples()
        {
            int _x;
            int _y;

            while (Apples.Count < _applesAmount)
            {
                _x = random.Next(_fieldWidth - 20);
                _y = random.Next(_fieldHeight - 20);

                bool flag = false;

                foreach (var a in Walls)
                {
                    if (AllCollision.BoxCollides(_x, _y, 15, 15, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in Apples)
                {
                    if (AllCollision.BoxCollides(_x, _y, 15, 15, a.X, a.Y, 20, 20))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    Apples.Add(new Apple(_x, _y));
                }

            }
        }

        private void CreateTanks()
        {
            int _x;
            int _y;

            while (Tanks.Count < _tanksAmount)
            {
                _x = random.Next(_fieldWidth - 20);
                _y = random.Next(_fieldHeight - 100);

                bool flag = false;

                foreach (var a in Walls)
                {
                    if (AllCollision.BoxCollides(_x, _y, 20, 20, a.XLeft, a.YUp, a.XRight - a.XLeft, a.YDown - a.YUp))
                    {
                        flag = true;
                        break;
                    }
                }

                foreach (var a in Tanks)
                {
                    if (AllCollision.BoxCollides(_x, _y, 20, 20, a.X, a.Y, 20, 20))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    Tanks.Add(new Tank(_x, _y));
                }

            }
        }

        private void CreateWalls()
        {
            Walls.Add(new Wall(140, 185, 50, 65));
            Walls.Add(new Wall(170, 185, 140, 230));
            Walls.Add(new Wall(30, 45, 80, 125));
            Walls.Add(new Wall(100, 115, 5, 65));
            Walls.Add(new Wall(80, 125, 130, 145));
            Walls.Add(new Wall(0, 60, 170, 185));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            NewGame();
        }

        public void Shoot()
        {
            switch (kolobok.TankDirection)
            {
                case Direction.Left:
                    PackmanProjectiles.Add(new PackmanProjectile(kolobok.X - 21, kolobok.Y - 5, kolobok.TankDirection));
                    break;
                case Direction.Right:
                    PackmanProjectiles.Add(new PackmanProjectile(kolobok.X + 11, kolobok.Y - 5, kolobok.TankDirection));
                    break;
                case Direction.Up:
                    PackmanProjectiles.Add(new PackmanProjectile(kolobok.X - 5, kolobok.Y - 21, kolobok.TankDirection));
                    break;
                case Direction.Down:
                    PackmanProjectiles.Add(new PackmanProjectile(kolobok.X - 5, kolobok.Y + 11, kolobok.TankDirection));
                    break;

            }
        }

        public void GameOver()
        {
            timer.Stop();
            timer.Tick -= new EventHandler(timer_Tick);
            Tanks = null;
            Projectiles = null;
            PackmanProjectiles = null;
            GameCount = 0;
            kolobok.SetBeginValue();
            Tanks = new List<Tank>();
            PackmanProjectiles = new List<PackmanProjectile>();
            Apples = new List<Apple>();
            Tanks = Tanks;
            Projectiles = new List<Projectile>();
            GameStatus = false;          
        }
    }
}
