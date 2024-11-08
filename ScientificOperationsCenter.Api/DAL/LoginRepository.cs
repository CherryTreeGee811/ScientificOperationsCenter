using AuthorizationServer.Models;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpClient _httpClient;


        public LoginRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> LoginAsync(UserLogin userLogin)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://scientificoperationscenter.auth:8060/auth/login", userLogin);
                response.EnsureSuccessStatusCode();
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return tokenResponse.Token;
            }
            catch (HttpRequestException httpEx)
            {
                // Log HttpRequestException for debugging
                Console.WriteLine(httpEx);
                throw new Exception("Error communicating with authentication service.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
