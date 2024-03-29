﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPEDROCRUD.Models.Cadastro_1;

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

        [HttpGet("all")]

        public async Task<List<Cliente>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes;
        }


        [HttpGet()]

        public IEnumerable<Cliente> Get(string telefone)
        {
            var cliente = _context.Clientes.ToArray().Where(m => m.Telefone == telefone);
            if (cliente == null)
                return null;

            return cliente;
        }

        [HttpPost()]

        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                if (int.Parse(cliente.Telefone.Substring(2)) != 9)
                    return BadRequest("Telefones precisam ter DDD e o primeiro numero tem que ser 9 \r\n Exemplo:xx9xxxxxxxx");
                if (cliente.Uf.Length != 2)
                    return BadRequest("A Uf precisa ter 2 dígitos");
                if (cliente.Cep.Length != 8)
                    return BadRequest("o CEP precisa ter 8 dígitos");

                _context.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }

            return Ok(cliente);
        }

        [HttpPut()]

        public async Task<IActionResult> Put(int Id, Cliente clientedata)
        {
            if (clientedata == null || Id == 0)
            {
                return BadRequest();
            }

            if (int.Parse(clientedata.Telefone.Substring(2)) != 9)
                return BadRequest("Telefones precisam ter DDD e o primeiro numero tem que ser 9 \r\n Exemplo:xx9xxxxxxxx");
            if (clientedata.Uf.Length != 2)
                return BadRequest("A Uf precisa ter 2 dígitos");
            if (clientedata.Cep.Length != 8)
                return BadRequest("o CEP precisa ter 8 dígitos");

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.ID == Id);

            if (cliente == null)
                return NotFound();
            cliente.Nome = clientedata.Nome;
            cliente.Cep = clientedata.Cep;
            cliente.Telefone = clientedata.Telefone;
            cliente.Cidade = clientedata.Cidade;
            cliente.Bairro = clientedata.Bairro;
            cliente.Endereco = clientedata.Endereco;
            cliente.Uf = clientedata.Uf;

            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpDelete()]
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
