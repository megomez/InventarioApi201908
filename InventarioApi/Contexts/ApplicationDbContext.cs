using InventarioApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Áreas de la empresa
        /// </summary>
        public DbSet<Area> Areas { get; set; }

        /// <summary>
        /// Tipos de elementos que tiene la empresa
        /// </summary>
        public DbSet<Telemento> Telementos { get; set; }

        /// <summary>
        /// Tipo de documento de las personas
        /// </summary>
        public DbSet<Tdocumento> Tdocumentos { get; set; }

        /// <summary>
        /// Personas registradas en la empresa
        /// </summary>
        public DbSet<Persona> Personas { get; set; }

        /// <summary>
        /// Inventario de elementos que hay en la empresa
        /// </summary>
        public DbSet<Elemento> Elementos { get; set; }

        /// <summary>
        /// Elementos asignados a cada una de las personas
        /// </summary>
        public DbSet<PersonaElemento> PersonaElementos { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
