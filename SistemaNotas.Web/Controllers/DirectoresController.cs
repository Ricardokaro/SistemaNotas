using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Datos;
using SistemaNotas.Entidades.Docente;
using SistemaNotas.Web.Models.Docente;

namespace SistemaNotas.Web.Controllers { 

    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoresController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public DirectoresController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // GET: api/Directores/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<DirectorViewModel>> Listar()
        {
            var director = await _context.directores
                .Include(d => d.docente)
                .Include(d => d.curso)
                .Include(d => d.anioescolar)
                .ToListAsync();

            return director.Select(d => new DirectorViewModel
            {
                iddirector = d.iddirector,
                iddocente = d.iddocente,
                docente = d.docente.primer_nombre + " " + d.docente.primer_apellido + " " + d.docente.segundo_apellido,
                idcurso = d.idcurso,
                curso = d.curso.nombre,
                idanio_escolar = d.idanio_escolar,
                anio_escolar = d.anioescolar.nombre
            });
        }

        // POST: api/Directores/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var buscarDirector = await _context.directores
                .Include(d => d.anioescolar)
                .Include(d => d.curso)
                .Include(d => d.docente)
                .Where(d => d.idanio_escolar == model.idanio_escolar && d.iddocente == model.iddocente
                || d.idanio_escolar == model.idanio_escolar && d.idcurso == model.idcurso)
                .ToListAsync();

            if (buscarDirector.Count > 0)
            {
                return Ok(new { mensaje = true });
            }
            else
            {

                Director director = new Director
                {
                    iddocente = model.iddocente,
                    idcurso = model.idcurso,
                    idanio_escolar = model.idanio_escolar
                };

                _context.directores.Add(director);
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
        }

        // PUT: api/Areas/Actualizar        
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarDirectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.iddirector <= 0)
            {
                return BadRequest();
            }

            var director = await _context.directores.FirstOrDefaultAsync(d => d.iddirector == model.iddirector);

            if (director == null)
            {
                return NotFound();
            }

            director.iddocente = model.iddocente;
            director.idcurso = model.idcurso;
            director.idanio_escolar = model.idanio_escolar;

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

        private bool DirectorExists(int id)
        {
            return _context.directores.Any(e => e.iddirector == id);
        }
    }
}