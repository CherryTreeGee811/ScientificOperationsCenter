using System.Text.Json.Serialization;

namespace AuthorizationServer.Models
{
    public class UserLogin
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }


        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
