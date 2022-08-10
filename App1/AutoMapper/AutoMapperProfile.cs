using App1.Models.Locals.Backbone.Empresas;
using App1.Models.Locals.Finanzas.Turnos;
using App1.Models.Requests.Backbone.Empresas;
using App1.Models.Requests.Finanzas.Turnos;
using App1.Models.Responses.Backbone.Empresas;
using App1.Models.Responses.Finanzas.Turnos;
using AutoMapper;
using System;

namespace App1.AutoMapper
{
    public static class AutoMapperProfile
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmpresaResponse, EmpresaModel>();

                cfg.CreateMap<EmpresaModel, EmpresaRequest>();

                cfg.CreateMap<EmpresaCorteResponse, EmpresaCorteModel>();

                cfg.CreateMap<EmpresaFormaPagoResponse, EmpresaFormaPagoModel>();

                cfg.CreateMap<TurnoResponse, TurnoModel>()
                .ForMember(dto => dto.Detalles, mapper => mapper.Ignore())
                .ForMember(dto => dto.FormasPago, mapper => mapper.Ignore());

                cfg.CreateMap<TurnoDetalleResponse, TurnoDetalleModel>();

                cfg.CreateMap<TurnoFormaPagoResponse, TurnoFormaPagoModel>();

                cfg.CreateMap<TurnoModel, TurnoRequest>();

                cfg.CreateMap<TurnoDetalleModel, TurnoDetalleRequest>();

                cfg.CreateMap<TurnoFormaPagoModel, TurnoFormaPagoRequest>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
