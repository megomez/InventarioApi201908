using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Contexts;
using InventarioApi.Entities;
using InventarioApi.Models;
using AutoMapper;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ElementosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// GET: api/personas
        /// Trae el listado de elementos que hay en el sistema
        /// </summary>
        /// <returns>Listado total de elementos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementoVM>>> GetElementos()
        {
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

            var elementosVM = await query.ToListAsync();

            return elementosVM;
        }


        /// <summary>
        /// GET: api/elementos/{id}
        /// Obtiene los datos de un elemento específico
        /// </summary>
        /// <param name="id">Id del elemento buscado</param>
        /// <returns>Los datos del elemento buscado</returns>
        [HttpGet("{id}", Name = "ObtenerElemento")]
        public async Task<ActionResult<ElementoVM>> GetElemento(long id)
        {
            if (this.ElementoExists(id))
            {
                return NotFound();
            }

            var query = from pe in context.PersonaElementos
                        join e in context.Elementos
                          on pe.ElementoId equals e.Id
                        join p in context.Personas
                          on pe.ElementoId equals p.Id
                        where e.Id == id
                        select new PersonaElementoVM
                        {
                            Id = pe.Id,
                            ObservacionAsignacion = pe.ObservacionAsignacion,
                            ObservacionRetiro = pe.ObservacionRetiro,
                            FechaAsignacion = pe.FechaAsignacion,
                            FechaRetiro = pe.FechaRetiro,
                            PersonaId = p.Id,
                            Persona = p.PrimerNombre + " " + p.PrimerApellido,
                            ElementoId = e.Id,
                            Elemento = e.Descripcion
                        };


            var elementos = from e in context.Elementos
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

            var elementoVM = await elementos.FirstOrDefaultAsync(x => x.Id == id);
            elementoVM.PersonaElementos = await query.ToListAsync();

            return elementoVM;
        }

        /// <summary>
        /// PUT: api/elementos/{id}
        /// Ejecuta la actualización de los datos de un elemento
        /// </summary>
        /// <param name="id">Id del elemento que se va a actualizar</param>
        /// <param name="elementoVM">Datos del elemento que se va a actualizar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElemento(long id, [FromBody] ElementoVM elementoVM)
        {
            var elemento = mapper.Map<Elemento>(elementoVM);
            elemento.Id = id;
            context.Entry(elemento).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// POST: api/elementos
        /// Inserta elemento en la base de datos
        /// </summary>
        /// <param name="elementoVM">Datos del elemento que se ingresa</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<ElementoVM>> PostElemento([FromBody] ElementoVM elementoVM)
        {
            var elemento = mapper.Map<Elemento>(elementoVM);
            context.Elementos.Add(elemento);
            await context.SaveChangesAsync();

            elementoVM = mapper.Map<ElementoVM>(elemento);

            return new CreatedAtRouteResult("ObtenerElemento", new { id = elementoVM.Id }, elementoVM);
        }

        /// <summary>
        /// DELETE: api/elementos/{id}
        /// Elimina un elemento de la base de datos
        /// </summary>
        /// <param name="id">Id del elemento que se elimina</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Elemento>> DeleteElemento(long id)
        {
            if (!this.ElementoExists(id))
            {
                return NotFound();
            }

            var elemento = await context.Elementos.FirstOrDefaultAsync(x => x.Id == id);
            context.Elementos.Remove(elemento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si el elemento existe
        /// </summary>
        /// <param name="id">Id del elemento buscado</param>
        /// <returns>Respuesta si existe o no el elemento</returns>
        private bool ElementoExists(long id)
        {
            return context.Elementos.Any(e => e.Id == id);
        }
    }
}
