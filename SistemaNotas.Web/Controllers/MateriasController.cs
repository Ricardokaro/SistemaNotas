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
using SistemaNotas.Web.Models.Administrar.Materia;

namespace SistemaNotas.Web.Controllers
{

    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly DbContextSistemaNotas _context;

        public MateriasController(DbContextSistemaNotas context)
        {
            _context = context;
        }


        // GET: api/Materias/Listar        
        [HttpGet("[action]")]
        public async Task<IEnumerable<MateriaViewModels>> Listar()
        {
            var materia = await _context.materias.Include(m => m.area).ToListAsync();

            return materia.Select(m => new MateriaViewModels
            {
                idmateria = m.idmateria,
                idarea = m.idarea,
                area = m.area.nombre,
                nombre = m.nombre,
                estado = m.estado                
            });
        }

        // GET: api/Materias/SelectMateriasViewModel        
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectMateriasViewModel>> SelectMaterias()
        {
            var materia = await _context.materias
                .Include(m => m.area)
                .Where(m => m.estado == true )
                .ToListAsync();

            return materia.Select(m => new SelectMateriasViewModel
            {
                idmateria = m.idmateria,              
                nombre = m.nombre                
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

            Materia materia = new Materia
            {
                idarea = model.idarea,
                nombre = model.nombre,
                estado = true
            };

            _context.materias.Add(materia);
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

        private bool MateriaExists(int id)
        {
            return _context.materias.Any(e => e.idmateria == id);
        }
    }
}