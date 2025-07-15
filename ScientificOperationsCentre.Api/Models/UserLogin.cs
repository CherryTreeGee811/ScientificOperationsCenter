using System.Text.Json.Serialization;


namespace ScientificOperationsCentre.Api.Models
{
    public class UserLogin
    {
        [JsonPropertyName("username")]
        public string? UserName { get; set; }


        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
