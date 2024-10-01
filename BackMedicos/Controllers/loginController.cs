using BackMedicos.DataBase;
using BackMedicos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly jwtConfig _jwtConfig;

        public loginController(ApplicationDbContext context, jwtConfig jwtConfig)
        {
            _context = context;
            _jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Registro")]
        public async Task<IActionResult> Post(Usuarios usuario)
        {
            var modelUsuario = new Usuarios
            {
                UserName = usuario.UserName,
                Password = _jwtConfig.configJWT(usuario.Password),
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Rol = usuario.Rol                
            };
            await _context.Usuarios.AddAsync(modelUsuario);
            await _context.SaveChangesAsync();
            return Ok(modelUsuario);
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(Login login)
        {
            var usuarioEncontrado = await _context.Usuarios
                                    .Where(u =>
                                    u.UserName == login.UserName &&
                                    u.Password == _jwtConfig.configJWT(login.Password)
                                    ).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _jwtConfig.generateJWT(usuarioEncontrado) });
        }
    }
}
