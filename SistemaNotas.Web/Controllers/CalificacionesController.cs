using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Datos;
using SistemaNotas.Entidades.Docente;
using SistemaNotas.Web.Models.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Controllers
{
    [Authorize(Roles = "Docente")]
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public CalificacionesController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // POST: api/Calificaciones/Crear        
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearCalificacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curso_materia_docente = await _context.cursoxmateriaxdocentes.Where(c => c.idanio_escolar == model.idanio_escolar && c.idcurso == model.idcurso && c.idmateria == model.idmateria).ToListAsync();
            

            Calificacion calificacion = new Calificacion
            {
                idestudiante = model.idestudiante,
                idcurso_materia_docente = curso_materia_docente[0].id,
                idperiodo = model.idperiodo,
                calificacion = model.calificacion,
                obervacion = model.observacion,
                estado = true
            };

            _context.calificaciones.Add(calificacion);
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

        // Post: api/Calificaciones/Listar
        [HttpPost("[action]")]
        public async Task<IEnumerable<CalificacionViewModel>> Listar([FromBody] EstudiantesCalificacionViewModel model)
        {
            var calificacion = await _context.calificaciones
            .Include(c => c.estudiante)               
            .Include(c => c.cursoxmateriaxdocente).ThenInclude(d=>d.docente)
            .Include(c => c.cursoxmateriaxdocente).ThenInclude(cc=>cc.curso)
            .Include(c => c.cursoxmateriaxdocente).ThenInclude(m => m.materia)
            .Include(c => c.periodoescolar).ThenInclude(a => a.anioescolar)
            .Where(c =>
            c.periodoescolar.anioescolar.idanio_escolar == model.idanio_escolar
            && c.cursoxmateriaxdocente.curso.idcurso == model.idcurso
            && c.cursoxmateriaxdocente.materia.idmateria == model.idmateria
            && c.cursoxmateriaxdocente.docente.idusuario == model.iddocente)
            .OrderBy(c => c.estudiante)
            .OrderBy(c => c.periodoescolar.idperiodo)
            .GroupBy(c => c.estudiante)            
            .ToListAsync();


             return calificacion.Select(c => new CalificacionViewModel
             {
                 idestudiante = c.Key.idusuario,
                 estudiante = c.Key.primer_nombre + " " + c.Key.primer_apellido + " " + c.Key.segundo_apellido,
                 listadoCalificaciones = c.Key.calificaciones.ToList()
              });

        }

        private bool CalificacionExists(int id)
        {
            return _context.calificaciones.Any(e => e.idcalificacion == id);
        }
    }
}