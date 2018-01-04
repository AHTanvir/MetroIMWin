using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace MetroImWin
{
    class HttpReqest
    {
        private String phone;
        private String Password;
        private String Authentication_SERVER_ADDRESS = "http://192.168.43.95/metroim/index.php";
       
        public String sendAuthenticatioReqest(String phone, String Password, String dept)
        {
            try {
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("phone", HttpUtility.UrlEncode(phone, Encoding.UTF8));
                webClient.QueryString.Add("password", HttpUtility.UrlEncode(Password, Encoding.UTF8));
                webClient.QueryString.Add("action", HttpUtility.UrlEncode("authenticateTeacher", Encoding.UTF8));
                webClient.QueryString.Add("dept", HttpUtility.UrlEncode(dept, Encoding.UTF8));
                return webClient.DownloadString(Authentication_SERVER_ADDRESS);
            } catch (WebException e) { return "0"; }
        }
        public String getInfo(String phone, String Password, String toPhone)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("phone", HttpUtility.UrlEncode(phone, Encoding.UTF8));
                webClient.QueryString.Add("password", HttpUtility.UrlEncode(Password, Encoding.UTF8));
                webClient.QueryString.Add("action", HttpUtility.UrlEncode("updateInfo", Encoding.UTF8));
                webClient.QueryString.Add("getFriendInfo", HttpUtility.UrlEncode(toPhone, Encoding.UTF8));
                return webClient.DownloadString(Authentication_SERVER_ADDRESS);
            }
            catch (WebException e)
            {
                return "0";
            }

        }
        public String userBlock(String phone, String Password, String toPhone,String b)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("phone", HttpUtility.UrlEncode(phone, Encoding.UTF8));
                webClient.QueryString.Add("password", HttpUtility.UrlEncode(Password, Encoding.UTF8));
                webClient.QueryString.Add("action", HttpUtility.UrlEncode("userBlock", Encoding.UTF8));
                webClient.QueryString.Add("block", HttpUtility.UrlEncode(toPhone, Encoding.UTF8));
                webClient.QueryString.Add("status", HttpUtility.UrlEncode(b, Encoding.UTF8));
                return webClient.DownloadString(Authentication_SERVER_ADDRESS);
            }
            catch (WebException e)
            {
                return "0";
            }

        }
        public String sendNotice(String phone, String Password, String name, String json,String dept,String type)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("phone", HttpUtility.UrlEncode(phone, Encoding.UTF8));
                webClient.QueryString.Add("password", HttpUtility.UrlEncode(Password, Encoding.UTF8));
                webClient.QueryString.Add("action", HttpUtility.UrlEncode("sendNotice", Encoding.UTF8));
                webClient.QueryString.Add("dept", HttpUtility.UrlEncode(dept, Encoding.UTF8));
                webClient.QueryString.Add("array", HttpUtility.UrlEncode(json, Encoding.UTF8));
                webClient.QueryString.Add("name", HttpUtility.UrlEncode(name, Encoding.UTF8));
                webClient.QueryString.Add("type", HttpUtility.UrlEncode(type, Encoding.UTF8));
                return webClient.DownloadString(Authentication_SERVER_ADDRESS);
            }
            catch (WebException e) {
                Console.WriteLine("web exception "+e);
                return "0"; }

        }
        public String uploadFile(String filePath)
        {
            try
            {
                HttpWebRequest requestToServerEndpoint =
(HttpWebRequest)WebRequest.Create("http://192.168.43.95/metroim/upload.php");
                string boundaryString = "----SomeRandomText";
                string fileUrl = @"d:\te.txt";
                requestToServerEndpoint.Method = WebRequestMethods.Http.Post;
                requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
                requestToServerEndpoint.KeepAlive = true;
                requestToServerEndpoint.Credentials = System.Net.CredentialCache.DefaultCredentials;
                MemoryStream postDataStream = new MemoryStream();
                StreamWriter postDataWriter = new StreamWriter(postDataStream);
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
                "myFileDescription",
                "A sample file description");
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data;"
                + "name=\"{0}\";"
                + "filename=\"{1}\""
                + "\r\nContent-Type: {2}\r\n\r\n",
                "myFile",
                Path.GetFileName(filePath),
                Path.GetExtension(filePath));
                postDataWriter.Flush();
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();

                postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                postDataWriter.Flush();
                requestToServerEndpoint.ContentLength = postDataStream.Length;
                using (Stream s = requestToServerEndpoint.GetRequestStream())
                {
                    postDataStream.WriteTo(s);
                }
                postDataStream.Close();
                WebResponse response = requestToServerEndpoint.GetResponse();
                StreamReader responseReader = new StreamReader(response.GetResponseStream());
                string replyFromServer = responseReader.ReadToEnd();
                return replyFromServer;
                return "end";
            }
            catch (WebException e)
            {
                return "0";
            }catch (Exception e) { return "0"; }
        }
    }
}
