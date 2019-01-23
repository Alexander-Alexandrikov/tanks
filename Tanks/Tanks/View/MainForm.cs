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
        private int _fieldWidth;
        private int _fieldHeight;
        private int _tanksAmount;
        private int _applesAmount;
        private int _objectsSpeed;
        private Drawing drawing;

        public MainForm() : this(250) { }
        public MainForm(int fieldWidth) : this(fieldWidth, 250) { }
        public MainForm(int fieldWidth, int fieldHeight) : this(fieldWidth, fieldHeight, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount) : this(fieldWidth, fieldHeight, tanksAmount, 5) { }
        public MainForm(int fieldWidth, int fieldHeight, int tanksAmount, int applesAmount) : this(fieldWidth, fieldHeight, tanksAmount, applesAmount, 100) { }
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
            drawing = new Drawing(pictureBox1, model);
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            timer.Stop();
            packmanController.GameOver();
            timer.Start();
            Play();
            
        }       

        private void Btn_KeyPress(object sender, KeyPressEventArgs e)
        {
            packmanController.ChangeDirection(e); 
        }

        private void ShowReport_KeyPress(object sender, KeyPressEventArgs e)
        {
            packmanController.ChangeDirection(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (model.GameStatus == false)
                timer.Stop();
            drawing.PaintAll();
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
            drawing.PaintAll();
            textBox1.Text = model.GameCount.ToString();
        }
    }
}
