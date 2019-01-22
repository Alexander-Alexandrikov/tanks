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
    public partial class ReportForm : Form
    {
        Model _model;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public ReportForm(Model model)
        {
            InitializeComponent();
            _model = model;
            LoadData();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick); 
            timer.Start();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            
            List<string[]> dataList = new List<string[]>();

            dataList.Add(new string[3] { "Пакмен", _model.kolobok.X.ToString(), _model.kolobok.Y.ToString() });

            foreach (var el in _model.Tanks)
            {
                dataList.Add(new string[3] { "Танк", el.X.ToString(), el.Y.ToString() });
            }

            foreach (var el in _model.Apples)
            {
                dataList.Add(new string[3] { "Яблоко", el.X.ToString(), el.Y.ToString() });
            }

            foreach (var el in _model.Walls)
            {
                dataList.Add(new string[3] { "Стена", el.XLeft.ToString(), el.YUp.ToString() });
            }

            foreach (var e in dataList)
            {
                dataGridView1.Rows.Add(e);
            }
            
        }

       private void Timer_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Tick -= new EventHandler(Timer_Tick);
            timer.Dispose();
            ReportFormStatus.status = true;
        }
    }
}
