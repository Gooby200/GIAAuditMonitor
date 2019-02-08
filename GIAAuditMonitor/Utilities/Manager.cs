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
            string username = "*****";
            string password = "*****";
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
    }
}