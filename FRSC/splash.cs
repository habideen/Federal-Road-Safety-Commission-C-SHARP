using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRSC
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        public int timerLeft { get; set; }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timerLeft < 10)
            {
                ++timerLeft;
                progressBar1.Value = progressBar1.Value + 10;
            }
            else
            {
                timer1.Stop();
                new loginFrm().Show();
                this.Hide();
            }
        }

        private void splash_Load(object sender, EventArgs e)
        {
            timerLeft = 0;
            timer1.Start();
        }
    }
}
