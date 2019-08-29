using AutoMapper;
using InventarioApi.Entities;
using InventarioApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tdocumento, TdocumentoVM>();
            CreateMap<TdocumentoVM, Tdocumento>();

            CreateMap<Telemento, TelementoVM>();
            CreateMap<TelementoVM, Telemento>();

            CreateMap<Elemento, ElementoVM>();
            CreateMap<ElementoVM, Elemento>();

            CreateMap<Area, AreaVM>();
            CreateMap<AreaVM, Area>();

            CreateMap<Persona, PersonaVM>();
            CreateMap<PersonaVM, Persona>();

            CreateMap<PersonaElemento, PersonaElemento>();
            CreateMap<PersonaElemento, PersonaElemento>();
        }
    }
}
