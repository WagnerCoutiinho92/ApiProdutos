
using AutoMapper;
using CrudApi.Domain.Entities;

namespace CrudApi.Application.Mapping;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, Produto>().ReverseMap();
    }
}
