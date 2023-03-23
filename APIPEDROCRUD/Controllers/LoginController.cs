using APIPEDROCRUD.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet()]
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
            var check = await _Context.Logins.FirstOrDefaultAsync(l => l.User == login.User);
            if(check != null)
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

        [HttpPut()]

        public async Task<IActionResult> Change(int Id, Login LoginPatch)
        {
            if (LoginPatch == null || Id <= 0)
            {
                return BadRequest();
            }

            var check = await _Context.Logins.FirstOrDefaultAsync(l => l.User == LoginPatch.User);
            if (check != null && check.Id != Id)
            {
                return BadRequest("Esse usuário já foi escolhido por favor, tente um usuário diferente");
            }

            var user = await _Context.Logins.FirstOrDefaultAsync(l => l.Id == Id);

            if (user == null)
                return NotFound();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(LoginPatch.Password);
            data = System.Security.Cryptography.SHA256.Create().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);

            user.User = LoginPatch.User;
            user.Name = LoginPatch.Name;
            user.Password = hash;

            await _Context.SaveChangesAsync();

            return Ok(user);

        }

        [HttpDelete()]

        public async Task<IActionResult> Delete(int Id)
        {
            if(Id <= 0)
            {
                return BadRequest();
            }

            var user = await _Context.Logins.FirstOrDefaultAsync(l => l.Id == Id);

            if (user == null)
                return NotFound();

            try
            {
                _Context.Remove(user);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
                
            return Ok();
        }
    }
}
