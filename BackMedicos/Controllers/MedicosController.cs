using BackMedicos.DataBase;
using BackMedicos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("admin"))]
    public class MedicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _context.Medicos.ToListAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Medicos.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Medicos medico)
        {
            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();
            return Ok(medico);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Medicos medico)
        {
            var medicoInfo = _context.Medicos.SingleOrDefault(x => x.Id == id);
            if (medicoInfo == null)
                return NotFound();
            medicoInfo.Nombre = medico.Nombre;
            medicoInfo.Email = medico.Email;
            medicoInfo.Matricula = medico.Matricula;
            medicoInfo.Telefono = medico.Telefono;
            medicoInfo.Especialidad = medico.Especialidad;
            medicoInfo.Estado = medico.Estado;
            _context.Attach(medicoInfo);
            await _context.SaveChangesAsync();
            return Ok(medicoInfo);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicoInfo = _context.Medicos.SingleOrDefault(x => x.Id == id);
            if (medicoInfo == null)
                return NotFound();
            _context.Medicos.Remove(medicoInfo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
    
