using System.Text.Json.Serialization;

namespace ScientificOperationsCenter.Auth.Models
{
    public class UserLogin
    {
        [JsonPropertyName("username")]
        public string? UserName { get; set; }


        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
