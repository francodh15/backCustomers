using BackMedicos.DataBase;
using BackMedicos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("admin"))]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        
        public async Task<ActionResult> Get()
        {
            var result = await _context.Clientes.ToListAsync();
            if (result == null)
                return NotFound();
            return Ok(result) ;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Clientes.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Clientes cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Clientes cliente)
        {
            var clienteInfo = _context.Clientes.SingleOrDefault(x => x.Id == id);
            if(clienteInfo == null)
                return NotFound();
            clienteInfo.Nombre = cliente.Nombre;
            clienteInfo.Email = cliente.Email;
            clienteInfo.Dni = cliente.Dni;
            clienteInfo.Telefono = cliente.Telefono;
            clienteInfo.Observaciones = cliente.Observaciones;
            _context.Attach(clienteInfo);
            await _context.SaveChangesAsync();
            return Ok(clienteInfo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteInfo = _context.Clientes.SingleOrDefault(x => x.Id == id);
            if (clienteInfo == null)
                return NotFound();
            _context.Clientes.Remove(clienteInfo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
