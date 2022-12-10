using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]

        public async Task<IActionResult> Put(Cliente clientedata)
        {
            if (clientedata == null || clientedata.Id == 0)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes.FindAsync(clientedata.Id);

            if (cliente == null)
                return BadRequest();
            cliente.Nome = clientedata.Nome;
            cliente.Cep = clientedata.Cep;
            cliente.Telefone = clientedata.Telefone;
            cliente.Cidade = clientedata.Cidade;
            cliente.Bairro = clientedata.Bairro;
            cliente.Endereco = clientedata.Endereco;

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
