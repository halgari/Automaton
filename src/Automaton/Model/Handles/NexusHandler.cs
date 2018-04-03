using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.Model
{
    class NexusHandler
    {
        public static HttpClient NexusLoginInstance { get; set; }

        private const string LoginUrl = "https://www.nexusmods.com/Sessions/?Login";

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

        public static void ModifyRegistryProtocol()
        {
        }
    }
}
