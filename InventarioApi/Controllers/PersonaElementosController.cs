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
    public class PersonaElementosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PersonaElementosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// GET: api/personaelementos
        /// Trae el listado de elementos que estan asignados a las personas
        /// </summary>
        /// <returns>Listado total de elementos asignados a personas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaElementoVM>>> GetPersonaElementos()
        {
            var query = from pe in context.PersonaElementos
                        join e in context.Elementos
                          on pe.ElementoId equals e.Id
                        join p in context.Personas
                          on pe.ElementoId equals p.Id
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

            var personaElementosVM = await query.ToListAsync();

            return personaElementosVM;
        }

        /// <summary>
        /// GET: api/personaelementos/{id}
        /// Obtiene los datos de un elemento específico
        /// </summary>
        /// <param name="id">Id del elemento buscado</param>
        /// <returns>Los datos del elemento buscado</returns>
        [HttpGet("{id}", Name = "ObtenerPersonaElemento")]
        public async Task<ActionResult<PersonaElementoVM>> GetPersonaElemento(long id)
        {
            if (this.PersonaElementoExists(id))
            {
                return NotFound();
            }

            var query = from pe in context.PersonaElementos
                        join e in context.Elementos
                          on pe.ElementoId equals e.Id
                        join p in context.Personas
                          on pe.ElementoId equals p.Id
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

            var personaElementoVM = await query.FirstOrDefaultAsync(x => x.Id == id);

            return personaElementoVM;
        }

        /// <summary>
        /// PUT: api/personaelementos/{id}
        /// Ejecuta la actualización de los datos de un elemento asignado a una persona
        /// </summary>
        /// <param name="id">Id del elemento que se va a actualizar</param>
        /// <param name="personaElementoVM">Datos del elemento que se va a actualizar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaElemento(long id, [FromBody] PersonaElementoVM personaElementoVM)
        {
            var personaElemento = mapper.Map<PersonaElemento>(personaElementoVM);
            personaElemento.Id = id;
            context.Entry(personaElemento).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// POST: api/personaelementos
        /// Inserta un elemento asignado a una persona en la base de datos
        /// </summary>
        /// <param name="personaElementoVM">Datos del elemento que se ingresa</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<PersonaElementoVM>> PostPersonaElemento([FromBody] PersonaElementoVM personaElementoVM)
        {
            var personaElemento = mapper.Map<PersonaElemento>(personaElementoVM);
            context.PersonaElementos.Add(personaElemento);
            await context.SaveChangesAsync();

            personaElementoVM = mapper.Map<PersonaElementoVM>(personaElemento);

            return new CreatedAtRouteResult("ObtenerPersonaElemento", new { id = personaElementoVM.Id }, personaElementoVM);
        }

        /// <summary>
        /// DELETE: api/personaelementos/{id}
        /// Elimina un elemento asignado a una persona de la base de datos
        /// </summary>
        /// <param name="id">Id del elemento asignado a una persona que se elimina</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonaElemento>> DeletePersonaElemento(long id)
        {
            if (!this.PersonaElementoExists(id))
            {
                return NotFound();
            }

            var personaElemento = await context.PersonaElementos.FirstOrDefaultAsync(x => x.Id == id);
            context.PersonaElementos.Remove(personaElemento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si el registro existe
        /// </summary>
        /// <param name="id">Id del registro buscado</param>
        /// <returns>Respuesta si existe o no el registro</returns>
        private bool PersonaElementoExists(long id)
        {
            return context.PersonaElementos.Any(e => e.Id == id);
        }
    }
}
