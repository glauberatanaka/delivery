using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.CarrinhoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class CarrinhoMappingProfile : Profile
    {
        public CarrinhoMappingProfile()
        {
            CreateMap<Carrinho, CarrinhoDto>();

            CreateMap<CarrinhoItem, CarrinhoItemDto>();
        }
    }
}
