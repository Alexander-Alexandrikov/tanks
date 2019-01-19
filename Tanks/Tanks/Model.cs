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
        int _objectsSpeed;
        public Tank tank;
        public WallView wallView;

        public Model(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount, int objectsSpeed)
        {
            _fieldWidth = fieldWidth;
            _fieldHeight = fieldHeight;
            _tanksAmount = tanksAmount;
            _applesAmount = applesAmount;
            _objectsSpeed = objectsSpeed;
            tank = new Tank();
            wallView = new WallView();
        }

        public void NewGame()
        {
            //while (true)
            //{
            //    tank.Run();
            //}
        }
    }
}
