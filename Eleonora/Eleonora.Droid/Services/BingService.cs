using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net.Http;

namespace Eleonora.Droid.Services
{
    public class BingService
    {
        const string subscriptionKey = "25f95843fc00499e985d68d988e701fe";
        const string uriBase = @"https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";

        public static async Task<string> FetchTokenAsync(string search)
        {
            var queryString = new Dictionary<string, string>();
            using (var client = new HttpClient())
            {
                queryString.Add("q", search);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                var uri = "https://bingapis.azure-api.net/api/v5/search/?  "
                    + "q=" + queryString["q"];

                var response = await client.GetAsync(uri);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}