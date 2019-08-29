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
    public class TdocumentosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TdocumentosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// GET: api/tdocumentos
        /// Trae el listado de tipos de documentos
        /// </summary>
        /// <returns>Listado de tipos de documentos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TdocumentoVM>>> GetTdocuemntos()
        {
            var tdocumentos = await context.Tdocumentos.ToListAsync();
            var tdocumentoVM = mapper.Map<List<TdocumentoVM>>(tdocumentos);

            return tdocumentoVM;
        }


        /// <summary>
        /// GET: api/tdocumentos/{id}
        /// Trae los datos de un tipo de documento específico
        /// </summary>
        /// <param name="id">Id del tipo de documento buscado</param>
        /// <returns>Datos del documento buscado</returns>
        [HttpGet("{id}", Name = "ObtenerTdocumento")]
        public async Task<ActionResult<TdocumentoVM>> GetTdocumento(long id)
        {
            if (!this.TdocumentoExists(id))
            {
                return NotFound();
            }

            var tdocumento = await context.Tdocumentos.FirstOrDefaultAsync(x => x.Id == id);
            var tdocumentoVM = mapper.Map<TdocumentoVM>(tdocumento);

            return tdocumentoVM;
        }


        /// <summary>
        /// PUT: api/tdocumentos/{id}
        /// Actualiza los datos de un tipo de documento
        /// </summary>
        /// <param name="id">Id del tipo de documento</param>
        /// <param name="tdocumento">Datos que se van a actualizar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTdocumento(long id, [FromBody] TdocumentoVM tdocumentoVM)
        {
            var tdocumento = mapper.Map<Tdocumento>(tdocumentoVM);
            tdocumento.Id = id;
            context.Entry(tdocumento).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// POST: api/tdocumentos
        /// Ingresa un nuevo tipo de documento al sistema
        /// </summary>
        /// <param name="tdocumentoVM">Datos del nuevo tipo de documento</param>
        /// <returns>Resultado de la inserción</returns>
        [HttpPost]
        public async Task<ActionResult<TdocumentoVM>> PostTdocumento([FromBody] TdocumentoVM tdocumentoVM)
        {
            var tdocumento = mapper.Map<Tdocumento>(tdocumentoVM);
            context.Tdocumentos.Add(tdocumento);
            await context.SaveChangesAsync();

            tdocumentoVM = mapper.Map<TdocumentoVM>(tdocumento);

            return new CreatedAtRouteResult("ObtenerTdocumento", new { id = tdocumentoVM.Id }, tdocumentoVM);
        }


        /// <summary>
        /// DELETE: api/tdocumentos/{id}
        /// Elimina un tipo de documento
        /// </summary>
        /// <param name="id">Id del tipo de documento que se va a eliminar</param>
        /// <returns>Respuesta 204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tdocumento>> DeleteTdocumento(long id)
        {
            if (!this.TdocumentoExists(id))
            {
                return NotFound();
            }

            var tdocumento = await context.Tdocumentos.FirstOrDefaultAsync(x => x.Id == id);
            context.Tdocumentos.Remove(tdocumento);
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método privado
        /// Comprueba si el tipo de documento consultado existe
        /// </summary>
        /// <param name="id">Id del tipo de documento consultado</param>
        /// <returns>Existe o no existe</returns>
        private bool TdocumentoExists(long id)
        {
            return context.Tdocumentos.Any(e => e.Id == id);
        }
    }
}
