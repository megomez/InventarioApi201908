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
    public class TelementosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TelementosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// GET: api/telementos
        /// Trae el listado de tipos de elementos
        /// </summary>
        /// <returns>Listado de tipos de elementos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelementoVM>>> GetTelementos()
        {
            var telemento = await context.Telementos.ToListAsync();
            var telementoVM = mapper.Map<List<TelementoVM>>(telemento);

            return telementoVM;
        }

        /// <summary>
        /// GET: api/telementos/{id}
        /// Trae los datos de un tipo de elemento específico
        /// </summary>
        /// <param name="id">Id del tipo de elemento buscado</param>
        /// <returns>Datos del elemento buscado</returns>
        [HttpGet("{id}", Name = "ObtenerTelemento")]
        public async Task<ActionResult<TelementoVM>> GetTelemento(long id)
        {
            if (!this.TelementoExists(id))
            {
                return NotFound();
            }

            var telementos = await context.Telementos.FirstOrDefaultAsync(x => x.Id == id);
            var query = from e in context.Elementos
                        join te in context.Telementos
                          on e.TelementoId equals te.Id
                        select new ElementoVM
                        {
                            Id = e.Id,
                            Descripcion = e.Descripcion,
                            Serial = e.Serial,
                            ValorCompra = e.ValorCompra,
                            FechaCompra = e.FechaCompra,
                            Estado = e.Estado,
                            TelementoId = te.Id,
                            Telemento = te.Nombre
                        };

            var telementosVM = mapper.Map<TelementoVM>(telementos);
            telementosVM.Elementos = await query.ToListAsync();

            return telementosVM;
        }

        /// <summary>
        /// PUT: api/telementos/{id}
        /// Actualiza los datos de un tipo de elemento
        /// </summary>
        /// <param name="id">Id del tipo de elemento</param>
        /// <param name="telementoVM">Datos que se van a actualizar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelemento(long id, [FromBody] TelementoVM telementoVM)
        {
            var telemento = mapper.Map<Telemento>(telementoVM);
            telemento.Id = id;
            context.Entry(telemento).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// POST: api/telementos
        /// Ingresa un nuevo tipo de elemento al sistema
        /// </summary>
        /// <param name="telementoVM">Datos del nuevo tipo de elemento</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<TelementoVM>> PostTelemento([FromBody] TelementoVM telementoVM)
        {
            var telemento = mapper.Map<Tdocumento>(telementoVM);
            context.Tdocumentos.Add(telemento);
            await context.SaveChangesAsync();

            telementoVM = mapper.Map<TelementoVM>(telemento);

            return new CreatedAtRouteResult("ObtenerTelemento", new { id = telementoVM.Id }, telementoVM);
        }


        /// <summary>
        /// DELETE: api/telementos/{id}
        /// Elimina un tipo de elemento
        /// </summary>
        /// <param name="id">Id del tipo de elemento que se va a eliminar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Telemento>> DeleteTelemento(long id)
        {
            if(!this.TelementoExists(id))
            {
                return NotFound();
            }

            var telemento = await context.Telementos.FirstOrDefaultAsync(x => x.Id == id);
            context.Telementos.Remove(telemento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si el tipo de elemento consultado existe
        /// </summary>
        /// <param name="id">Id del tipo de elemento consultado</param>
        /// <returns>Existe o no existe</returns>
        private bool TelementoExists(long id)
        {
            return context.Telementos.Any(e => e.Id == id);
        }
    }
}
