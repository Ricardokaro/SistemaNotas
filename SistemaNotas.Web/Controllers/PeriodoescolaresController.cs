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
using SistemaNotas.Web.Models.Administrar.Periodoescolar;

namespace SistemaNotas.Web.Controllers
{
    [Authorize(Roles = "Administrador, Docente")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoescolaresController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public PeriodoescolaresController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // GET: api/Periodos/Listar        ]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PeriodoescolarViewModel>> Listar()
        {
            var peridoescolar = await _context.periodoescolares.Include(p => p.anioescolar).ToListAsync();

            return peridoescolar.Select(p => new PeriodoescolarViewModel
            {
                idperiodo = p.idperiodo,
                idanio_escolar = p.idanio_escolar,
                anio_escolar = p.anioescolar.anio,
                fecha_inicio = formatoEnviarFecha(p.fecha_inicio),
                fecha_final = formatoEnviarFecha(p.fecha_final),
                nombre = p.nombre,
                porcentaje = p.porcentaje
            });
        }

        // POST: api/Periodos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Periodoescolar periodoescolar = new Periodoescolar
            {
                idanio_escolar = model.idanio_escolar,
                fecha_inicio = formatoGuardarFecha(model.fecha_inicio),
                fecha_final = formatoGuardarFecha(model.fecha_final),
                nombre = model.nombre,
                porcentaje = model.porcentaje
            };

            _context.periodoescolares.Add(periodoescolar);
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

        // Post: api/Periodos/ListarPeriodoxAnio        ]
        [HttpPost("[action]")]
        public async Task<IEnumerable<SelectPeriodoEscolarViewModel>> ListarPeriodoxAnio([FromBody] ConsultaPorAnioViewModel model)
        {          

            var aniocursado = await _context.aniocursados
                .Include(a => a.anioescolar)
                .Include(c => c.curso)
                .Where(ac => ac.anioescolar.idanio_escolar == model.idanio_escolar && ac.curso.idcurso == model.idcurso)
                .ToListAsync();

            var peridoescolar = await _context.periodoescolares
                .Include(p => p.anioescolar)
                .Where(p => p.idanio_escolar == model.idanio_escolar)
                .ToListAsync();

            List<Periodoescolar> periodoSeleccionado = new List<Periodoescolar>();

            foreach (var item in peridoescolar)
            {
                var calificacion = await _context.calificaciones
                    .Include(e => e.estudiante)
                    .Include(p => p.periodoescolar)
                         .ThenInclude(a => a.anioescolar)
                    .Include(cmd => cmd.cursoxmateriaxdocente)
                         .ThenInclude(c => c.curso)
                    .Include(cmd => cmd.cursoxmateriaxdocente)
                         .ThenInclude(m => m.materia)
                    .Where(c => c.periodoescolar.idanio_escolar == model.idanio_escolar
                     && c.cursoxmateriaxdocente.idcurso == model.idcurso
                     && c.cursoxmateriaxdocente.idmateria == model.idmateria
                     && c.periodoescolar.idperiodo == item.idperiodo)
                     .OrderBy(e => e.estudiante)
                     .OrderBy(p => p.idperiodo)
                     .GroupBy(e => e.estudiante)
                    .ToListAsync();

                if (calificacion.Count() < aniocursado.Count()) {
                    periodoSeleccionado.Add(item);
                    break;
                }
            }
            


            return periodoSeleccionado.Select(p => new SelectPeriodoEscolarViewModel
            {
                idperiodo = p.idperiodo,
                nombre = p.nombre
            });
        }

        private DateTime formatoGuardarFecha(string date)
        {

            string[] fecha = date.Split("-");
            string anio = fecha[0];
            string mes = fecha[1];
            string dia = fecha[2];

            //return DateTime.Parse(dia + "/" + mes + "/" + anio);
            return DateTime.Parse(anio + "/" + mes + "/" + dia);
        }

        private string formatoEnviarFecha(DateTime date)
        {

            /*string[] fecha = date.ToShortDateString().Split("/");
            string anio = fecha[2];
            string mes = fecha[1];
            string dia = fecha[0];

            return anio + "-" + mes + "-" + dia;*/
            string[] fecha = date.ToShortDateString().Split("/");
            string anio = fecha[2];
            string dia = fecha[1];
            string mes = fecha[0];

            return anio + "-" + mes + "-" + dia;
        }

        private bool PeriodoescolarExists(int id)
        {
            return _context.periodoescolares.Any(e => e.idperiodo == id);
        }
    }
}