using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Models
{
    /// <summary>
    /// Modelo de las personas registradas en la empresa
    /// </summary>
    public class PersonaVM
    {
        /// <summary>
        /// Id de la persona en la base de datos
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Documento de la persona
        /// </summary>
        [Required]
        [MaxLength(10, ErrorMessage = "El documento no puede exceder los 10 dígitos")]
        public string Documento { get; set; }

        /// <summary>
        /// Nombre completo de la persona
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Primer nombre de la persona
        /// </summary>
        [Required]
        public string PrimerNombre { get; set; }

        /// <summary>
        /// Segundo nombre de la persona
        /// </summary>
        public string SegundoNombre { get; set; }

        /// <summary>
        /// Primer apellido de la persona
        /// </summary>
        [Required]
        public string PrimerApellido { get; set; }

        /// <summary>
        /// Segundo apellido de la persona
        /// </summary>
        public string SegundoApellido { get; set; }

        /// <summary>
        /// Fecha de nacimiento de la persona
        /// </summary>
        [Required]
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Dirección de residencia de la persona
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Teléfono de contacto de la persona
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Email de contacto de la persona
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Id del tipo de documento de la persona
        /// </summary>
        public string TipoDocumento { get; set; }

        /// <summary>
        /// Id del área donde está la persona
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Id del tipo de documento de la persona
        /// </summary>
        [Required]
        public long TdocumentoId { get; set; }

        /// <summary>
        /// Id del área donde está la persona
        /// </summary>
        [Required]
        public long AreaId { get; set; }
        
        /// <summary>
        /// Elementos que se le han asignado a la persona
        /// </summary>
        public List<PersonaElementoVM> PersonaElementos { get; set; }

    }
}
