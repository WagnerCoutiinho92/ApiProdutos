using AutoMapper;
using Projeto.Entidades;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Services.Mappings
{
    public class EntityToModelMapping : Profile
    {
        public EntityToModelMapping()
        {
            CreateMap<Estoque, EstoqueConsultaModel>()
                .ForMember(para => para.QuantidadeProdutos, de => de.MapFrom(e => e.Produtos.Sum(p => p.Quantidade)));

            CreateMap<Produto, ProdutoConsultaModel>()
                .ForMember(para => para.Total,de => de.MapFrom(p => p.Preco * p.Quantidade)).ForMember(para => para.NomeEstoque,de => de.MapFrom(p => p.Estoque.Nome));
        }
    }
}