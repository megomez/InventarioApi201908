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
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PersonasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// GET: api/personas
        /// Trae el listado de personas que hay en el sistema
        /// </summary>
        /// <returns>Listado total de personas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaVM>>> GetPersonas()
        {
            var query = from p in context.Personas
                        join td in context.Tdocumentos
                          on p.TdocumentoId equals td.Id
                        join a in context.Areas
                          on p.AreaId equals a.Id
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
                
            var personasVM = await query.ToListAsync();

            return personasVM;
        }

        /// <summary>
        /// GET: api/Personas/{id}
        /// Obtiene los datos de una persona específica
        /// </summary>
        /// <param name="id">Id de la persona buscada</param>
        /// <returns>Los datos de la persona buscada</returns>
        [HttpGet("{id}", Name = "ObtenerPersona")]
        public async Task<ActionResult<PersonaVM>> GetPersona(long id)
        {
            if (!this.PersonaExists(id))
            {
                return NotFound();
            }

            var query = from pe in context.PersonaElementos
                        join e in context.Elementos
                          on pe.ElementoId equals e.Id
                        join p in context.Personas
                          on pe.ElementoId equals p.Id
                        where p.Id == id
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

            var personas = from p in context.Personas
                        join td in context.Tdocumentos
                          on p.TdocumentoId equals td.Id
                        join a in context.Areas
                          on p.AreaId equals a.Id
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

            var personaVM = await personas.FirstOrDefaultAsync(x => x.Id == id);
            personaVM.PersonaElementos = await query.ToListAsync();

            return personaVM;
        }


        /// <summary>
        /// PUT: api/personas/{id}
        /// Ejecuta la actualización de los datos de una persona
        /// </summary>
        /// <param name="id">Id de la persona que se va a actualizar</param>
        /// <param name="personaVM">Datos de la persona que se va a actualizar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(long id, [FromBody] PersonaVM personaVM)
        {
            var persona = mapper.Map<Persona>(personaVM);
            persona.Id = id;
            context.Entry(persona).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// POST: api/Personas
        /// Inserta persona en la base de datos
        /// </summary>
        /// <param name="personaIngreso">Datos de la persona que se ingresa</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<PersonaVM>> PostPersona([FromBody] PersonaVM personaIngreso)
        {
            var persona = mapper.Map<Persona>(personaIngreso);
            context.Personas.Add(persona);
            await context.SaveChangesAsync();

            var personaVM = mapper.Map<PersonaVM>(persona);
            personaVM.NombreCompleto = persona.PrimerNombre + " " +
                                (persona.SegundoNombre.Length == 0 ? "" : persona.SegundoNombre + " ") +
                                persona.PrimerApellido + " " +
                                (persona.SegundoApellido.Length == 0 ? "" : persona.SegundoApellido);

            return new CreatedAtRouteResult("ObtenerPersona", new { id = personaVM.Id }, personaVM);
        }


        /// <summary>
        /// DELETE: api/personas/{id}
        /// Elimina una persona de la base de datos
        /// </summary>
        /// <param name="id">Id de la persona que se elimina</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Persona>> DeletePersona(long id)
        {
            if (!this.PersonaExists(id))
            {
                return NotFound();
            }

            var persona = await context.Personas.FirstOrDefaultAsync(x => x.Id == id);
            context.Personas.Remove(persona);
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si la persona existe
        /// </summary>
        /// <param name="id">Id de la persona buscada</param>
        /// <returns>Respuesta si existe o no la persona</returns>
        private bool PersonaExists(long id)
        {
            return context.Personas.Any(e => e.Id == id);
        }
    }
}
