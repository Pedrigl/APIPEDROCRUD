using APIPEDROCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace APIPEDROCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : Controller
    {
        private readonly LoginContext _Context;

        public LoginController(LoginContext context)
        {
            _Context = context;
        }
        [HttpGet("user/{username}/password/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = System.Security.Cryptography.SHA256.Create().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            password = hash;
            var user = await _Context.Logins.FirstOrDefaultAsync(m => m.User == username && m.Password == password);

            return Ok(user);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(Login login)
        {
            var check = _Context.Logins.ToArray().Where(l => l.User == login.User);
            if(check.Count() > 0)
            {
                return BadRequest("Esse usuário já foi escolhido por favor, tente um usuário diferente");
            }
            
            byte[] data = System.Text.Encoding.ASCII.GetBytes(login.Password);
            data = System.Security.Cryptography.SHA256.Create().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            login.Password = hash;
            _Context.Add(login);
            await _Context.SaveChangesAsync();
            return Ok(login);

        }
    }
}
