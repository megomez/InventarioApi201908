using InventarioApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Models
{
    /// <summary>
    /// Elementos que hay en la empresa
    /// </summary>
    public class ElementoVM
    {
        /// <summary>
        /// Id del inventario en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Descripción del elemento en el inventario
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Número de serie del elemento que hay en el inventario
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Valor monetario del elemento que está en el inventario
        /// </summary>
        public double ValorCompra { get; set; }

        /// <summary>
        /// Fecha de compra del elemento que está en el inventario
        /// </summary>
        public DateTime FechaCompra { get; set; }

        /// <summary>
        /// Estado del elemento
        /// ('Sin asignar', 'Asignado', 'Dañado', 'Retirado')
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Id del Tipo de elemento
        /// </summary>
        public long TelementoId { get; set; }

        /// <summary>
        /// Tipo de elemento
        /// </summary>
        public string Telemento { get; set; }

        /// <summary>
        /// Personas a las que se ha asignado el elemento
        /// </summary>
        public List<PersonaElementoVM> PersonaElementos { get; set; }
    }
}
