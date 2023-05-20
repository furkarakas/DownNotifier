using System.Net;

namespace DownNotifier.BackgroundJob.CheckUrl
{
    public class CheckUrlService : ICheckUrlService
    {
        public bool CheckUrl(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
            request.Method = "HEAD"; 
            HttpWebResponse response = request.GetResponse() as HttpWebResponse; 
            var result= (response.StatusCode == HttpStatusCode.OK);
            response.Close();

            return result;
        }
    }
}
