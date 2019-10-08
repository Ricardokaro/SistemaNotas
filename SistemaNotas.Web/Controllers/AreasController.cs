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
using SistemaNotas.Web.Models.Administrar.Area;

namespace SistemaNotas.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public AreasController(DbContextSistemaNotas context)
        {
            _context = context;
        }

        // GET: api/Areas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AreaViewModel>> Listar()
        {
            var area = await _context.areas.ToListAsync();

            return area.Select(a => new AreaViewModel
            {
                idarea = a.idarea,
                nombre = a.nombre,
                estado = a.estado
            });
        }

        // POST: api/Areas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Area area = new Area
            {
                nombre = model.nombre,
                estado = true
            };

            _context.areas.Add(area);
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

        // PUT: api/Areas/Actualizar        
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idarea <= 0)
            {
                return BadRequest();
            }

            var area = await _context.areas.FirstOrDefaultAsync(a => a.idarea == model.idarea);

            if (area == null)
            {
                return NotFound();
            }

            area.nombre = model.nombre;

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

        // PUT: api/Areas/Desactivar/1        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await _context.areas.FirstOrDefaultAsync(a => a.idarea == id);

            if (area == null)
            {
                return NotFound();
            }

            area.estado = false;

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

        // PUT: api/Areas/Activar/1        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var area = await _context.areas.FirstOrDefaultAsync(a => a.idarea == id);

            if (area == null)
            {
                return NotFound();
            }

            area.estado = true;

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

        // GET: api/Areas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var area = await _context.areas.Where(a => a.estado == true).ToListAsync();

            return area.Select(c => new SelectViewModel
            {
                idarea = c.idarea,
                nombre = c.nombre
            });
        }

        private bool AreaExists(int id)
        {
            return _context.areas.Any(e => e.idarea == id);
        }
    }
}