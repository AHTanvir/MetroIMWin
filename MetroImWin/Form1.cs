using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetroImWin
{
    public partial class Login : Form
    {
       static private HttpReqest req;
        static String PHONE_PATTERN ="\\+\\d{13}";
        static String res;
        private WindowsFormsSynchronizationContext context;
        Regex reg = new Regex(PHONE_PATTERN);
        private DialogMessage errorDilog=new DialogMessage();
        private Progress progress;
        bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        public Login()
        {
            InitializeComponent();
            button_login.FlatAppearance.BorderSize = 0;

   
        /*    Task task = Task.Factory.StartNew(() =>
            {
                ReadDocx b = new ReadDocx();
                
            });
            task.Wait();*/

    }

        private void phone_text_TextChanged(object sender, EventArgs e)
        {
        }
        private void phone_text_Click(object sender, EventArgs e)
        {
            phone_text.Text = "+8801736549421";
        }
        private void password_text_Click(object sender, EventArgs e)
        {
            password_text.UseSystemPasswordChar = true;
            password_text.Text = "anwar";

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (isAvailable())
            {
                String phone = phone_text.Text;
                String pass = password_text.Text;
                Match m = reg.Match(phone);
                if (m.Success && pass.Length > 4)
                {
                    if (progress == null)
                    {
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        progress = new Progress();
                        progress.Owner = this;
                        progress.Show();
                        req = new HttpReqest();
                        Task newtask = Task.Factory.StartNew(() =>
                        {
                            res = req.sendAuthenticatioReqest(phone, pass, "cse");
                        }

                   ).ContinueWith(_ =>
                   {
                       progress.Close();
                       progress = null;
                       if (!res.Equals("0"))
                       {
                           Main main = new Main(phone, pass, res);
                           main.FormClosed += new FormClosedEventHandler(mainClose);
                           main.Show();
                           this.Hide();
                       }
                       else
                       {
                           errorDilog.setMsg("Error", "Phone or Password does't match");
                           errorDilog.ShowDialog();
                       }
                   }, context);
                    }
                    else progress.Activate();
                }
                else error_label.Text = "Invalid phone or password";
            }
            else
            {
                errorDilog.setMsg("Error", "Internet connections are not available");
                errorDilog.ShowDialog();
            }

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void error_label_Click(object sender, EventArgs e)
        {

        }
        private void mainClose(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool isAvailable()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
    }
}
