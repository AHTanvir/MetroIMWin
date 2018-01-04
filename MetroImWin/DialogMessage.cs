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
    public partial class DialogMessage : Form
    {
        public DialogMessage(String message)
        {
            InitializeComponent();
            label1.Text = message;
        }
        public DialogMessage(String titel,String message)
        {
            InitializeComponent();
            label2.Text =titel;
            label1.Text = message;
        }
        public DialogMessage()
        {
            InitializeComponent();
        }
        public void setMsg(String titel, String message)
        {
            label2.Text = titel;
            label1.Text = message;
        }
        public void setMsg(String message)
        {
            label1.Text = message;
        }
    }
}
