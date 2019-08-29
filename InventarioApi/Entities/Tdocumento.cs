using InventarioApi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Entities
{
    /// <summary>
    /// Tipo de documento de las personas
    /// </summary>
    public class Tdocumento
    {
        /// <summary>
        /// Id del tipo de documento en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre del tipo de documento
        /// </summary>
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Sigla del tipo de documento
        /// </summary>
        [Required]
        public string Sigla { get; set; }

        /// <summary>
        /// Listado de personas que tiene el tipo de documento
        /// </summary>
        public List<Persona> Personas { get; set; }
    }
}
