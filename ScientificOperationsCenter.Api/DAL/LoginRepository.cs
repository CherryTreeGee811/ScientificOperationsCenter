using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL
{
    public class LoginRepository(
            HttpClient httpClient
        ) : ILoginRepository
    {
        private readonly HttpClient _httpClient = httpClient;


        public async Task<string> LoginAsync(UserLogin userLogin)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://scientificoperationscenter.auth:8060/auth/login", userLogin);
                response.EnsureSuccessStatusCode();
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return tokenResponse?.Token ?? string.Empty;
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
