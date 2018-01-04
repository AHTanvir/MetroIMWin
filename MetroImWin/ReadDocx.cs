using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MetroImWin
{
    class ReadDocx
    {
        private List<IdModel> ids = new List<IdModel>();
        private List<ResultModel> results = new List<ResultModel>();
        private Message mme= new Message();
        private String hedMsg;
        private String ID_PATTERN = "(\\d{7})";
        public ReadDocx()
        {
           ttt();
            createJsonArray();
        }
        private void ttt()
        {
            Boolean header = true;
            int s_conter = 0;
            int total_sub = 0;
            int result_attr = 3;
            String allSub=null;
            int n = 0;
            String[] arr = new String[result_attr];
            String id="";
            String[] sub = new String[4];
            int c = 0;
            try
            {
                string docPath = "E:\\CsharpResultText.docx";
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
                            if ((s_conter <result_attr) && !(line.Contains("Id")))
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
                        else if(!line.Equals(" ") &&!line.Equals("\n"))
                        {
                            line = line.Replace(" ","");
                            line = line.Replace("\u0007", "");
                            Regex reg2 = new Regex(ID_PATTERN);
                            Match m2 = reg2.Match(line);
                            if (m2.Success)
                            { id = line;
                                allSub=null;
                                foreach (var item in arr)
                                {
                                    allSub = allSub + "  " + item;
                                }
                                allSub = allSub + "\n";
                            }
                            else
                            {
                                if (c == 1) 
                                    allSub =allSub+line;
                                else allSub = allSub + ",  "+line;
                                if (n == result_attr)
                                {
                                    allSub = allSub + "\n";
                                    n = 0;
                                }  n++;
                                if (c == 9)
                                {
                                    IdModel i = new IdModel();
                                    i.id=id;
                                    ids.Add(i);
                                    ResultModel rr = new ResultModel();
                                   // rr.result = allSub;
                                    results.Add(rr);
                                   // Console.WriteLine("all sub "+allSub);
                                    //Console.Write("id " + id + " " + sub[0] + " " + sub[2]);
                                    c = 0;
                                }else c++;

                            }
                        }
                    }
                  //  Console.Write("Docx Header " + hedMsg);
                }

            }
            catch (Exception e) { Console.WriteLine("Docx texx exception  " + e); }
        }
        void createJsonArray()
        {
            var json = JsonConvert.SerializeObject(
                new {
                    StudentId = ids,
                    Header = mme,
                    Result = results
                });
            //var jsonSerialiser = new JavaScriptSerializer();
            //  var json = jsonSerialiser.Serialize(data);
            HttpReqest h = new HttpReqest();
           String res= h.sendNotice("+8801736549421","anwar","test",json,"cse","result");

            Console.Write("Http Request callback "+res);
          //  Console.Write("jasson array " + res);
        }

    }
}
