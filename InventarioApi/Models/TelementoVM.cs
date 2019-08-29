using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Models
{
    public class TelementoVM
    {
        /// <summary>
        /// Id del tipo de elemento en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre del tipo de elemento
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Listado de elementos
        /// </summary>
        public List<ElementoVM> Elementos { get; set; }
    }
}
