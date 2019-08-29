using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Entities
{
    /// <summary>
    /// Tipos de elementos que tiene la empresa
    /// </summary>
    public class Telemento
    {
        /// <summary>
        /// Id del tipo de elemento en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre del tipo de elemento
        /// </summary>
        [Required]
        public string Nombre { get; set; }

        /// <summary>
        /// Listado de elementos que tiene el tipo de elemento
        /// </summary>
        public List<Elemento> Elementos { get; set; }

    }
}
