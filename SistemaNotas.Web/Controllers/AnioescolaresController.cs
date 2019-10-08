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
using SistemaNotas.Web.Models.Administrar.Anioescolar;

namespace SistemaNotas.Web.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnioescolaresController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public AnioescolaresController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // GET: api/Aniosescolares/Listar
        [Authorize(Roles = "Administrador, Docente")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AnioescolarViewModel>> Listar()
        {
            var anioescolar = await _context.aniosescolares.ToListAsync();

            return anioescolar.Select(a => new AnioescolarViewModel
            {
                idanio_escolar = a.idanio_escolar,
                nombre = a.nombre,
                fecha_inicio = formatoEnviarFecha(a.fecha_inicio),
                fecha_final =  formatoEnviarFecha(a.fecha_final),
                anio = a.anio,
                estado = a.estado
            });
        }

        // POST: api/Anioescolares/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Anioescolar anioescolar = new Anioescolar
            {
                nombre = model.nombre,
                fecha_inicio =  formatoGuardarFecha(model.fecha_inicio),
                fecha_final = formatoGuardarFecha(model.fecha_final),
                anio = model.anio,
                estado = true
            };

            _context.aniosescolares.Add(anioescolar);
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

        // PUT: api/Anioescolares/Actualizar 
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idanio_escolar <= 0)
            {
                return BadRequest();
            }

            var anioescolar = await _context.aniosescolares.FirstOrDefaultAsync(a => a.idanio_escolar == model.idanio_escolar);

            if (anioescolar == null)
            {
                return NotFound();
            }

            anioescolar.nombre = model.nombre;
            anioescolar.fecha_inicio = formatoGuardarFecha(model.fecha_inicio);
            anioescolar.fecha_final = formatoGuardarFecha(model.fecha_final);
            anioescolar.anio = model.anio;           

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

        // PUT: api/Anioescolares/Desactivar/1        
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var anioescolar = await _context.aniosescolares.FirstOrDefaultAsync(a => a.idanio_escolar == id);

            if (anioescolar == null)
            {
                return NotFound();
            }

            anioescolar.estado = false;

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

        // PUT: api/Anioescolares/Activar/1        
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var anioescolar = await _context.aniosescolares.FirstOrDefaultAsync(a => a.idanio_escolar == id);

            if (anioescolar == null)
            {
                return NotFound();
            }

            anioescolar.estado = true;

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
            var anioescolar = await _context.aniosescolares.Where(a => a.estado == true).ToListAsync();

            return anioescolar.Select(a => new SelectViewModel
            {
                idanio_escolar = a.idanio_escolar,
                nombre = a.nombre
            });
        }

        private DateTime formatoGuardarFecha(string date) {

            string [] fecha = date.Split("-");
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

        private bool AnioescolarExists(int id)
        {
            return _context.aniosescolares.Any(e => e.idanio_escolar == id);
        }
    }
}