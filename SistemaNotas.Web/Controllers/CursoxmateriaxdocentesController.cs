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

namespace SistemaNotas.Web.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CursoxmateriaxdocentesController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public CursoxmateriaxdocentesController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // POST: api/Cursoxmateriaxdocentes/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }                   

            Cursoxmateriaxdocente cursoxmateriaxdocente = new Cursoxmateriaxdocente
            {
                idcurso = model.idcurso,
                idmateria = model.idmateria,
                iddocente = model.iddocente,
                idanio_escolar = model.idanio_escolar,
                intensidad_horaria = model.intensidad_horaria,
                estado = true
            };

            _context.cursoxmateriaxdocentes.Add(cursoxmateriaxdocente);
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

        // GET: api/Cursoxmateriaxdocentes/Listar
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<CursoxmateriaxdocenteViewModel>> Listar([FromBody] ConsultarCursoxanioescolarViewModel model)
        {

            var cursoxanioxdocente = await _context.cursoxmateriaxdocentes
                .Include(cmd => cmd.curso)
                .Include(cmd => cmd.materia)
                .Include(cmd => cmd.docente)
                .Include(cmd => cmd.anioescolar)
                .Where(cmd => cmd.idanio_escolar == model.idanio_escolar && cmd.idcurso == model.idcurso)
                .ToListAsync();            

            return cursoxanioxdocente.Select(cmd => new CursoxmateriaxdocenteViewModel
            {
                id = cmd.id,
                idcurso = cmd.idcurso,
                curso = cmd.curso.nombre,
                idmateria = cmd.idmateria,
                materia = cmd.materia.nombre,
                iddocente = cmd.iddocente,
                docente = cmd.docente.primer_nombre+" "+ cmd.docente.segundo_nombre+" "+cmd.docente.primer_apellido+" "+cmd.docente.segundo_apellido,
                idanio_escolar = cmd.idanio_escolar,
                anioescolar = cmd.anioescolar.nombre,
                intensidad_horaria = cmd.intensidad_horaria,
                estado = cmd.estado
            });
        }

        // GET: api/Cursoxmateriaxdocentes/ListarCursosDocente
        [Authorize(Roles = "Administrador, Docente")]        
        [HttpPost("[action]")]
        public async Task<IEnumerable<CursoxDocenteViewModel>> ListarCursosDocente([FromBody] ConsultaCursoxDocenteViewModel model)
        {

            var cursoxanioxdocente = await _context.cursoxmateriaxdocentes
                .Include(cmd => cmd.curso)                
                .Include(cmd => cmd.docente)
                .Include(cmd => cmd.anioescolar)
                .Where(cmd => cmd.idanio_escolar == model.idanio_escolar && cmd.iddocente == model.iddocente)
                .ToListAsync();

            return cursoxanioxdocente.Select(cmd => new CursoxDocenteViewModel
            {               
                idcurso = cmd.idcurso,
                curso = cmd.curso.nombre
            });
        }

        // GET: api/Cursoxmateriaxdocentes/ListarCursosDocenteDirector
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<CursoxDocenteViewModel>> ListarCursosDocenteDirector([FromBody] ConsultaCursoxDocenteViewModel model)
        {

            var cursoxanioxdocente = await _context.cursoxmateriaxdocentes
                .Include(cmd => cmd.curso)
                    .ThenInclude(d => d.directores)
                .Include(cmd => cmd.docente)
                .Include(cmd => cmd.anioescolar)
                .Where(cmd => cmd.curso.directores.Count(d => d.iddocente == model.iddocente && d.idanio_escolar==model.idanio_escolar) > 0)
                .ToListAsync();

            return cursoxanioxdocente.Select(cmd => new CursoxDocenteViewModel
            {
                idcurso = cmd.idcurso,
                curso = cmd.curso.nombre
            });
        }

        // GET: api/Cursoxmateriaxdocentes/ListarMateriaDocente
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<MateriaDocenteViewModel>> ListarMateriaDocente([FromBody] ConsultaMateriaDocenteViewModel model)
        {

            var cursoxanioxdocente = await _context.cursoxmateriaxdocentes
                .Include(cmd => cmd.curso)
                .Include(cmd => cmd.materia)
                .Include(cmd => cmd.docente)
                .Include(cmd => cmd.anioescolar)
                .Where(cmd => cmd.idanio_escolar == model.idanio_escolar && cmd.idcurso == model.idcurso && cmd.iddocente == model.iddocente)
                .ToListAsync();

            return cursoxanioxdocente.Select(cmd => new MateriaDocenteViewModel
            {
                idmateria = cmd.idmateria,
                nombre = cmd.materia.nombre
            });
        }

        // PUT: api/Cursoxmateriaxdocentes/Actualizar   
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarCursoxanioescolarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <= 0)
            {
                return BadRequest();
            }

            var cursoxmateriaxdocente = await _context.cursoxmateriaxdocentes.FirstOrDefaultAsync(c => c.id == model.id);

            if (cursoxmateriaxdocente == null)
            {
                return NotFound();
            }

            cursoxmateriaxdocente.iddocente = model.iddocente;
            cursoxmateriaxdocente.intensidad_horaria = model.intensidad_horaria;

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


        private bool CursoxmateriaxdocenteExists(int id)
        {
            return _context.cursoxmateriaxdocentes.Any(e => e.id == id);
        }
    }
}