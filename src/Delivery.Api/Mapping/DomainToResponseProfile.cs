using AutoMapper;
using Delivery.Api.Endpoints.ProdutoEndpoints;
using Delivery.Core.Entities.ProdutoAggregate;

namespace Delivery.Api.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Produto, CreateProdutoResponse>();
        }
    }
}
