using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Contexts;
using InventarioApi.Entities;
using AutoMapper;
using InventarioApi.Models;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AreasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// GET: api/areas
        /// Obtiene las áreas registradas
        /// </summary>
        /// <returns>Listado de áreas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaVM>>> GetAreas()
        {
            var areas = await context.Areas.ToListAsync();
            var areasVM = mapper.Map<List<AreaVM>>(areas);

            return areasVM;
        }

        /// <summary>
        /// GET: api/areas/{id}
        /// Obtiene una área específica por id
        /// </summary>
        /// <param name="id">Nombre del área buscada</param>
        /// <returns>Área correspondiente al Id</returns>
        [HttpGet("{id}", Name = "ObtenerArea")]
        public async Task<ActionResult<AreaVM>> GetArea(long id)
        {
            if (!this.AreaExists(id))
            {
                return NotFound();
            }

            var areas = await context.Areas.FirstOrDefaultAsync(x => x.Id == id);
            var query = from p in context.Personas
                        join td in context.Tdocumentos
                          on p.TdocumentoId equals td.Id
                        join a in context.Areas
                          on p.AreaId equals a.Id
                        where a.Id == id
                        select new PersonaVM
                        {
                            Id = p.Id,
                            Documento = p.Documento,
                            NombreCompleto = p.PrimerNombre + " " +
                                     (p.SegundoNombre.Length == 0 ? "" : p.SegundoNombre + " ") +
                                     p.PrimerApellido + " " +
                                     (p.SegundoApellido.Length == 0 ? "" : p.SegundoApellido),
                            PrimerNombre = p.PrimerNombre,
                            SegundoNombre = p.SegundoNombre,
                            PrimerApellido = p.PrimerApellido,
                            SegundoApellido = p.SegundoApellido,
                            FechaNacimiento = p.FechaNacimiento,
                            Direccion = p.Direccion,
                            Telefono = p.Telefono,
                            Email = p.Email,
                            TipoDocumento = td.Sigla,
                            TdocumentoId = td.Id,
                            Area = a.Nombre,
                            AreaId = a.Id
                        };

            var areasVM = mapper.Map<AreaVM>(areas);
            areasVM.Personas = await query.ToListAsync();

            return areasVM;
        }


        /// <summary>
        /// PUT: api/areas/{id}
        /// Actualiza los datos del área
        /// </summary>
        /// <param name="id">Id del área que se va a actualizar</param>
        /// <param name="areaVM">Datos de la actualización</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(long id, [FromBody] AreaVM areaVM)
        {
            var area = mapper.Map<Area>(areaVM);
            area.Id = id;
            context.Entry(area).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        // 
        /// <summary>
        /// POST: api/areas
        /// Ingresa una nueva área al sistema
        /// </summary>
        /// <param name="areaVM">Datos del área que se ingresa</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<AreaVM>> PostArea([FromBody] AreaVM areaVM)
        {
            var area = mapper.Map<Area>(areaVM);
            context.Areas.Add(area);
            await context.SaveChangesAsync();

            areaVM = mapper.Map<AreaVM>(area);

            return new CreatedAtRouteResult("ObtenerArea", new { id = areaVM.Id }, areaVM);
        }

        /// <summary>
        /// DELETE: api/Areas/{id}
        /// Elimina un área del sistema
        /// </summary>
        /// <param name="id">Id del área que se va a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> DeleteArea(long id)
        {
            if (!this.AreaExists(id))
            {
                return NotFound();
            }

            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id);
            context.Areas.Remove(area);
            await context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si el área consultada existe
        /// </summary>
        /// <param name="id">Id del área consultada</param>
        /// <returns>Existe o no existe</returns>
        private bool AreaExists(long id)
        {
            return context.Areas.Any(e => e.Id == id);
        }
    }
}
