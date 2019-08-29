using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Models
{
    /// <summary>
    /// Modelo de las áreas de la empresa
    /// </summary>
    public class AreaVM
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
        /// Nombre del area
        /// </summary>
        public List<PersonaVM> Personas { get; set; }
    }
}
