using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MetroImWin
{
    public partial class Notice : Form
    {
        private WindowsFormsSynchronizationContext context;
        private String phone;
        private String password;
        private String dept;
        private String path=null;
        private List<IdModel> ids = new List<IdModel>();
        private List<ResultModel> results = new List<ResultModel>();
        private List<JObject> temlist = new List<JObject>();
        private Message mme = new Message();
        private String ID_PATTERN = "(\\d{7})";
        private Progress progress;
        private String res = "0";
        private String type;
        private int id1, id2;
        private String name ="";
        private static String ID_PATTERN2= "^((\\d{3})[\\s-])(\\d{3}[\\s-])(\\d{1,3})$";
        private HttpReqest httpReq = new HttpReqest();

        public Notice(String phone,String password)
        {
            InitializeComponent();
            btn_send.FlatAppearance.BorderSize = 0;
            this.phone = phone;
            this.password = password;
        }
        Regex reg = new Regex(ID_PATTERN2);
        private DialogMessage errorMsg= new DialogMessage();
        private void dept_spinner_SelectedIndexChanged(object sender, EventArgs e)
        {
            dept = dept_spinner.SelectedItem.ToString().ToLower();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Match m = reg.Match(beg_id.Text);
            Match m2 = reg.Match(end_id.Text);
            if (dept != null)
            {
                if (type != null)
                {
                    if (type == "result")
                    {
                        name = "Final Result";
                        send();
                    }
                    else
                    {
                        if (m.Success && m2.Success)
                        {
                            try
                            {
                                String[] s = (beg_id.Text).Split("-".ToCharArray());
                                String[] s1 = (end_id.Text).Split("-".ToCharArray());
                                id1 =Convert.ToInt32(s[2]);
                                id2 = Convert.ToInt32(s1[2]); ;
                            } catch (StackOverflowException ee){}
                            if (id1 <= id2)
                            {
                                if (type == "text")
                                    path = msg.Text;
                                send();
                            }
                            else
                            {
                                errorMsg.setMsg("Error", "Ending id must be equal or greater then begnning id");
                                errorMsg.ShowDialog();
                            }
                        }
                        else
                        {
                            errorMsg.setMsg("Error", "Invalid id");
                            errorMsg.ShowDialog();
                        }
                    }
                }else
                {
                    errorMsg.setMsg("Error", "Select notice type");
                    errorMsg.ShowDialog();
                }
            }
            else
            {
                errorMsg.setMsg("Error","Select department");
                errorMsg.ShowDialog();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void send()
        {
            if (progress == null)
            {
                var context = TaskScheduler.FromCurrentSynchronizationContext();
                progress = new Progress();
                progress.Owner = this;
                progress.Show();
                ids.Clear();
                results.Clear();
                // MessageBox.Show(id1.ToString() + " " + id2.ToString(), "ids", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    if (type == "image" || type == "pdf")
                    {
                        path = httpReq.uploadFile(path);
                    }
                    if (path != null && !path.Equals("0"))
                    {
                        if (type == "result")
                        {
                            readDocx(path);
                        }
                        else
                        {
                            String ii = beg_id.Text;
                            ii = (ii.Substring(0, ii.LastIndexOf("-"))).Replace("-", "");
                            for (int i = id1; i <= id2; i++)
                            {
                                IdModel im = new IdModel();
                                im.id = ii + i.ToString();
                                ids.Add(im);
                            }
                            mme.header = path;
                        }
                        String json = createJsonArray();
                        Console.WriteLine("send ssd  " + json);
                        Console.ReadLine();
                        res = httpReq.sendNotice(this.phone, this.password, name, json, dept, type);
                        Console.WriteLine("send ssd  ");
                    }
                }).ContinueWith(_ =>
                {
                    dept = null;
                    path = null;
                    type = null;
                    progress.Close();
                    progress = null;
                    if (res != "0")
                    {
                        errorMsg.setMsg("Successful","Send");
                        errorMsg.ShowDialog();
                    }
                    else
                    {
                        errorMsg.setMsg("Error", "Failed");
                        errorMsg.ShowDialog();
                    }

                    Console.WriteLine("send result callBack " + res);
                }, context);

            }
            else errorMsg.Activate();
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void type_spinner_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = type_spinner.SelectedItem.ToString().ToLower();
            if (type =="result")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Doccx Files|*.Docx";
                openFileDialog1.Title = "Select a Docx File";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    Console.WriteLine("file chosser path " + openFileDialog1.FileName);
                }
                this.beg_id.Visible = false;
                this.end_id.Visible = false;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.msg.Visible = false;
            }else if (type == "image")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image|*.Jpg";
                openFileDialog1.Title = "Select a Image";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    Console.WriteLine("file chosser path " + openFileDialog1.FileName);
                }
                this.beg_id.Visible = true;
                this.end_id.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.msg.Visible = false;
            }
            else if (type == "pdf")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Pdf Files|*.pdf";
                openFileDialog1.Title = "Select a pdf File";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    Console.WriteLine("file chosser path " + openFileDialog1.FileName);
                }
                this.beg_id.Visible = true;
                this.end_id.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.msg.Visible = false;
            }
            else if (type == "text")
            {
                this.beg_id.Visible = true;
                this.end_id.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.msg.Visible = true;
            }
        }

        private void beg_id_TextChanged(object sender, EventArgs e)
        {
        }
        private void beg_id_Click(object sender, EventArgs e)
        {
            beg_id.Text = "";
        }
        private void end_id_Click(object sender, EventArgs e)
        {
            Match m = reg.Match(beg_id.Text);
            if (!m.Success)
            {
                label1.Text = "invalid id";
                errorMsg.setMsg("Error", "Invalid id");
                errorMsg.ShowDialog();
            }
            else label1.Text = "e,g 111-222-1";
            end_id.Text = "";
        }

        private void msg_TextChanged(object sender, EventArgs e)
        {

        }
        private Boolean readDocx(String docPath)
        {
            Boolean header = true;
            int s_conter = 0;
            int total_sub = 0;
            int result_attr = 3;
            String allSub = null;
            String hedMsg = null;
            int n = 0;
            String[] arr = new String[result_attr];
            String[] tem = new String[result_attr];
            String id = "";
            String[] sub = new String[4];
            ids.Clear();
            results.Clear();
            int c = 0;
            try
            {
                //string docPath = "E:\\CsharpResultText.docx";
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                Document doc = app.Documents.Open(docPath);
                String words = doc.Content.Text;
                //Console.Write("Docx texx " + words);
                doc.Close();
                app.Quit();
                using (StringReader reader = new StringReader(words))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (header && !line.Equals("Id"))
                        {
                            hedMsg = hedMsg + "\n" + line;
                        }
                        else if ((line.Contains("Id")) || (line.Contains("Subject")) || (line.Contains("Credit")) || (line.Contains("GPA")))
                        {
                            if (line.Contains("Subject"))
                                total_sub++;
                            if ((s_conter < result_attr) && !(line.Contains("Id")))
                            {
                                arr[s_conter] = line;
                                s_conter++;
                            }
                            if (header)
                            {
                                mme.header = hedMsg;
                            }
                            header = false;
                        }
                        else if (!line.Equals(" ") && !line.Equals("\n"))
                        {
                            line = line.Replace(" ", "");
                            line = line.Replace("\u0007", "");
                            Regex reg2 = new Regex(ID_PATTERN);
                            Match m2 = reg2.Match(line);
                            if (m2.Success)
                            {
                                results.Clear();
                                id = line;
                                allSub = null;
                                foreach (var item in arr)
                                {
                                    allSub = allSub + "  " + item;
                                }
                                allSub = allSub + "\n";
                            }
                            else if (!line.Equals(" ") && !line.Equals(""))
                            {
                                tem[n] = line;
                                if (n == (result_attr - 1))
                                {
                                    ResultModel rr = new ResultModel();
                                    rr.subject = tem[0];
                                    rr.credit = tem[1];
                                    rr.gpa = tem[2];
                                    results.Add(rr);
                                    n = 0;
                                }
                                else n++;
                                if (c == ((result_attr * total_sub) - 1))
                                {
                                    IdModel i = new IdModel();
                                    i.id = id;
                                    ids.Add(i);
                                    var sss = JsonConvert.SerializeObject(new { res = results });
                                    temlist.Add(JObject.Parse(sss));
                                    //Console.WriteLine("all sub "+sss);
                                    //Console.Write("id " + id + " " + sub[0] + " " + sub[2]);
                                    c = 0;
                                }
                                else c++;

                            }
                        }
                    }
                    //  Console.Write("Docx Header " + hedMsg);
                }

            }
            catch (Exception e) { Console.WriteLine("Docx texx exception  " + e); return false; }
            return true;
        }
        String createJsonArray()

        {
            return JsonConvert.SerializeObject(
                new
                {
                    StudentId = ids,
                    Header = mme,
                    Result = temlist
                });
        }
    }
}
