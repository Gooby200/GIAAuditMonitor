using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace GIAAuditMonitor
{
    public class Manager
    {
        public const string OCTOPUS_LOGS_DIRECTORY = @"C:\Users\rpesa\Downloads\TaskLogs\TaskLogs\";
        public const string AUDIT_LOG_DIRECTORY = @"C:\GIAAuditMonitor\Logs\";

        public static async Task<string> GetAsyncInfo(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string username = UserCredentials.USERNAME;
            string password = UserCredentials.PASSWORD;
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static bool StoreRelationFileServer(Relation relation) {
            try {
                string todayAuditDirectory = Manager.AUDIT_LOG_DIRECTORY + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
                if (relation.OctopusRelease != null) {
                    File.AppendAllText(todayAuditDirectory + relation.OctopusRelease.ConvertNullToString() + ".txt", relation.ToString());
                    return true;
                }
            } catch (Exception ex) {
                return false;
            }
            return false;
        }
    }

    public static class Extensions {
        public static string ConvertNullToString(this object str) {
            if (str == null) {
                return "";
            } else {
                return str.ToString();
            }
        }
    }
}