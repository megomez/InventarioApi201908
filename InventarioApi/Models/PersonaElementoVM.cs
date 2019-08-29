using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Models
{
    /// <summary>
    /// Elementos asignados a cada una de las personas
    /// </summary>
    public class PersonaElementoVM
    {
        /// <summary>
        /// Id del registro del elemento asígnado a la persona
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Observaciones de la asignación de un elemento a una persona
        /// </summary>
        public string ObservacionAsignacion { get; set; }

        /// <summary>
        /// Observaciones del retiro de un elemento a una persona
        /// </summary>
        public string ObservacionRetiro { get; set; }

        /// <summary>
        /// Fecha de asignación de un elemento a una persona
        /// </summary>
        public DateTime FechaAsignacion { get; set; }

        /// <summary>
        /// Fecha de retiro de un elemento a una persona
        /// </summary>
        public DateTime FechaRetiro { get; set; }

        /// <summary>
        /// Id de la persona a la que se le asigna el elemento
        /// </summary>
        public long PersonaId { get; set; }

        /// <summary>
        /// Nombre de la persona a la que se le asigna el elemento
        /// </summary>
        public string Persona { get; set; }

        /// <summary>
        /// Id del elemento que se le asigna a una persona
        /// </summary>
        public long ElementoId { get; set; }

        /// <summary>
        /// Elemento que se le asigna a una persona
        /// </summary>
        public string Elemento { get; set; }
    }
}
