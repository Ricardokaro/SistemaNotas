using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaNotas.Datos;
using SistemaNotas.Entidades.Usuarios;
using SistemaNotas.Web.Models.Usuarios;
using SistemaNotas.Web.Models.Usuarios.Usuario;


namespace SistemaNotas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;
        private readonly IConfiguration _config;

        public UsuariosController(DbContextSistemaNotas context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Usuarios/Listar
        //[Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioVewModel>> Listar()
        {
            var usuario = await _context.usuarios.Include(u => u.rol).ToListAsync();

            return usuario.Select(u => new UsuarioVewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                primer_nombre = u.primer_nombre,
                segundo_nombre = u.segundo_nombre,
                primer_apellido = u.primer_apellido,
                segundo_apellido = u.segundo_apellido,
                correo_electronico = u.correo_electronico,
                celular = u.celular,
                direccion = u.direccion,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }

        // GET: api/Usuarios/ListarDocentes
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioVewModel>> ListarDocentes()
        {
            //string consulta =  _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente").ToString();
            var usuario = await _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente").ToListAsync();

            return usuario.Select(u => new UsuarioVewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                primer_nombre = u.primer_nombre,
                segundo_nombre = u.segundo_nombre,
                primer_apellido = u.primer_apellido,
                segundo_apellido = u.segundo_apellido,
                correo_electronico = u.correo_electronico,
                celular = u.celular,
                direccion = u.direccion,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }

        // GET: api/Usuarios/SelectDocentes
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectDocenteViewModel>> SelectDocentes()
        {
            //string consulta =  _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente").ToString();
            var docente = await _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente" && u.condicion == true).ToListAsync();

            return docente.Select(u => new SelectDocenteViewModel
            {
                iddocente = u.idusuario,
                nombre = u.primer_nombre + " " + u.primer_apellido + " " + u.segundo_apellido 
            });
        }

        // GET: api/Usuarios/ListarEstudiantes
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioVewModel>> ListarEstudiantes()
        {
            //string consulta =  _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente").ToString();
            var usuario = await _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Estudiante").ToListAsync();

            return usuario.Select(u => new UsuarioVewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                primer_nombre = u.primer_nombre,
                segundo_nombre = u.segundo_nombre,
                primer_apellido = u.primer_apellido,
                segundo_apellido = u.segundo_apellido,
                correo_electronico = u.correo_electronico,
                celular = u.celular,
                direccion = u.direccion,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }     

        // GET: api/Usuarios/SelectEstudiantes
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectEstudianteViewModel>> SelectEstudiantes()
        {
            //string consulta =  _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Docente").ToString();
            var estudiante = await _context.usuarios.Include(u => u.rol).Where(u => u.rol.nombre == "Estudiante" && u.condicion == true).ToListAsync();

            return estudiante.Select(u => new SelectEstudianteViewModel
            {
                idestudiante = u.idusuario,
                nombre = u.primer_nombre + " " + u.primer_apellido + " " + u.segundo_apellido
            });
        }

        // POST: api/Usuarios/Crear
        //[Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var num_documento = model.num_documento;
            if (await _context.usuarios.AnyAsync(u => u.num_documento == num_documento))
            {
                BadRequest("El Usuario ya se encuentra Registrado en el sistema");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario usuario = new Usuario
            {
                idrol = model.idrol,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                primer_nombre = model.primer_nombre,
                segundo_nombre = model.segundo_nombre,
                primer_apellido = model.primer_apellido,
                segundo_apellido = model.segundo_apellido,
                correo_electronico = model.correo_electronico,
                celular = model.celular,
                direccion = model.direccion,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                condicion = true
            };

            _context.usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        // PUT: api/Usuarios/Actualizar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.idusuario == model.idusuario);

            if (usuario == null)
            {
                return NotFound();
            }
            usuario.idrol = model.idrol;
            usuario.tipo_documento = model.tipo_documento;
            usuario.num_documento = model.num_documento;
            usuario.primer_nombre = model.primer_nombre;
            usuario.segundo_nombre = model.segundo_nombre;
            usuario.primer_apellido = model.primer_apellido;
            usuario.segundo_apellido = model.segundo_apellido;
            usuario.correo_electronico = model.correo_electronico;
            usuario.celular = model.celular;
            usuario.direccion = model.direccion;            

            if (model.act_password == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHas, out byte[] passwordSalt);
                usuario.password_hash = passwordHas;
                usuario.password_salt = passwordSalt;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //guardar Excepcion 
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Desactivar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //guardar Excepcion 
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Usuarios/Activar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //guardar Excepcion 
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var num_documento = model.num_documento;
            var usuario = await _context.usuarios.Where(u => u.condicion == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.num_documento == num_documento);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                new Claim(ClaimTypes.Name, num_documento),
                new Claim(ClaimTypes.Role, usuario.rol.nombre),
                new Claim("idusuario", usuario.idusuario.ToString()),
                new Claim("rol", usuario.rol.nombre),
                new Claim("nombre",usuario.primer_nombre+" "+usuario.segundo_nombre+" "+usuario.primer_apellido+" "+usuario.segundo_apellido)
            };

            return Ok(
                 new { token = GenerarToken(claims) }
             );

        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.idusuario == id);
        }
    }
}