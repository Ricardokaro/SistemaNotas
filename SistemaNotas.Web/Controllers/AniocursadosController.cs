using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Datos;
using SistemaNotas.Entidades.Estudiante;
using SistemaNotas.Web.Models.Estudiante;
using SistemaNotas.Web.Models.Administrar.Aniocursado;

namespace SistemaNotas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AniocursadosController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public AniocursadosController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // POST: api/Aniocursados/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Aniocursado aniocursado = new Aniocursado
            {
                idcurso = model.idcurso,
                idestudiante = model.idestudiante,                
                idanio_escolar = model.idanio_escolar,                
                estado = "Matriculado"
            };

            _context.aniocursados.Add(aniocursado);
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

        // GET: api/Aniocursados/Listar
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<AniocursadoViewModel>> Listar([FromBody] AniocursadoViewModel model)
        {
            var aniocursados = await _context.aniocursados
               .Include(ac => ac.curso)
               .Include(ac => ac.estudiante)
               .Include(ac => ac.anioescolar)
               .Where(ac => ac.idanio_escolar == model.idanio_escolar && ac.idcurso == model.idcurso)
               .ToListAsync();

            return aniocursados.Select(a => new AniocursadoViewModel {
                idanio_cursado = a.idanio_cursado,
                idcurso = a.idcurso,
                curso = a.curso.nombre,
                idestudiante = a.idestudiante,
                estudiante = a.estudiante.primer_nombre + " " + a.estudiante.primer_apellido + " " + a.estudiante.segundo_apellido,
                idanio_escolar = a.idanio_escolar,
                anio_escolar = a.anioescolar.nombre,
                estado = a.estado
            });
        }

        // GET: api/Aniocursados/Listar
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<AniocursadoViewModel>> ListarEstudiantesxDirector([FromBody] EstudiantexDirectorViewModel model)
        {
            var aniocursados = await _context.aniocursados
               .Include(ac => ac.curso)
                    .ThenInclude(d => d.directores)
               .Include(ac => ac.estudiante)
               .Include(ac => ac.anioescolar)
               .Where(ac => ac.idanio_escolar == model.idanio_escolar && ac.idcurso == model.idcurso && ac.curso.directores.Count(d => d.iddocente  == model.iddirector) > 0)
               .ToListAsync();

            return aniocursados.Select(a => new AniocursadoViewModel
            {
                idanio_cursado = a.idanio_cursado,
                idcurso = a.idcurso,
                curso = a.curso.nombre,
                idestudiante = a.idestudiante,
                estudiante = a.estudiante.primer_nombre + " " + a.estudiante.primer_apellido + " " + a.estudiante.segundo_apellido,
                idanio_escolar = a.idanio_escolar,
                anio_escolar = a.anioescolar.nombre,
                estado = a.estado
            });
        }

        // GET: api/Aniocursados/ListarEstudiantesSinCalificacion
        [Authorize(Roles = "Administrador, Docente")]
        [HttpPost("[action]")]
        public async Task<IEnumerable<EstudiantesSinCalificacionViewModel>> ListarEstudiantesSinCalificacion([FromBody] SelectEstudianteSincalificacionViewModel model)
        {

            var aniocursados = await _context.aniocursados                
                .Include(ac => ac.curso)                
                .Include(ac => ac.estudiante).ThenInclude(c => c.calificaciones)
                .Include(ac => ac.anioescolar)
                .Where(ac => ac.idanio_escolar == model.idanio_escolar && ac.idcurso == model.idcurso)
                .ToListAsync();

            var anioxcursoxmateriaxdocente = await _context.cursoxmateriaxdocentes
                .Where(c => c.idanio_escolar == model.idanio_escolar
               && c.idcurso == model.idcurso
               && c.idmateria == model.idmateria
                ).ToListAsync();


            List<Aniocursado> arrayAnioCursados = new List<Aniocursado>();
            foreach (var aniocursado in aniocursados)
            {
                foreach (var calificacion in aniocursado.estudiante.calificaciones)
                {
                    if (model.idperiodo == calificacion.idperiodo 
                        && calificacion.idcurso_materia_docente == anioxcursoxmateriaxdocente[0].id
                        && anioxcursoxmateriaxdocente[0].idmateria == model.idmateria) {
                        arrayAnioCursados.Add(aniocursado);
                    }
                }                    
            }

            foreach (var arrayAnioCursado in arrayAnioCursados)
            {
                aniocursados.Remove(arrayAnioCursado);
            }

            return aniocursados.Select(ac => new EstudiantesSinCalificacionViewModel
            {
                idanio_cursado = ac.idanio_cursado,
                idcurso = ac.idcurso,
                curso = ac.curso.nombre,
                idestudiante = ac.idestudiante,
                estudiante = ac.estudiante.primer_nombre + " " + ac.estudiante.primer_apellido + " " + ac.estudiante.segundo_apellido,
                idanio_escolar = ac.idanio_escolar,
                anio_escolar = ac.anioescolar.nombre,
                calificacion = 0,
                observacion = "",
                estado = ac.estado           
            });
        }

        // POST: api/Aniocursados/Estamatriculado/1
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Estamatriculado([FromBody] EstamatriculadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aniocursado = await _context.aniocursados
              .Include(ac => ac.curso)
              .Include(ac => ac.estudiante)
              .Include(ac => ac.anioescolar)
              .Where(ac => ac.idanio_escolar == model.idanio_escolar && ac.idestudiante == model.idestudiante && ac.estado=="Matriculado")
              .ToListAsync();



            if (aniocursado.Count > 0)
            {
                return Ok();
            }

            return NotFound();
        }

        private bool AniocursadoExists(int id)
        {
            return _context.aniocursados.Any(e => e.idanio_cursado == id);
        }
    }
}