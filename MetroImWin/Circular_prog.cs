using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetroImWin
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
            this.BackColor = Color.DarkOrange;
            this.TransparencyKey = Color.DarkOrange;
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Progress_Load(object sender, EventArgs e)
        {

        }

        private void circularProgressBar1_Click_1(object sender, EventArgs e)
        {

        }

        private void circularProgressBar1_Click_2(object sender, EventArgs e)
        {

        }
        protected override void OnPaintBackground(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.DarkOrange, e.ClipRectangle);
    }
    }
}
