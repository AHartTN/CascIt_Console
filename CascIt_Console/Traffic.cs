namespace CascIt_Console
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    /// <summary>
    /// This class handled the various traffic that occurrs via applications that AH Operations develops.
    /// </summary>
    public static class Traffic
    {
        public static string HTTPGET(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 100000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                if (responseStream != null)
                    using (StreamReader resStream = new StreamReader(responseStream))
                        return resStream.ReadToEnd();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(e);
                return null;
            }
        }

        public static string HTTPPOST(string url, string postData)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "x-www-form-urlencoded";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                using (Stream requestStream = webRequest.GetRequestStream())
                    requestStream.Write(byteArray, 0, byteArray.Length);
                using (Stream responseStream = webRequest.GetResponse().GetResponseStream())
                    if (responseStream != null)
                        using (StreamReader responseReader = new StreamReader(responseStream))
                            return responseReader.ReadToEnd();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(postData);
                Console.WriteLine(e);
                return null;
            }
        }

        public static T JsonRPCGET<T>(string url) where T : class
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json-rpc; charset=utf-8";
            request.Accept = "application/json-rpc, text/javascript, */*";
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            string json = "";

            if (stream != null)
                using (StreamReader sr = new StreamReader(stream))
                    while (!sr.EndOfStream)
                        json += sr.ReadLine();
            return Deserialize<T>(json);
        }

        public static string JsonRPCPOST(string url, string strJson)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json, text/javascript, */*";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(strJson);
            request.ContentLength = byteArray.Length;

            using (Stream s = request.GetRequestStream())
                s.Write(byteArray, 0, byteArray.Length);

            string json = "";
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            if (stream != null)
                using (StreamReader sr = new StreamReader(stream))
                    while (!sr.EndOfStream)
                        json += sr.ReadLine();
            return json;
        }

        public static T JsonRPCPost<T>(string url, string strJson) where T : class
        {
            string response = JsonRPCPOST(url, strJson);
            T result = Deserialize<T>(response);
            return result;
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
                return null;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        public static T Deserialize<T>(string source) where T : class
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            T result = jss.Deserialize<T>(source);
            return result;
        }

        public static dynamic DynamicDeserialize(string source)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            return jss.Deserialize(source, typeof(object));
        }
    }
}
