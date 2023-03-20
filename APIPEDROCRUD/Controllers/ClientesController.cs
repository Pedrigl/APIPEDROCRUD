using Microsoft.AspNetCore.Mvc;
using APIPEDROCRUD.Models;

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


        [HttpGet("telefone/{telefone}")]

        public async Task<IActionResult> Get(string telefone)
        {
            var cliente = _context.Clientes.ToArray().Where(m => m.Telefone == telefone);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost()]

        public async Task<IActionResult> Post(Cliente cliente)
        {
            if (cliente.Uf.Length != 2)
                return BadRequest("A Uf precisa ter 2 dígitos");
            if (cliente.Cep.Length != 8)
                return BadRequest("o CEP precisa ter 8 dígitos");
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpPut("Id/{Id}")]

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

        [HttpDelete("Id/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id < 1)
                return BadRequest();
            var cliente = await _context.Clientes.FindAsync(Id);
            if (cliente == null)
                return BadRequest();
            _context.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok();

        }
        
    }

}
