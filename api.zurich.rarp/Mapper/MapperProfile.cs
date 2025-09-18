using AutoMapper;
using biz.zurich.rarp.Entities;


namespace api.zurich.rarp.Mapper
{
    /// <summary>
    /// Mapper de Models a Entities
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MapperProfile()
        {

            #region Clientes
            CreateMap<Usuario, Models.Clientes.ClientesDto>().ReverseMap();
            CreateMap<Usuario, Models.Clientes.CrearClientesDto>().ReverseMap();
            CreateMap<Usuario, Models.Clientes.UpdateClientesDto>().ReverseMap();
            #endregion
            #region Polizas
            CreateMap<Poliza, Models.Clientes.PolizaDto>().ReverseMap();
            CreateMap<Poliza, Models.Clientes.CrearPolizaDto>().ReverseMap();
            CreateMap<Poliza, Models.Clientes.UpdatePolizaDto>().ReverseMap();
            #endregion
        }
    }
}
