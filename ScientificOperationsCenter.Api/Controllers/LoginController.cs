using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.DAL.Interfaces;


namespace ScientificOperationsCenter.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var token = await _loginRepository.LoginAsync(userLogin);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
