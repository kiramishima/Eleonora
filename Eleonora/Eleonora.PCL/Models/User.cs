using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Eleonora.Core.Models
{
    public class User
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

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "user_picture")]
        public string UserPicture { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
