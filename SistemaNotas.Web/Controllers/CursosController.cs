using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Datos;
using SistemaNotas.Entidades.Administrar;
using SistemaNotas.Web.Models.Administrar.Curso;

namespace SistemaNotas.Web.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public CursosController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // GET: api/Cursos/Listar
        [Authorize(Roles = "Administrador, Docente")]        
        [HttpGet("[action]")]
        public async Task<IEnumerable<CursoViewModel>> Listar()
        {
            var curso = await _context.cursos.ToListAsync();

            return curso.Select(c => new CursoViewModel
            {
                idcurso = c.idcurso,
                nombre = c.nombre,
                nivel = c.nivel,
                estado = c.estado
            });
        }

        // POST: api/Cursos/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Curso curso = new Curso
            {
                nombre = model.nombre,  
                nivel = model.nivel,
                estado = true
            };

            _context.cursos.Add(curso);
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

        // PUT: api/Cursos/Actualizar  
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcurso <= 0)
            {
                return BadRequest();
            }

            var curso = await _context.cursos.FirstOrDefaultAsync(c => c.idcurso == model.idcurso);

            if (curso == null)
            {
                return NotFound();
            }

            curso.nombre = model.nombre;
            curso.nivel = model.nivel;

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

        // PUT: api/Cursos/Desactivar/1    
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var curso = await _context.cursos.FirstOrDefaultAsync(c => c.idcurso == id);

            if (curso == null)
            {
                return NotFound();
            }

            curso.estado = false;

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

        // PUT: api/Cursos/Activar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var curso = await _context.cursos.FirstOrDefaultAsync(c => c.idcurso == id);

            if (curso == null)
            {
                return NotFound();
            }

            curso.estado = true;

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

        // GET: api/Aniosescolares/Select
        [Authorize(Roles = "Administrador, Docente")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var curso = await _context.cursos.Where(a => a.estado == true).ToListAsync();

            return curso.Select(c => new SelectViewModel
            {
                idcurso = c.idcurso,
                nombre = c.nombre
            });
        }

        private bool CursoExists(int id)
        {
            return _context.cursos.Any(e => e.idcurso == id);
        }
    }
}