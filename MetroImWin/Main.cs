using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;



namespace MetroImWin
{
    public partial class Main : Form
    {
        HttpReqest req=new HttpReqest();
        String res;
        private int counter = 0;
        List<Users> usr = new List<Users>();
        private String phone;
        String password;
        String ID_PATTERN = "^((\\d{1,9}))$";
        String NAME_PATTERN = "^((\\w+))$";
        static String PHONE_PATTERN = "\\+\\d{1,13}";
        private WindowsFormsSynchronizationContext context;
        private Progress progress;
        private DialogMessage errorDialog=new DialogMessage();
        private SendRes sendResult;
        private Notice notice;
        private System.Text.RegularExpressions.Regex reg;
        public Main(String phone, String password,String result)
        {
            InitializeComponent();
            this.phone = phone;
            this.password = password;
            user_tabel.AutoGenerateColumns = false;
            spinner_dept.SelectedIndex = 0;
            btn_notice.FlatAppearance.BorderSize=0;
            updateTable(result);
            notice = new Notice(phone,password);

        }
        public void setObject(ref object req)
        {
            this.req =(HttpReqest)req;
        }
        private void search_text_click(object sender, EventArgs e)
        {
            search_text.Tag = "";

        }
        private void spinner_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
          if(counter != 0)
            {
                if (Login.isAvailable())
                {
                    if (progress == null)
                    {
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        progress = new Progress();
                        progress.Owner = this;
                        progress.Show();
                        String de = spinner_dept.SelectedItem.ToString().ToLower();
                        Console.WriteLine("Conbobox selection " + de);

                        Task newtask = Task.Factory.StartNew(() =>
                        {
                            res = req.sendAuthenticatioReqest(this.phone, this.password, de);
                        }).ContinueWith(_ =>
                        {
                            progress.Close();
                            progress = null;
                            if (res != null && res != null)
                                updateTable(res);
                            else
                            {
                                errorDialog.setMsg("Error", "Failed to featched information");
                                errorDialog.ShowDialog();
                            }
                        }, context);
                    }
                    else progress.Activate();
                }
                else
                {
                    errorDialog.setMsg("Error", "Internet connections are not available");
                    errorDialog.ShowDialog();
                }
            }
            counter++;
        }
        public void setRequestInstace(object obj)
        {
            // req= obj.GetType(obj);
        }
        private void user_tabel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Login.isAvailable())
            {
                if (progress == null)
                {
                    var context = TaskScheduler.FromCurrentSynchronizationContext();
                    progress = new Progress();
                    progress.Owner = this;
                    progress.Show();
                    DataGridViewRow r = this.user_tabel.Rows[e.RowIndex];
                    String pho = r.Cells["Column3"].Value.ToString();
                    Console.WriteLine("This selected phone" + pho);
                    Task newtask = Task.Factory.StartNew(() => {
                        res = req.getInfo(this.phone, this.password, pho);
                    }).ContinueWith(_ => {
                        progress.Close();
                        progress = null;
                        if (res != "0")
                        {
                            ViewUsersData us = new ViewUsersData(this.phone, this.password, res);
                            us.Show();
                        }
                    }, context);
                }
                else progress.Activate();
            }
            else
            {
                errorDialog.setMsg("Error", "Internet connections are not available");
                errorDialog.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void updateTable(String result)
        {
            try
            {
                usr.Clear();
                JObject jData = JObject.Parse(result);
                var jArray = jData.GetValue("info");
                foreach (var item in jArray)
                {
                    Users u = new Users();
                    JObject j = ((JObject)item);
                    u.name =""+ j.GetValue("name");
                    u.id = "" + j.GetValue("id");
                    u.phone = "" + j.GetValue("phone");
                    usr.Add(u);
                   // user_tabel.Rows.Add(j.GetValue("id"), j.GetValue("name"), j.GetValue("phone"));
                }
            }
            catch (ArgumentException ui)
            {
            }
            catch (Exception e) { }
            user_tabel.Rows.Clear();
            foreach (var n in usr)
            {
                user_tabel.Rows.Add(n.id, n.name, n.phone);
            }
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            String query=search_text.Text;
            if(query != null && !query.Equals("Name, Id, Phone"))
            {
                searchUsers(query);
            }

        }

        private void btn_notice_Click(object sender, EventArgs e)
        {
           
            if (Login.isAvailable())
            {
                notice.ShowDialog();
            }
            else
            {
                errorDialog.setMsg("Error", "Internet connections are not available");
                errorDialog.ShowDialog();
            }
        }
        private void search_text_Click(object sender, EventArgs e)
        {
            search_text.Text = "";
        }
        private void search_text_TextChanged(object sender, EventArgs e)
        {
           
            String query = search_text.Text;
            if (!query.Equals("") && !query.Equals("Name, Id, Phone"))
            {
                if(query.Length>2)
                    searchUsers(query);
            }else 
            {
                user_tabel.Rows.Clear();
                foreach (var n in usr)
                user_tabel.Rows.Add(n.id, n.name, n.phone);
            }
        }
        private void searchUsers(String query)
        {
            reg = new Regex(PHONE_PATTERN);
            Match m = reg.Match(query);
            Regex reg1 = new Regex(ID_PATTERN);
            Match m1 = reg1.Match(query);
            Regex reg2 = new Regex(NAME_PATTERN);
            Match m2 = reg2.Match(query);
            if (m.Success)
            {
                user_tabel.Rows.Clear();
                foreach (var n in usr)
                {
                    if (n.phone.Contains(query))
                    {
                        user_tabel.Rows.Add(n.id, n.name, n.phone);
                    }
                }
            }
            else if (m1.Success)
            {
                user_tabel.Rows.Clear();
                foreach (var n in usr)
                {
                    if (n.id.Contains(query))
                    {
                        user_tabel.Rows.Add(n.id, n.name, n.phone);
                    }
                }
            }
            else if (m2.Success)
            {
                user_tabel.Rows.Clear();
                foreach (var n in usr)
                {
                    if ((n.name.ToLower()).Contains(query.ToLower()))
                    {
                        user_tabel.Rows.Add(n.id, n.name, n.phone);
                    }
                }
            }
/*            else
            {
                MessageBox.Show("Invalid query", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login from = new Login();
            from.FormClosed += new FormClosedEventHandler(mainClose);
            from.Show();
            this.Hide();
        }
        private void mainClose(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
