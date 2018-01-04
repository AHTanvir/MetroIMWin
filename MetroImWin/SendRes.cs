using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MetroImWin
{
    public partial class SendRes : Form
    {

        public SendRes(String phone, String password)
        {
            this.phone = phone;
            this.password = password;
            InitializeComponent();
        }
        private WindowsFormsSynchronizationContext context;
        private String  phone;
        private String password;
        private String dept;
        private String path;
        private List<IdModel> ids = new List<IdModel>();
        private List<ResultModel> results = new List<ResultModel>();
        private List<JObject> temlist = new List<JObject>();
        private Message mme = new Message();
        private String ID_PATTERN = "(\\d{7})";
        private Progress progress;
        private String res ="0";
        private DialogMessage errorMsg;
        private HttpReqest httpReq = new HttpReqest();
        private void dept_spinner_SelectedIndexChanged(object sender, EventArgs e)
        {
             dept = dept_spinner.SelectedItem.ToString().ToLower();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Doccx Files|*.Docx";
            openFileDialog1.Title = "Select a Docx File";
  
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path=openFileDialog1.FileName;
                Console.WriteLine("file chosser path "+ openFileDialog1.FileName);
            }
        }
        private Boolean readDocx(String docPath)
        {
            Boolean header = true;
            int s_conter = 0;
            int total_sub = 0;
            int result_attr = 3;
            String allSub = null;
            String hedMsg=null;
        int n = 0;
            String[] arr = new String[result_attr];
            String[] tem = new String[result_attr];
            String id ="";
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
                                if (n == (result_attr-1))
                                {
                                    ResultModel rr = new ResultModel();
                                    rr.subject = tem[0];
                                    rr.credit =tem[1];
                                    rr.gpa = tem[2];
                                    results.Add(rr);
                                    n = 0;
                                }else n++;
                                if (c ==((result_attr*total_sub)-1))
                                {
                                    IdModel i = new IdModel();
                                    i.id = id;
                                    ids.Add(i);
                                    var sss = JsonConvert.SerializeObject(new { res=results});
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
            catch (Exception e) { Console.WriteLine("Docx texx exception  " + e ); return false; }
            return true;
        }
        String  createJsonArray()
        
        {
            return   JsonConvert.SerializeObject(
                new
                {
                    StudentId = ids,
                    Header = mme,
                    Result = temlist
                });
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (dept != null && !dept.Equals(""))
            {
                if (progress == null)
                {
                    var context = TaskScheduler.FromCurrentSynchronizationContext();
                    progress = new Progress();
                    progress.Owner = this;
                    progress.Show();

                    System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        if (readDocx(path))
                        {
                            String json = createJsonArray();
                            Console.WriteLine("send ssd  " + json);
                            res = httpReq.sendNotice(this.phone,this.password,"Final Result",json,dept,"result");
                        }
                        Console.WriteLine("send ssd  " );
                    }).ContinueWith(_ => {
                        progress.Close();
                        progress = null;
                        if (res != "0")
                        {
                            errorMsg = new DialogMessage("Successful", "Result send");
                            errorMsg.ShowDialog();
                        }
                        else showErrorDialog("Failed to send");


                        Console.WriteLine("send result callBack "+res);
                    }, context);
                }
            }
            else showErrorDialog("Select department");
        }
        private void showErrorDialog(String msg)
        {
            errorMsg = new DialogMessage(msg);
            errorMsg.ShowDialog();
        }

        private void SendRes_Load(object sender, EventArgs e)
        {

        }
    }
}
