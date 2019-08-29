using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Entities
{
    /// <summary>
    /// Áreas de la empresa
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Id del área en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre del area
        /// </summary>
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Listado de personas que tiene el área
        /// </summary>
        public List<Persona> Personas { get; set; }
    }
}
