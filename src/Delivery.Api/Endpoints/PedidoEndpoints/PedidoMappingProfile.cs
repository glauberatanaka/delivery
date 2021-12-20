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
            CreateMap<Pedido, PedidoDto>();

            CreateMap<PedidoItem, PedidoItemDto>();

            CreateMap<PedidoEndereco, PedidoEnderecoDto>();
        }
    }
}
