using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using GalaSoft.MvvmLight.Messaging;

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

        public static async Task<string> AttemptFindDownloadPath(string nxmString)
        {
            var splitNxm = nxmString.Split('/');
            var downloadPage = DownloadUrl + splitNxm[splitNxm.Length - 1];
            var downloadPageHtml = await GetDownloadPage(downloadPage, NexusLoginInstance);

            var htmlParser = new HtmlParser();
            var html = await htmlParser.ParseAsync(downloadPageHtml);

            var matchingElement = html.All.First(x => x.Id == "dl_link");
            var matchingElementValue = matchingElement.GetAttribute("value");

            return matchingElementValue;
        }

        public static async Task<bool> StartFileDownload(string downloadLink, CancellationTokenSource cancellationToken)
        {
            var nexusDownloadUpdate = new NexusDownload()
            {
                CancellationTokenSource = cancellationToken,
                FileName = GetFileName(downloadLink),
                FileSize = "",
                DownloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetFileName(downloadLink)),
                DownloadStatus = new NexusDownloadStatus()
                {
                    CurrentSpeed = "",
                    PercentageComplete = 0,
                    TotalDownloaded = ""
                }
            };

            // Broadcast update
            //Messenger.Default.Send(nexusDownloadUpdate, NexusDownloadUpdate.Update);

            using (var webClient = new WebClient())
            {
                webClient.DownloadProgressChanged += (sender, args) =>
                {
                    nexusDownloadUpdate.FileSize = args.TotalBytesToReceive.ToString();
                    nexusDownloadUpdate.DownloadStatus.PercentageComplete = args.ProgressPercentage;
                    nexusDownloadUpdate.DownloadStatus.TotalDownloaded = args.BytesReceived.ToString();
                };

                webClient.DownloadFileAsync(new Uri(downloadLink), nexusDownloadUpdate.DownloadPath);
            }

            return true;
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

        private static string GetFileName(string fileUrl)
        {
            var splitFileUrl = fileUrl.Split('/');
            var lastClump = splitFileUrl[splitFileUrl.Length - 1];

            var fileName = lastClump.Substring(0, lastClump.LastIndexOf("?"));

            return HttpUtility.UrlDecode(fileName);
        }
    }

    class NexusDownload
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public string DownloadPath { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public NexusDownloadStatus DownloadStatus { get; set; }
    }

    class NexusDownloadStatus
    {
        public string CurrentSpeed { get; set; }
        public string TotalDownloaded { get; set; }
        public int PercentageComplete { get; set; }
    }

    enum NexusDownloadUpdate
    {
        Update
    }
}
