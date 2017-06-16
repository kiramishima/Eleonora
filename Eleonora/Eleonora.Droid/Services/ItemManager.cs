using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Eleonora.Droid.Services
{
    public class ItemManager
    {
        MobileServiceClient client;
        IMobileServiceTable<TorneoItem> todoTable;
        public ItemManager()
        {
            client = new MobileServiceClient(@"https://xamarinchampions.azurewebsites.net/");
            todoTable = client.GetTable<TorneoItem>();
        }
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
        public async Task SaveTaskAsync(TorneoItem item)
        {
            if (item.Id == null)
            {
                await todoTable.InsertAsync(item);
            }
            else
            {
                await todoTable.UpdateAsync(item);
            }
        }
    }

    public class TorneoItem
    {
        private string _id;
        private string _email;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        [JsonProperty(PropertyName = "Email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }
        public string Reto { get; set; }
        public string DeviceId { get; set; }
        [Version]
        public string Version { get; set; }
    }
}