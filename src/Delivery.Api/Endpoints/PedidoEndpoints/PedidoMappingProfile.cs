using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.PedidoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    public class PedidoMappingProfile : Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.PedidoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UsuarioNome,
                    opt => opt.MapFrom(src => src.IdentityUser.UserName));

            CreateMap<PedidoItem, PedidoItemDto>();

            CreateMap<PedidoEndereco, PedidoEnderecoDto>();
        }
    }
}
