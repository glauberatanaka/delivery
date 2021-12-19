using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.ProdutoAggregate;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.Id));

            CreateMap<PatchRequest, Produto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Nome, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.Nome));
                    opt.MapFrom(src => src.Nome);
                })
                .ForMember(dest => dest.Descricao, opt =>
                {
                    opt.Condition(src => !string.IsNullOrEmpty(src.Descricao));
                    opt.MapFrom(src => src.Descricao);
                })
                .ForMember(dest => dest.Preco, opt =>
                {
                    opt.Condition(src => src.Preco.HasValue);
                    opt.MapFrom(src => src.Preco);
                })
                .ForMember(dest => dest.QuantidadeEmEstoque, opt =>
                {
                    opt.Condition(src => src.QuantidadeEmEstoque.HasValue);
                    opt.MapFrom(src => src.QuantidadeEmEstoque);
                });
        }
    }
}
