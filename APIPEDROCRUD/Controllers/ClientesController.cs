using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPEDROCRUD.Models;
using Newtonsoft.Json;

namespace APIPEDROCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientesController : Controller
    {
        private readonly BdcadastroContext _context;

        public ClientesController(BdcadastroContext context)
        {
            _context = context;
        }

        [HttpPost("FiltrarJSON")]
        public IActionResult FiltraMedBeauty(Jsons json)
        {
            dynamic jsonParse = JsonConvert.DeserializeObject(json.json);

            List<string> ceps = JsonConvert.DeserializeObject<List<string>>(jsonParse.values);

            List<string> filtrada = ceps.FindAll(x => x == json.cep);
            return Ok(filtrada);

        }

        [HttpGet("telefone")]

        public async Task<IActionResult> Get(string telefone)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Telefone == telefone);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Cliente cliente)
        {
            if (cliente.Uf.Length != 2)
                return BadRequest("A Uf precisa ter 2 dígitos");
            if (cliente.Cep.Length != 8)
                return BadRequest("o CEP precisa ter 8 dígitos");
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]

        public async Task<IActionResult> Put(int Id,Cliente clientedata)
        {
            if (clientedata == null || Id == 0)
            {
                return BadRequest();
            }
            if (clientedata.Uf.Length != 2)
                return BadRequest("A Uf precisa ter 2 dígitos");
            if (clientedata.Cep.Length != 8)
                return BadRequest("o CEP precisa ter 8 dígitos");

            var cliente = await _context.Clientes.FindAsync(Id);

            if (cliente == null)
                return BadRequest();
            cliente.Nome = clientedata.Nome;
            cliente.Cep = clientedata.Cep;
            cliente.Telefone = clientedata.Telefone;
            cliente.Cidade = clientedata.Cidade;
            cliente.Bairro = clientedata.Bairro;
            cliente.Endereco = clientedata.Endereco;
            cliente.Uf = clientedata.Uf;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return BadRequest();
            _context.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok();

        }
        
    }
}
