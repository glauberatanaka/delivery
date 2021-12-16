using AutoMapper;
using Delivery.Core.Entities.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>();

            CreateMap<PatchRequest, Produto>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Nome, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.Nome));
                    opt.MapFrom(src => src.Nome);
                })
                .ForMember(p => p.Descricao, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.Descricao));
                    opt.MapFrom(src => src.Descricao);
                })
                .ForMember(p => p.Preco, opt =>
                {
                    opt.Condition(src => src.Preco.HasValue);
                    opt.MapFrom(src => src.Preco);
                })
                .ForMember(p => p.QuantidadeEmEstoque, opt =>
                {
                    opt.Condition(src => src.QuantidadeEmEstoque.HasValue);
                    opt.MapFrom(src => src.QuantidadeEmEstoque);
                });
        }
    }
}
