using System.Text.Json.Serialization;

namespace ScientificOperationsCentre.Auth.Models
{
    public class UserLogin
    {
        [JsonPropertyName("username")]
        public string? UserName { get; set; }


        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
