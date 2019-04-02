using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Data.Mappings
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            //chave primária..
            HasKey(p => p.IdProduto);
            //demais propriedades..
            Property(p => p.Nome)
            .HasMaxLength(50)
            .IsRequired();

            Property(p => p.Preco)
            .HasPrecision(18, 2)
            .IsRequired();

            Property(p => p.Quantidade)
            .IsRequired();

            Property(p => p.DataCadastro)
            .IsRequired();

            //Mapeamento do relacionamento..
            HasRequired(p => p.Estoque) //Produto TEM 1 Estoque
            .WithMany(e => e.Produtos) //Estoque TEM Muitos Produtos
            .HasForeignKey(p => p.IdEstoque); //Chave estrangeira
        }
    }
}
