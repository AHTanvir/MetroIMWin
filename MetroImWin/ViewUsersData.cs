using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MetroImWin
{
    public partial class ViewUsersData : Form
    {
        String phone;
        String password;
        String contact;
        HttpReqest req=new HttpReqest();
        private WindowsFormsSynchronizationContext context;
        private Progress progress;
        private String res = "0";
        private DialogMessage errorMsg;
        public ViewUsersData(String phone, String password,String result)
        {
            this.phone = phone;
            this.password = password;
            InitializeComponent();
            btn_block.FlatAppearance.BorderSize = 0;
            Task task = Task.Factory.StartNew(() =>
            {
                setResult(result);
            });
            task.Wait();
          //  pictureBox1.Location = Image.FromHbitmap(CreateBitmapImage(""));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
        public void setResult(String result)
        {
            try {
                JObject jData = JObject.Parse(result);
                var jArray = jData.GetValue("info");
                foreach (var item in jArray)
                {
                    JObject j = ((JObject)item);
                    Console.WriteLine("This json array" + j.GetValue("phone"));
                    label1.Text =""+ j.GetValue("name");
                    label2.Text = "" + j.GetValue("id");
                    this.contact=""+j.GetValue("phone");
                    label3.Text =contact;
                    label4.Text = "" + j.GetValue("type");
                    label5.Text = "" + j.GetValue("dept");
                    label6.Text = "" + j.GetValue("ip");
                    label7.Text = "" + j.GetValue("port");
                    label8.Text = "" + j.GetValue("l_seen");
                    String status =""+j.GetValue("contactstatus");
                    if (status.Equals("Block"))
                        btn_block.Text = "Unblock";
                    else btn_block.Text = "Block";
                    pictureBox1.Image = Base64StringToBitmap(""+ j.GetValue("photo"));
                }
            }
            catch (ArgumentException ui)
            {
            }
            catch (Exception e) { }
        }
        public static Bitmap Base64StringToBitmap(String base64String)
      {
          Bitmap bmpReturn = null;

          byte[] byteBuffer = Convert.FromBase64String(base64String);
          MemoryStream memoryStream = new MemoryStream(byteBuffer);

          memoryStream.Position = 0;

          bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

          memoryStream.Close();
          memoryStream = null;
          byteBuffer = null;

          return bmpReturn;
      }

        private void ViewUsersData_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_block_Click(object sender, EventArgs e)
        {
            String btext = btn_block.Text;
            if (progress == null)
            {
                var context = TaskScheduler.FromCurrentSynchronizationContext();
                progress = new Progress();
                progress.Owner = this;
                progress.Show();
                if (btext.Equals("Block"))
                {
                    Task newtask = Task.Factory.StartNew(() =>
                    {
                        res = req.userBlock(this.phone, this.password, contact, "Block");
                    }).ContinueWith(_ => {
                        progress.Close();
                        progress = null;
                        if (res != "0")
                        {
                            btn_block.Text = "Unblock";
                            errorMsg = new DialogMessage("Successful", "Blocked");
                            errorMsg.ShowDialog();
                        }
                        else showErrorDialog("Failed");

                        Console.WriteLine("send result callBack " + res);
                    },context);
                }
                else
                {
                    Task newtask = Task.Factory.StartNew(() =>
                    {
                        res = req.userBlock(this.phone, this.password, contact, "Hi");
                    }).ContinueWith(_ => {
                        progress.Close();
                        progress = null;
                        if (res != "0")
                        {
                            btn_block.Text = "Block";
                            errorMsg = new DialogMessage("Successful", "Unblocked");
                            errorMsg.ShowDialog();
                        }
                        else showErrorDialog("Failed");

                        Console.WriteLine("send result callBack " + res);
                    }, context);
                }
                Console.WriteLine("block result " + res);
            }
        }
        private void showErrorDialog(String msg)
        {
            errorMsg = new DialogMessage(msg);
            errorMsg.ShowDialog();
        }
    }
}
