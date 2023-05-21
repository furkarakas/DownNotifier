using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DownNotifier.BackgroundJob.CheckUrl
{
    public class CheckUrlService : ICheckUrlService
    {
        public bool CheckUrl(string url)
        {
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
            //request.Method = "HEAD"; 
            //HttpWebResponse response = request.GetResponse() as HttpWebResponse; 
            //var result= (response.StatusCode == HttpStatusCode.OK);
            //response.Close();

            //return result;

            ServicePointManager.MaxServicePointIdleTime = 1000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 15000;
            request.Method = "GET";
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            System.Net.ServicePointManager.Expect100Continue = false;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if(!((int)response.StatusCode >= 200 && (int)response.StatusCode <= 299))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
