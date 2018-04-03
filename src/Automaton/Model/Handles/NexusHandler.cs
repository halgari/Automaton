using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using HtmlAgilityPack;

namespace Automaton.Model
{
    class NexusHandler
    {
        public static HttpClient NexusLoginInstance { get; set; }

        private const string LoginUrl = "https://www.nexusmods.com/Sessions/?Login";
        private const string DownloadUrl = @"https://www.nexusmods.com/skyrim/download/";

        /// <summary>
        /// Attempts to log current HttpClient into the NexusMods servers. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Will return true if successful, false if not.</returns>
        public static async Task<bool> AttemptNexusLogIn(string username, string password)
        {
            NexusLoginInstance = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            var loginResult = await NexusLoginInstance.PostAsync(new Uri(LoginUrl), formContent);
            var isLoggedIn = loginResult.Headers.ToList()[1].Key != "NexusLoginErrorMessage";

            return isLoggedIn;
        }

        public static async Task<string> AttempFindDownloadPath(string nxmString)
        {
            var splitNxm = nxmString.Split('/');
            var downloadPage = DownloadUrl + splitNxm[splitNxm.Length - 1];
            var downloadPageHtml = GetDownloadPage(downloadPage, NexusLoginInstance).Result;
            var htmlDoc = new HtmlDocument();

            return "";
        }

        public static async Task<bool> StartFileDownload(string downloadLink)
        {
            var nexusDownloadUpdate = new NexusDownload()
            {
                DownloadPath = downloadLink,
                FileName = downloadLink,
                FileSize = downloadLink,
                DownloadStatus = new NexusDownloadStatus()
                {
                    CurrentSpeed = "",
                    PercentageComplete = 0
                }
            };

            var webClient = new WebClient();

            Messenger.Default.Send(nexusDownloadUpdate, NexusDownloadUpdate.Update);

            return false;
        }

        private static async Task<string> GetDownloadPage(string downloadPage, HttpClient httpClient)
        {
            using (var response = await httpClient.GetAsync(downloadPage))
            {
                using (var content = response.Content)
                {
                    var result = content.ReadAsStringAsync();

                    return result.Result;
                }
            }
        }
    }

    class NexusDownload
    {
        public string DownloadPath { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public NexusDownloadStatus DownloadStatus { get; set; }
    }

    class NexusDownloadStatus
    {
        public string CurrentSpeed { get; set; }
        public int PercentageComplete { get; set; }
    }

    enum NexusDownloadUpdate
    {
        Update
    }
}
