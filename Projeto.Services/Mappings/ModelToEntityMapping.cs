using AutoMapper;
using Projeto.Entidades;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Services.Mappings
{
    public class ModelToEntityMapping : Profile
    {
        public ModelToEntityMapping()
        {
            CreateMap<EstoqueCadastroModel, Estoque>();
            CreateMap<EstoqueEdicaoModel, Estoque>();

            CreateMap<ProdutoCadastroModel, Produto>();
            CreateMap<ProdutoEdicaoModel, Produto>();
        }
    }
}