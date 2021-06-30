using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BF1RecordQuery.Data
{
    class GameToolsApi
    {
        public static Task<string> GetAllData(string nameStr)
        {
            return Task.Run(() =>
            {
                nameStr = "https://api.gametools.network/bf1/all/?name=" + nameStr + "&lang=zh-tw";

                WebClient wb = new WebClient();
                {
                    wb.Encoding = Encoding.UTF8;
                    wb.Proxy = null;
                    nameStr = wb.DownloadString(new Uri(nameStr));

                    return nameStr;
                }

                #region HttpWebRequest
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(nameStr);
                //request.Proxy = null;
                //request.KeepAlive = false;
                //request.Method = "GET";
                //request.ContentType = "application/json; charset=UTF-8";
                //request.AutomaticDecompression = DecompressionMethods.GZip;

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream myResponseStream = response.GetResponseStream();
                //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //string retString = myStreamReader.ReadToEnd();

                //myStreamReader.Close();
                //myResponseStream.Close();

                //if (response != null)
                //{
                //    response.Close();
                //}
                //if (request != null)
                //{
                //    request.Abort();
                //}

                //return retString;
                #endregion
            });
        }

        public static Task<string> GetServers(string nameStr)
        {
            return Task.Run(() =>
            {
                nameStr = "https://api.gametools.network/bf1/servers/?name=" + nameStr + "&lang=zh-tw&region=all&platform=pc";

                WebClient wb = new WebClient();
                {
                    wb.Encoding = Encoding.UTF8;
                    wb.Proxy = null;
                    nameStr = wb.DownloadString(new Uri(nameStr));

                    return nameStr;
                }
            });
        }
    }
}
